//System/Form libraries.
using System;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

//Engoc libraries.
using Encog.Neural.Networks;
using Encog.Neural.Networks.Layers;
using Encog.Engine.Network.Activation;
using Encog.ML.Data;
using Encog.Neural.Networks.Training.Propagation.Back;
using Encog.ML.Train;
using Encog.ML.Data.Basic;
using Encog.Util.CSV;
using Encog.App.Analyst;
using Encog.App.Analyst.CSV.Normalize;

//Accord libraries.
using Accord.Neuro;
using Accord.Neuro.Learning;

namespace ENP1
{
    public partial class TestForm : Form
    {
        public TestForm()
        {
            InitializeComponent();
        }

        //Global path variable for working directory.
        public string path;
        public string dataFile;

        /// <summary> need comments </summary>
        private void Form1_Load(object sender, EventArgs e) { }

        /// Buttons ///

        /// <summary> need comments </summary>
        private List<string> GetFile(bool titleType)
        {
            //Reset paths.
            path = null; dataFile = null;

            if((int)(outputsUpDown.Value) == 0)
            {
                MessageBox.Show("You must have at least 1 output", "Output Error.");
                return null;
            }

            //Get csv path and create normalized version path.
            openFileDialog1.Filter = "csv files (*.csv)|*.csv";
            DialogResult dialogResult = openFileDialog1.ShowDialog();
            
            //Exit function if file selection was cancelled.
            if (dialogResult == DialogResult.Cancel)
            {
                Output.Text += "Cancelling File Selection. . .\n";
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
            var sourcefile = new FileInfo(openFileDialog1.FileName);
            var normalfile = new FileInfo(openFileDialog1.FileName.Replace(".csv", "Normal.csv"));

            Output.Text += "Loading File: " + dataFile + ". . .\n";

            return Data.Normalise(sourcefile, normalfile, path, dataFile, (int)(outputsUpDown.Value), titleType);
        }

        /// <summary> need comments </summary>
        private void NetworkBtn_Click(object sender, EventArgs e)
        {
            List<string> outputTitles = GetFile(false);

            if (outputTitles == null)
            {
                return;
            }

            //False when percentage split, true when cross validation.
            bool validation;

            if (radBtnSplit.Checked)
            {
                validation = false;
            }
            else
            {
                validation = true;
            }

            //Setup training dataset.
            Data info = new Data(); info = info.ReturnInfo(path + dataFile.Replace(".csv", "Normal.csv"), outputTitles, sampleBar.Value, validation);

            //Load analyst from earlier.
            var analyst = new EncogAnalyst();
            analyst.Load(new FileInfo(path + @"\normalizationData" + dataFile.Replace(".csv", "") + ".ega"));

            var sourcefile = new FileInfo(path + dataFile);

            var norm = new AnalystNormalizeCSV();
            norm.Analyze(sourcefile, true, CSVFormat.English, analyst);

            NeuralNetwork network;

            if (radBtnEncog.Checked)
            {
                if (!deepNetworkBox.Checked)
                {
                    Output.Text += ("\n@Encog:\n\n");
                    network = new EncogNeuralNetwork();
                }
                else
                {
                    Output.Text += ("\n@Deep Encog:\n\n");
                    network = new EncogDeepNeuralNetwork();
                }
            }
            else
            {
                if (!deepNetworkBox.Checked)
                {
                    Output.Text += ("\n@Accord:\n\n");
                    network = new AccordNeuralNetwork();
                }
                else
                {
                    Output.Text += ("\n@Deep Accord:\n\n");
                    network = new AccordDeepNeuralNetwork();
                }
            }

            if (validation)
            {
                int poolSize = (info.InputData.Length * sampleBar.Value) / 10;

                double[][] arrayIn = info.InputData; double[][] arrayOut = info.OutputData;

                info.InputData = Data.CreateArray<double>(poolSize, info.InputData[0].Length);
                info.OutputData = Data.CreateArray<double>(poolSize, info.OutputData[0].Length);

                Random rnd = new Random();

                int[] index = new int[arrayIn.Length - 1];

                for (int j = 0; j < info.InputData.Length; j++)
                {
                    index[j] = rnd.Next(0, arrayIn.Length);
                    info.InputData[j] = arrayIn[index[j]]; info.OutputData[j] = arrayOut[index[j]];
                }

                arrayIn = Data.RemoveFromArray(arrayIn, index, poolSize);
                arrayOut = Data.RemoveFromArray(arrayOut, index, poolSize);

                for (int i = 0; i < arrayIn.Length / poolSize; i++)
                {
                    info.InputDataSample = Data.CreateArray<double>(poolSize, arrayIn[0].Length);
                    info.OutputDataSample = Data.CreateArray<double>(poolSize, arrayOut[0].Length);

                    for (int j = 0; j < info.InputDataSample.Length; j++)
                    {
                        index[j] = rnd.Next(0, arrayIn.Length);
                        info.InputDataSample[j] = arrayIn[index[j]]; info.OutputDataSample[j] = arrayOut[index[j]];
                    }

                    arrayIn = Data.RemoveFromArray(arrayIn, index, poolSize);
                    arrayOut = Data.RemoveFromArray(arrayOut, index, poolSize);

                    Output.Text += "Training complete with an inaccuracy of: " + Math.Round(network.Train(info, ((float)(learningRateBar.Value) / 10), ((float)(momentumBar.Value) / 10)), 10) + "\n\n";

                    double[][] answers = Data.CreateArray<double>(poolSize, info.InputData[0].Length);

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

                    Output.Text += network.Display(answers, info, analyst);
                }
            }
            else
            {
                network.Create(info.InputNumber, info.OutputNumber);
                Output.Text += "Training complete with an inaccuracy of: " + Math.Round(network.Train(info, ((float)(learningRateBar.Value) / 10), ((float)(momentumBar.Value) / 10)), 5) + "\n\n";

                double[][] answers = Data.CreateArray<double>(info.InputDataSample.Length, info.InputDataSample[0].Length);

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

                Output.Text += network.Display(answers, info, analyst);
            }
        }

        /// <summary> need comments </summary>
        private void RateTestBtn_Click(object sender, EventArgs e)
        {
            List<string> outputTitles = GetFile(false);

            if (outputTitles == null)
            {
                return;
            }

            //False when percentage split, true when cross validation.
            bool validation = false;

            if (radBtnSplit.Checked)
            {
                validation = false;
            }
            else
            {
                validation = true;
            }

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
                            Data info = new Data(); info = info.ReturnInfo(path + dataFile.Replace(".csv", "Normal.csv"), outputTitles, sampleBar.Value, validation);

                            //Setup network
                            Accord.Neuro.IActivationFunction function = new SigmoidFunction();
                            ActivationNetwork networkAccord = new ActivationNetwork(function, info.InputNumber, neurons, info.OutputNumber);

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

                            } while ((((end.Hour * 60 * 60) + end.Minute * 60) + end.Second) - (((start.Hour * 60 * 60) + start.Minute * 60) + start.Second) < 1);

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

                            } while ((((end.Hour * 60 * 60) + end.Minute * 60) + end.Second) - (((start.Hour * 60 * 60) + start.Minute * 60) + start.Second) < 1);

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

