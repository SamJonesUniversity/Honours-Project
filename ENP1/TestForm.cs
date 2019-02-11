//System/Form libraries.
//Accord libraries.
using Accord.Neuro;
using Accord.Neuro.Learning;
using Encog.App.Analyst;
using Encog.App.Analyst.CSV.Normalize;
using Encog.Engine.Network.Activation;
using Encog.ML.Data;
using Encog.ML.Data.Basic;
using Encog.ML.Train;
//Engoc libraries.
using Encog.Neural.Networks;
using Encog.Neural.Networks.Layers;
using Encog.Neural.Networks.Training.Propagation.Back;
using Encog.Util.CSV;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace ENP1
{
    public partial class TestForm : Form
    {
        public TestForm()
        {
            InitializeComponent();
        }

        /// <summary> Form load. </summary>
        private void Form1_Load(object sender, EventArgs e) { }

        /// Buttons ///

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Windows.Forms.Control.set_Text(System.String)")]
        private List<string> GetFile(bool titleType, ref string path, ref string dataFile, int outputs)
        {
            //Reset paths.
            path = null; dataFile = null;

            if (outputs == 0)
            {
                MessageBox.Show("You must have at least 1 output", "Output Error.");
                return null;
            }

            //Get csv path.
            openFileDialog1.Filter = "csv files (*.csv)|*.csv";
            DialogResult dialogResult = openFileDialog1.ShowDialog();

            //Exit function if file selection was cancelled.
            if (dialogResult == DialogResult.Cancel)
            {
                output.Text += "Cancelling File Selection. . .\n";
                return null;
            }

            //Check the file is .csv format.
            if (!openFileDialog1.SafeFileName.EndsWith(".csv"))
            {
                MessageBox.Show("The file you have selected is not in the correct format (.csv)", "File Access Error");
                return null;
            }

            //Setup paths from file.
            dataFile = openFileDialog1.SafeFileName;
            path = openFileDialog1.FileName.Replace(openFileDialog1.SafeFileName, "");
            var sourceFile = new FileInfo(openFileDialog1.FileName);

            if(!Directory.Exists(openFileDialog1.FileName.Replace(openFileDialog1.SafeFileName, "normal")))
            {
                Directory.CreateDirectory(openFileDialog1.FileName.Replace(openFileDialog1.SafeFileName, "normal"));
            }

            var normalFile = new FileInfo(openFileDialog1.FileName.Replace(openFileDialog1.SafeFileName, @"normal\" + openFileDialog1.SafeFileName.Replace(".csv", "Normal.csv")));

            output.Text += "Loading File: " + dataFile + ". . .\n";

            return Data.Normalise(sourceFile, normalFile, path, dataFile, outputs, titleType);
        }

        /// <summary> Button to test network settings. </summary>
        private void NetworkBtn_Click(object sender, EventArgs e)
        {
			//Setup paths and lists.
			string dataFile = "";
			string path = "";

			//Normalise file.
            List<string> outputTitles = GetFile(false, ref path, ref dataFile, (int)(outputsUpDown.Value));

            if (outputTitles == null)
            {
                return;
            }

            //False when percentage split, true when cross validation.
            bool validation = !radBtnSplit.Checked;

            //Setup training dataset.
            Data info = new Data(); info = info.ReturnInfo(path + @"normal\" + dataFile.Replace(".csv", "Normal.csv"), outputTitles, sampleBar.Value, validation);

            //Load analyst from earlier.
            var analyst = new EncogAnalyst();
            analyst.Load(new FileInfo(path + @"normal\" + "normalizationData" + dataFile.Replace(".csv", "") + ".ega"));

            var sourcefile = new FileInfo(path + dataFile);

            var norm = new AnalystNormalizeCSV();
            norm.Analyze(sourcefile, true, CSVFormat.English, analyst);

			//Setup network.
            NeuralNetwork network;

			//If to decide which network type to use.
            if (radBtnEncog.Checked)
            {
                if (!deepNetworkBox.Checked)
                {
                    output.Text += "\n@Encog:\n\n";
                    network = new EncogNeuralNetwork();
                }
                else
                {
                    output.Text += "\n@Deep Encog:\n\n";
                    network = new EncogDeepNeuralNetwork();
                }
            }
            else
            {
                if (!deepNetworkBox.Checked)
                {
                    output.Text += "\n@Accord:\n\n";
                    network = new AccordNeuralNetwork();
                }
                else
                {
                    output.Text += "\n@Deep Accord:\n\n";
                    network = new AccordDeepNeuralNetwork();
                }
            }

			//If using cross-validation.
            if (validation)
            {
                //Setup pool size.
                decimal tmpPoolSize = info.InputData.Length * decimal.Divide(sampleBar.Value , 100);
                int poolSize = (int)tmpPoolSize;

                double[][] arrayIn = info.InputData; double[][] arrayOut = info.OutputData;

                info.InputData = Data.CreateArray<double>(poolSize, info.InputData[0].Length);
                info.OutputData = Data.CreateArray<double>(poolSize, info.OutputData[0].Length);

				//Random to randomise pool selection.
                Random rnd = new Random();

                int[] index = new int[poolSize];

				//Radomly allocate items for training pool.
                for (int j = 0; j < info.InputData.Length; j++)
                {
                    index[j] = rnd.Next(0, arrayIn.Length);
                    info.InputData[j] = arrayIn[index[j]]; info.OutputData[j] = arrayOut[index[j]];
                }

				//Remove pooled items from array.
                arrayIn = Data.RemoveFromArray(arrayIn, index, poolSize);
                arrayOut = Data.RemoveFromArray(arrayOut, index, poolSize);

				//Start allocating sample pools.
                for (int i = 0; i <= arrayIn.Length / poolSize; i++)
                {
                    info.InputDataSample = Data.CreateArray<double>(poolSize, arrayIn[0].Length);
                    info.OutputDataSample = Data.CreateArray<double>(poolSize, arrayOut[0].Length);

                    //Radomly allocate items for [i] sample pool.
                    for (int j = 0; j < info.InputDataSample.Length; j++)
                    {
                        index[j] = rnd.Next(0, arrayIn.Length);
                        info.InputDataSample[j] = arrayIn[index[j]]; info.OutputDataSample[j] = arrayOut[index[j]];
                    }

                    arrayIn = Data.RemoveFromArray(arrayIn, index, poolSize);
                    arrayOut = Data.RemoveFromArray(arrayOut, index, poolSize);

                    //Create network.
                    network.Create(info.InputNumber, info.OutputNumber);
                    output.Text += "Training complete with an inaccuracy of: " + Math.Round(network.Train(info, (float)(learningRateBar.Value) / 10, (float)(momentumBar.Value) / 10), 10) + "\n\n";

                    double[][] answers = Data.CreateArray<double>(poolSize, info.InputData[0].Length);

					//Compute outputs.
                    for (int j = 0; j < answers.Length; j++)
                    {
                        if (radBtnAccord.Checked)
                        {
                            if (!deepNetworkBox.Checked)
                            {
                                answers[j] = network.AccordNetwork.Compute(info.InputDataSample[j]);
                            }
                            else if (deepNetworkBox.Checked)
                            {
                                answers[j] = network.DeepAccordNetwork.Compute(info.InputDataSample[j]);
                            }
                        }
                        else
                        {
                            network.EncogNetwork.Compute(info.InputDataSample[j], answers[j]);
                        }
                    }

					//Display network.
                    output.Text += network.Display(answers, analyst, info, outputTitles, path + dataFile.Replace(".csv", "Normal.csv"));
                }
            }
			//Else percentage split.
            else
            {
				//Create network.
                network.Create(info.InputNumber, info.OutputNumber);
                output.Text += "Training complete with an inaccuracy of: " + Math.Round(network.Train(info, (float)(learningRateBar.Value) / 10, (float)(momentumBar.Value) / 10), 5) + "\n\n";

                double[][] answers = Data.CreateArray<double>(info.InputDataSample.Length, info.InputDataSample[0].Length);

				//Compute outputs.
                for (int i = 0; i < answers.Length; i++)
                {
                    if (radBtnAccord.Checked)
                    {
                        if (!deepNetworkBox.Checked)
                        {
                            answers[i] = network.AccordNetwork.Compute(info.InputDataSample[i]);
                        }
                        else if (deepNetworkBox.Checked)
                        {
                            answers[i] = network.DeepAccordNetwork.Compute(info.InputDataSample[i]);
                        }
                    }
                    else
                    {
                        network.EncogNetwork.Compute(info.InputDataSample[i], answers[i]);
                    }
                }

                //Display network.
                output.Text += network.Display(answers, analyst, info, outputTitles, path + @"normal\" + dataFile.Replace(".csv", "Normal.csv"));
            }
        }

		/// This function needs to be re-written ///
        /// <summary> Test function to iterate through all learning rates and momentums with different numbers of neurons and layers. </summary>
        private void RateTestBtn_Click(object sender, EventArgs e)
        {
            //Setup paths and lists.
            string dataFile = "";
            string path = "";

            //Normalise file.
            List<string> outputTitles = GetFile(false, ref path, ref dataFile, (int)(outputsUpDown.Value));

            if (outputTitles == null)
            {
                return;
            }

            //False when percentage split, true when cross validation.
            bool validation = !radBtnSplit.Checked;

            string header = path + "Results.csv";

            using (StreamWriter sw = new StreamWriter(header))
            {
                sw.WriteLine(String.Format("Encog Error,Accord Error,Learning Rate,Momentum,Neurons,Layers"));
            }

            for (int layers = 1; layers <= 4; layers++)
            {
                for (int neurons = 5; neurons <= 15; neurons += 5)
                {
                    for (double lr = 0.1; lr <= 1; lr += 0.1)
                    {
                        for (double m = 0.1; m <= 1; m += 0.1)
                        {
                            // initialize input and output values.
                            Data info = new Data(); info = info.ReturnInfo(path + @"normal\" + dataFile.Replace(".csv", "Normal.csv"), outputTitles, sampleBar.Value, validation);

                            //Setup network
                            Accord.Neuro.IActivationFunction function = new SigmoidFunction();
                            ActivationNetwork networkAccord = new ActivationNetwork(function, info.InputNumber, neurons, info.OutputNumber);

							//Switch to calculate number of layers to use.
                            switch (layers)
                            {
                                case 2:
                                    networkAccord = new ActivationNetwork(function, info.InputNumber, neurons, neurons, info.OutputNumber); //Activation function, input, hidden, hidden, output.
                                    break;

                                case 3:
                                    networkAccord = new ActivationNetwork(function, info.InputNumber, neurons, neurons, neurons, info.OutputNumber); //Activation function, input, hidden, hidden, output.
                                    break;

                                case 4:
                                    networkAccord = new ActivationNetwork(function, info.InputNumber, neurons, neurons, neurons, neurons, info.OutputNumber); //Activation function, input, hidden, hidden, output.
                                    break;

                                case 5:
                                    networkAccord = new ActivationNetwork(function, info.InputNumber, neurons, neurons, neurons, neurons, neurons, info.OutputNumber); //Activation function, input, hidden, hidden, output.
                                    break;
                            }

                            //Setup trainer using backpropagation.
                            BackPropagationLearning teacher = new BackPropagationLearning(networkAccord)
                            {
                                LearningRate = lr,
                                Momentum = m
                            };

                            //Train network on data set.
                            double error = double.PositiveInfinity;

                            //Recording time per tick.
                            DateTime start = DateTime.Now;
                            DateTime end;

                            do
                            {
                                error = teacher.RunEpoch(info.InputData, info.OutputData);
                                end = DateTime.Now;

                            } while (((end.Hour * 60 * 60) + (end.Minute * 60) + end.Second) - ((start.Hour * 60 * 60) + (start.Minute * 60) + start.Second) < 1);

                            IMLDataSet data = new BasicMLDataSet(info.InputData, info.OutputData);
                            IMLDataSet sampleData = new BasicMLDataSet(info.InputDataSample, info.OutputDataSample);

                            //Setup network, parameters (Activation, bias, number of neurons).
                            BasicNetwork networkEncog = new BasicNetwork();
                            networkEncog.AddLayer(new BasicLayer(null, true, info.InputNumber)); //Input.

                            for(int i = 0; i < layers; i++)
                            {
                                networkEncog.AddLayer(new BasicLayer(new ActivationSigmoid(), true, neurons)); //Hidden.
                            }

                            networkEncog.AddLayer(new BasicLayer(new ActivationSigmoid(), false, info.OutputNumber)); //Output.
                            networkEncog.Structure.FinalizeStructure();
                            networkEncog.Reset();

                            //Train network on data set, parameters (Network, dataset, learning rate, momentum).
                            IMLTrain learner = new Backpropagation(networkEncog, data, lr, m);

                            start = DateTime.Now;

                            do
                            {
                                learner.Iteration();
                                end = DateTime.Now;

                            } while (((end.Hour * 60 * 60) + (end.Minute * 60) + end.Second) - ((start.Hour * 60 * 60) + (start.Minute * 60) + start.Second) < 1);

                            using (StreamWriter sw = new StreamWriter(path + "Results.csv", true))
                            {
                                sw.WriteLine(String.Format(
                                    "{0},{1},{2},{3},{4},{5}", learner.Error, error, lr, m, neurons, layers));
                            }
                        }
                    }
                }
            }
        }

		/// <summary> Button to save current network settings </summary>
        private void NetworkSaveBtn_Click(object sender, EventArgs e)
        {
            //Setup paths and lists.
            string dataFile = "";
            string path = "";

            //Normalise file.
            List<string> outputTitles = GetFile(false, ref path, ref dataFile, (int)(outputsUpDown.Value));
            List<string> inputTitles = new List<string>();

            if (outputTitles == null)
            {
                return;
            }

            if (nameTxt.Text?.Length == 0)
            {
                MessageBox.Show("You must give your network a name.", "No Network Name.");
                return;
            }

            //False when percentage split, true when cross validation.
            const bool validation = true;

            //Setup dataset.
            Data info = new Data();
            info = info.ReturnInfo(path + @"normal\" + dataFile.Replace(".csv", "Normal.csv"), outputTitles, 0, validation);

            //Load analyst from earlier.
            var analyst = new EncogAnalyst();
            analyst.Load(new FileInfo(path + @"normal\" + "normalizationData" + dataFile.Replace(".csv", "") + ".ega"));

            var sourcefile = new FileInfo(path + dataFile);

            var norm = new AnalystNormalizeCSV();
            norm.Analyze(sourcefile, true, CSVFormat.English, analyst);

			//Store input headings from analyst.
            for (int i = 0; i < analyst.Script.Fields.Length - outputTitles.Count; i++)
            {
                inputTitles.Add(analyst.Script.Fields[i].Name);
            }

			//Setup network.
            NeuralNetwork network;

			//If to decide which network type to use.
            if (radBtnEncog.Checked)
            {
                if (!deepNetworkBox.Checked)
                {
                    output.Text += "\n@Encog:\n\n";
                    network = new EncogNeuralNetwork();
                }
                else
                {
                    output.Text += "\n@Deep Encog:\n\n";
                    network = new EncogDeepNeuralNetwork();
                }
            }
            else
            {
                if (!deepNetworkBox.Checked)
                {
                    output.Text += "\n@Accord:\n\n";
                    network = new AccordNeuralNetwork();
                }
                else
                {
                    output.Text += "\n@Deep Accord:\n\n";
                    network = new AccordDeepNeuralNetwork();
                }
            }

			//Create network.
            network.Create(info.InputNumber, info.OutputNumber);

            //Save network data to object.
            NetworkSaveData networkSave = new NetworkSaveData
            {
                NetworkFile = path + @"networks\" + nameTxt.Text,
                NetworkType = network.GetType().ToString().Replace("ENP1.",""),
                AnalystFile = path + @"normal\" + "normalizationData.ega",
                CsvFile = dataFile,
                Path = path,
                InputHeadings = inputTitles,
                OutputHeadings = outputTitles,
                Name = nameTxt.Text,

                //Train network.
                Inaccuracy = Math.Round(network.Train(info, (float)(learningRateBar.Value) / 10, (float)(momentumBar.Value) / 10), 5).ToString()
            };

            //Save network to file.
            if (!Directory.Exists(path + "networks"))
            {
                Directory.CreateDirectory(path + "networks");
            }

            network.Save(path + @"networks\" + nameTxt.Text);

            //Write network object to json file.
            using (var sw = new StreamWriter(path + @"networks\networks.json", true))
            {
                using (var jsw = new JsonTextWriter(sw))
                {
                    //jsw.Formatting = Formatting.Indented;
                    JsonSerializer serial = new JsonSerializer();
                    serial.Serialize(jsw, networkSave);
                    sw.WriteLine();
                }
            }

            output.Text += "Successfully saved network " + nameTxt.Text + " with a training inaccuracy of: " + networkSave.Inaccuracy;
        }

        /// <summary> Button to hide and show advanced network settings. </summary>
        private void AdvancedBtn_Click(object sender, EventArgs e)
        {
            if (learningRateBar.Visible)
            {
                learningRateBar.Hide();
                momentumBar.Hide();
                learningRateLbl.Hide();
                momentumLbl.Hide();
                deepNetworkBox.Hide();
                advancedLbl.Text = "Advanced Settings";
                advancedBtn.Text = "+";
            }
            else
            {
                learningRateBar.Show();
                momentumBar.Show();
                learningRateLbl.Show();
                momentumLbl.Show();
                deepNetworkBox.Show();
                advancedLbl.Text = "Close";
                advancedBtn.Text = "-";
            }
        }

        /// Scroll bars. ///

        /// <summary> Scroll bar to select pools size for cross-validation or percentage split size. </summary>
        private void SampleBar_Scroll(object sender, EventArgs e)
        {
            if (sampleBar.Value % sampleBar.SmallChange != 0)
            {
                int trackValue = (sampleBar.Value / sampleBar.SmallChange) * sampleBar.SmallChange;

                sampleBar.Value = trackValue;
            }

            if (radBtnSplit.Checked)
            {
                sampleLbl.Text = String.Format("Sample Data: " + (sampleBar.Value).ToString() + "%");
            }
            else
            {
                sampleLbl.Text = String.Format("Pool Size: " + (sampleBar.Value).ToString() + "%");
            }
        }

        /// <summary> Scroll bar to select network learning rate. </summary>
        private void LearningRateBar_Scroll(object sender, EventArgs e)
        {
            learningRateLbl.Text = String.Format("Learning Rate: " + ((float)(learningRateBar.Value) / 10).ToString());
        }

        /// <summary> Scroll bar to select network momentum. </summary>
        private void MomentumBar_Scroll(object sender, EventArgs e)
        {
            momentumLbl.Text = String.Format("Momentum: " + ((float)(momentumBar.Value) / 10).ToString());
        }

        /// Radio buttons ///

        /// <summary> Percentage split radio button. </summary>
        private void RadBtnSplit_CheckedChanged(object sender, EventArgs e)
        {
            sampleBar.Maximum = 100;
            sampleBar.Minimum = 10;
            sampleBar.SmallChange = 10;
            sampleBar.LargeChange = 10;
            sampleBar.TickFrequency = 10;
            sampleBar.Value = 10;
            sampleLbl.Text = String.Format("Sample Data: " + (sampleBar.Value).ToString() + "%");
        }

        /// <summary> Cross-validation radio button. </summary>
        private void RadBtnCrossVal_CheckedChanged(object sender, EventArgs e)
        {
            sampleBar.Maximum = 30;
            sampleBar.Minimum = 5;
            sampleBar.SmallChange = 5;
            sampleBar.LargeChange = 10;
            sampleBar.TickFrequency = 5;
            sampleBar.Value = 5;
            sampleLbl.Text = String.Format("Pool Size: " + (sampleBar.Value).ToString() + "%");
        }
    }
}