        private void NetworkSaveBtn_Click(object sender, EventArgs e)
        {
            List<string> outputTitles = GetFile(false);
            List<string> inputTitles = new List<string>();

            if (outputTitles == null)
            {
                return;
            }

            //False when percentage split, true when cross validation.
            bool validation = true;

            //Setup training dataset.
            Data info = new Data(); info = info.ReturnInfo(path + dataFile.Replace(".csv", "Normal.csv"), outputTitles, sampleBar.Value, validation);

            //Load analyst from earlier.
            var analyst = new EncogAnalyst();
            analyst.Load(new FileInfo(path + @"\normalizationData" + dataFile.Replace(".csv", "") + ".ega"));

            var sourcefile = new FileInfo(path + dataFile);

            var norm = new AnalystNormalizeCSV();
            norm.Analyze(sourcefile, true, CSVFormat.English, analyst);

            for (int i = 0; i < analyst.Script.Fields.Length - outputTitles.Count; i++)
            {
                inputTitles.Add(analyst.Script.Fields[i].Name);
            }

            NeuralNetwork network;

            if (radBtnEncog.Checked)
            {
                if (!deepNetworkBox.Checked)
                {
                    Output.Text += ("\n@Encog:\n\n");
                    network = new EncogNeuralNetwork();
                }
                else
                {
                    Output.Text += ("\n@Deep Encog:\n\n");
                    network = new EncogDeepNeuralNetwork();
                }
            }
            else
            {
                if (!deepNetworkBox.Checked)
                {
                    Output.Text += ("\n@Accord:\n\n");
                    network = new AccordNeuralNetwork();
                }
                else
                {
                    Output.Text += ("\n@Deep Accord:\n\n");
                    network = new AccordNeuralNetwork();
                }
            }

            network.Create(info.InputNumber, info.OutputNumber);

            NetworkSaveData networkSave = new NetworkSaveData();
            networkSave.Inaccuracy = Math.Round(network.Train(info, ((float)(learningRateBar.Value) / 10), ((float)(momentumBar.Value) / 10)), 5) + "\n\n";
            networkSave.NetworkFile = path + dataFile.Replace(".csv", "");
            network.Save(path + dataFile.Replace(".csv", ""));

            networkSave.AnalystFile = path + @"\normalizationData" + dataFile.Replace(".csv", "") + ".ega";
            networkSave.CsvFile = path + dataFile;
            networkSave.Headings = inputTitles;
            networkSave.Name = nameTxt.Text;

            using (var sw = new StreamWriter(path + "networks.json", true))
            using (var jsw = new JsonTextWriter(sw))
            {
                //jsw.Formatting = Formatting.Indented;
                JsonSerializer serial = new JsonSerializer();
                serial.Serialize(jsw, networkSave);
                sw.WriteLine();
            }

            Output.Text += "Successfully saved network " + nameTxt.Text + " with a training inaccuracy of: " + networkSave.Inaccuracy;
        }

        /// <summary> need comments </summary>
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

        /// <summary> need comments </summary>
        private void SampleBar_Scroll(object sender, EventArgs e)
        {
            if(radBtnSplit.Checked)
            {
                sampleLbl.Text = String.Format("Sample Data: " + (sampleBar.Value * 10).ToString() + "%");
            }
            else
            {
                sampleLbl.Text = String.Format("Pool Size: " + (sampleBar.Value * 10).ToString() + "%");
            }
        }

        /// <summary> need comments </summary>
        private void LearningRateBar_Scroll(object sender, EventArgs e)
        {
            learningRateLbl.Text = String.Format("Learning Rate: " + ((float)(learningRateBar.Value) / 10).ToString());
        }

        /// <summary> need comments </summary>
        private void MomentumBar_Scroll(object sender, EventArgs e)
        {
            momentumLbl.Text = String.Format("Momentum: " + ((float)(momentumBar.Value) / 10).ToString());
        }
        
        /// Radio buttons ///

        /// <summary> need comments </summary>
        private void RadBtnSplit_CheckedChanged(object sender, EventArgs e)
        {
            sampleLbl.Text = String.Format("Sample Data: " + (sampleBar.Value * 10).ToString() + "%");
        }

        /// <summary> need comments </summary>
        private void RadBtnCrossVal_CheckedChanged(object sender, EventArgs e)
        {
            sampleLbl.Text = String.Format("Pool Size: " + (sampleBar.Value * 10).ToString() + "%");
        }
    }
}