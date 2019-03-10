using Encog.App.Analyst;
using Encog.App.Analyst.CSV.Normalize;
using Encog.Util.CSV;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ENP1
{
    public partial class TestForm : Form
    {
        public TestForm()
        {
            InitializeComponent();
        }
        
        string path;
        string dataFile;
        List<string> outputTitles = new List<string>();

        private void SetPanel(Panel panel)
        {
            foreach (Control p in Controls)
            {
                if (p is Panel)
                {
                    if (panel == p)
                    {
                        ClientSize = p.Size;
                        p.Visible = true;
                        p.Location = new System.Drawing.Point(0, 0);
                        backBtn.Location = new System.Drawing.Point(10, p.Height - backBtn.Height - 10);
                    }
                    else
                    {
                        p.Visible = false;
                    }
                }
            }
        }

        private void Loading()
        {
            
        }

        /// <summary> Form load. </summary>
        private void Form1_Load(object sender, EventArgs e)
        {
            SetPanel(panel1);
        }

        /// Buttons ///

        private void FileBtn_Click(object sender, EventArgs e)
        {
            //Reset paths.
            path = null; dataFile = null;

            int outputs = (int)outputsUpDown.Value;

            if (outputs == 0)
            {
                MessageBox.Show("You must have at least 1 output", "Output Error.");
                return;
            }

            //Get csv path.
            openFileDialog1.Filter = "csv files (*.csv)|*.csv";
            DialogResult dialogResult = openFileDialog1.ShowDialog();

            //Exit function if file selection was cancelled.
            if (dialogResult == DialogResult.Cancel)
            {
                MessageBox.Show("Cancelling File Selection.", "File Cancelation");
                return;
            }

            //Check the file is .csv format.
            if (!openFileDialog1.SafeFileName.EndsWith(".csv"))
            {
                MessageBox.Show("The file you have selected is not in the correct format (.csv)", "File Access Error");
                return;
            }

            //Setup paths from file.
            dataFile = openFileDialog1.SafeFileName;
            path = openFileDialog1.FileName.Replace(openFileDialog1.SafeFileName, "");
            var sourceFile = new FileInfo(openFileDialog1.FileName);

            if (!Directory.Exists(openFileDialog1.FileName.Replace(openFileDialog1.SafeFileName, "normal")))
            {
                Directory.CreateDirectory(openFileDialog1.FileName.Replace(openFileDialog1.SafeFileName, "normal"));
            }

            var normalFile = new FileInfo(openFileDialog1.FileName.Replace(openFileDialog1.SafeFileName, @"normal\" + openFileDialog1.SafeFileName.Replace(".csv", "Normal.csv")));

            if (Data.IsFileLocked(sourceFile, false))
            {
                return;
            }

            if (Data.IsFileLocked(normalFile, true))
            {
                return;
            }

            loadedLbl.Text = "Loaded File: " + dataFile;

            outputTitles = Data.Normalise(sourceFile, normalFile, path, dataFile, outputs);

            if (outputTitles == null)
            {
                return;
            }

            SetPanel(panel2);
        }

        /// <summary> Button to test network settings. </summary>
        private void NetworkBtn_Click(object sender, EventArgs e)
        {
            //False when percentage split, true when cross validation.
            bool validation = !radBtnSplit.Checked;

            //Setup training dataset.
            Data info = new Data(); info = info.ReturnInfo(path + @"normal\" + dataFile.Replace(".csv", "Normal.csv"), outputTitles, sampleBar.Value, validation);

            if (info == null)
            {
                return;
            }

            //Load analyst from earlier.
            var analyst = new EncogAnalyst();
            var normalisationData = new FileInfo(path + @"normal\" + "normalizationData" + dataFile.Replace(".csv", "") + ".ega");

            if(Data.IsFileLocked(normalisationData, false))
            {
                return;
            }

            analyst.Load(normalisationData);

            var sourcefile = new FileInfo(path + dataFile);

            var norm = new AnalystNormalizeCSV();

            if(Data.IsFileLocked(sourcefile, false))
            {
                return;
            }

            norm.Analyze(sourcefile, true, CSVFormat.English, analyst);

			//Setup network.
            NeuralNetwork network;

			//If to decide which network type to use.
            if (radBtnEncog.Checked)
            {
                if (!deepNetworkBox.Checked)
                {
                    output.Text = "@Encog:\n\n";
                    network = new EncogNeuralNetwork();
                }
                else
                {
                    output.Text = "@Deep Encog:\n\n";
                    network = new EncogDeepNeuralNetwork();
                }
            }
            else
            {
                if (!deepNetworkBox.Checked)
                {
                    output.Text = "@Accord:\n\n";
                    network = new AccordNeuralNetwork();
                }
                else
                {
                    output.Text = "@Deep Accord:\n\n";
                    network = new AccordDeepNeuralNetwork();
                }
            }

            SetPanel(panel4);
            TaskCompletionSource<double> task = new TaskCompletionSource<double>();
            Task<double> t1 = task.Task;

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
                    network.Create(info.InputNumber, layersBar.Value, neuronsBar.Value, info.OutputNumber);

                    float lr = (float)(learningRateBar.Value) / 10; float mom = (float)(momentumBar.Value) / 10;

                    Task.Factory.StartNew(() =>
                    {
                        task.SetResult(Math.Round(network.Train(info, lr, mom), 5));
                    });

                    output.Text += "Training complete with an inaccuracy of: " + t1.Result + "\n\n";

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
                    output.Text += network.Display(answers, analyst, info, outputTitles, path + @"normal\" + dataFile.Replace(".csv", "Normal.csv"));
                }
            }
			//Else percentage split.
            else
            {
                //Create network.
                network.Create(info.InputNumber, layersBar.Value, neuronsBar.Value, info.OutputNumber);

                float lr = (float)(learningRateBar.Value) / 10; float mom = (float)(momentumBar.Value) / 10;

                Task.Factory.StartNew(() =>
                {
                    task.SetResult(Math.Round(network.Train(info, lr, mom), 5));
                });

                output.Text += "Training complete with an inaccuracy of: " + t1.Result + "\n\n";

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

            SetPanel(panel3);
        }

        /// <summary> Test function to iterate through all learning rates and momentums with different numbers of neurons and layers. </summary>
        private void RateTestBtn_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("This function will take approximately an hour to complete, do you want to proceed?", "Confirmation", MessageBoxButtons.YesNo);
            if (result == DialogResult.No)
            {
                return;
            }

            //False when percentage split, true when cross validation.
            bool validation = true;

            //Save network to file.
            if (!Directory.Exists(path + "tests"))
            {
                Directory.CreateDirectory(path + "tests");
            }

            string header = path + "tests" + @"\Results" + dataFile;

            if (Data.IsFileLocked(new FileInfo(header), true))
            {
                return;
            }

            using (StreamWriter sw = new StreamWriter(header))
            {
                sw.WriteLine(String.Format("Error,Network Type,Layers,Neurons,Learning Rate,Momentum"));
            }

            // initialize input and output values.
            Data info = new Data(); info = info.ReturnInfo(path + @"normal\" + dataFile.Replace(".csv", "Normal.csv"), outputTitles, sampleBar.Value, validation);

            if (info == null)
            {
                return;
            }

            //Setup network.
            List<NeuralNetwork> networkList = new List<NeuralNetwork>
            {
                new EncogNeuralNetwork(),
                new EncogDeepNeuralNetwork(),
                new AccordNeuralNetwork(),
                new AccordDeepNeuralNetwork()
            };

            List<BestNetwork> bestList = new List<BestNetwork>();

            SetPanel(panel4);
            List<Task<BestNetwork>> taskList = new List<Task<BestNetwork>>();

            var start = DateTime.Now;

            Parallel.ForEach(networkList, network => taskList.Add(Task<BestNetwork>.Factory.StartNew(() => network.CalculateBest(info, header))));

            Task.WaitAll(taskList.ToArray());

            foreach (Task<BestNetwork> doneTask in taskList)
            {
                bestList.Add(doneTask.Result);
            }

            BestNetwork best = bestList[0];

            int index = 0;
            int count = 0;
            foreach (BestNetwork b in bestList)
            {
                double error = b.Error;

                if (error < best.Error)
                {
                    best = b;
                    index = count;
                }

                count++;
            }

            var end = DateTime.Now - start;

            MessageBox.Show("The best netork is: " + networkList[index].GetType().ToString().Replace("ENP1.", "") 
                + "\nLayers: " + best.Layers + "\nNeurons: " + best.Neurons + "\nLearning Rate: " + best.LearningRate
                + "\nMomentum: " + best.Momentum + "\nInaccuracy: " + best.Error + "\nMinutes taken: " + end.TotalMinutes);

            SetPanel(panel2);
        }

		/// <summary> Button to save current network settings </summary>
        private void NetworkSaveBtn_Click(object sender, EventArgs e)
        {
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

            char[] dirty = Path.GetInvalidFileNameChars();

            foreach (char c in dirty)
            {
                if (nameTxt.Text.Contains(c.ToString()))
                {
                    MessageBox.Show("Your name contains invalid characters. Error at character " + c, "Invalid Network Name.");
                    return;
                }
            }

            //False when percentage split, true when cross validation.
            const bool validation = true;

            //Setup dataset.
            Data info = new Data();
            info = info.ReturnInfo(path + @"normal\" + dataFile.Replace(".csv", "Normal.csv"), outputTitles, 0, validation);

            //Load analyst from earlier.
            var analyst = new EncogAnalyst();

            var normalisationData = new FileInfo(path + @"normal\" + "normalizationData" + dataFile.Replace(".csv", "") + ".ega");

            if (Data.IsFileLocked(normalisationData, false))
            {
                return;
            }

            analyst.Load(normalisationData);

            var sourcefile = new FileInfo(path + dataFile);

            if (Data.IsFileLocked(sourcefile, false))
            {
                return;
            }

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
                    output.Text = "@Encog:\n\n";
                    network = new EncogNeuralNetwork();
                }
                else
                {
                    output.Text = "@Deep Encog:\n\n";
                    network = new EncogDeepNeuralNetwork();
                }
            }
            else
            {
                if (!deepNetworkBox.Checked)
                {
                    output.Text = "@Accord:\n\n";
                    network = new AccordNeuralNetwork();
                }
                else
                {
                    output.Text = "@Deep Accord:\n\n";
                    network = new AccordDeepNeuralNetwork();
                }
            }

			//Create network.
            network.Create(info.InputNumber, layersBar.Value, neuronsBar.Value, info.OutputNumber);

            //Save network to file.
            if (!Directory.Exists(path + "networks"))
            {
                Directory.CreateDirectory(path + "networks");
            }

            SetPanel(panel4);
            TaskCompletionSource<double> task = new TaskCompletionSource<double>();
            Task<double> t1 = task.Task;

            float lr = (float)(learningRateBar.Value) / 10; float mom = (float)(momentumBar.Value) / 10;

            Task.Factory.StartNew(() =>
            {
                task.SetResult(Math.Round(network.Train(info, lr, mom), 5));
            });

            //Save network data to object.
            NetworkSaveData networkSave = new NetworkSaveData
            {
                NetworkFile = path + @"networks\" + nameTxt.Text,
                NetworkType = network.GetType().ToString().Replace("ENP1.", ""), //"ENP1" must change to reflect solution name (name.) if ever changed.
                AnalystFile = path + @"normal\" + "normalizationData.ega",
                CsvFile = dataFile,
                Path = path,
                InputHeadings = inputTitles,
                OutputHeadings = outputTitles,
                Name = nameTxt.Text,

                //Train network.
                Inaccuracy = t1.Result.ToString()
            };

            if ((path + @"networks\" + nameTxt.Text)?.Length < 260)
            {
                network.Save(path + @"networks\" + nameTxt.Text);
            }
            else
            {
                MessageBox.Show("Your file name or total file path is too long for the windows limit of 260.", "Invalid Network Name Size.");
                return;
            }

            if (Data.IsFileLocked(new FileInfo(path + @"networks\networks.json"), true))
            {
                return;
            }

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
            SetPanel(panel3);
        }

        private void BackBtn_Click(object sender, EventArgs e)
        {
            if (panel1.Visible)
            {
                Close();
            }
            else if (panel2.Visible)
            {
                SetPanel(panel1);
            }
            else if (panel3.Visible)
            {
                SetPanel(panel2);
            }
        }

        /// <summary> Button to hide and show advanced network settings. </summary>
        private void AdvancedBtn_Click(object sender, EventArgs e)
        {
            if (learningRateBar.Visible)
            {
                learningRateBar.Hide();
                learningRateLbl.Hide();

                momentumBar.Hide();
                momentumLbl.Hide();

                neuronsBar.Hide();
                neuronsLbl.Hide();

                layersBar.Hide();
                layersLbl.Hide();

                deepNetworkBox.Hide();

                advancedLbl.Text = "Advanced Settings";
                advancedBtn.Text = "+";
            }
            else
            {
                learningRateBar.Show();
                learningRateLbl.Show();

                momentumBar.Show();
                momentumLbl.Show();

                neuronsBar.Show();
                neuronsLbl.Show();
                
                layersBar.Show();
                layersLbl.Show();

                if(deepNetworkBox.Checked && radBtnEncog.Checked)
                {
                    layersBar.Enabled = false;
                }
                else
                {
                    layersBar.Enabled = true;
                }

                deepNetworkBox.Show();

                advancedLbl.Text = "Close";
                advancedBtn.Text = "-";
            }
        }

        private void AdvancedFileBtn_Click(object sender, EventArgs e)
        {
            if (valtypeLbl.Visible)
            {
                radBtnSplit.Hide();
                radBtnCrossVal.Hide();

                valtypeLbl.Hide();

                sampleBar.Hide();

                sampleLbl.Hide();

                advancedFileLbl.Text = "Advanced Settings";
                advancedFileBtn.Text = "+";
            }
            else
            {
                radBtnSplit.Show();
                radBtnCrossVal.Show();

                valtypeLbl.Show();

                sampleBar.Show();

                sampleLbl.Show();

                advancedFileLbl.Text = "Close";
                advancedFileBtn.Text = "-";
            }
        }

        private void RecommendedBtn_Click(object sender, EventArgs e)
        {
            learningRateBar.Value = 1; LearningRateBar_Scroll(sender, e);
            momentumBar.Value = 8; MomentumBar_Scroll(sender, e);
            neuronsBar.Value = 25; NeuronsBar_Scroll(sender, e);
            layersBar.Value = 3; LayersBar_Scroll(sender, e);
            deepNetworkBox.Checked = true; DeepNetworkBox_CheckedChanged(sender, e);
            radBtnAccord.Checked = true;
        }

        /// Scroll bars ///

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

        /// <summary> Scroll bar to select network neuron amount. </summary>
        private void NeuronsBar_Scroll(object sender, EventArgs e)
        {
            neuronsLbl.Text = String.Format("Neurons: " + neuronsBar.Value).ToString();
        }

        /// <summary> Scroll bar to select network layer amount. </summary>
        private void LayersBar_Scroll(object sender, EventArgs e)
        {
            layersLbl.Text = String.Format("Layers: " + layersBar.Value).ToString();
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

        private void RadBtnEncog_CheckedChanged(object sender, EventArgs e)
        {
            if (radBtnEncog.Checked && deepNetworkBox.Checked)
            {
                layersBar.Enabled = false;
            }
            else
            {
                layersBar.Enabled = true;
            }
        }
        
        /// Check Boxes ///

        /// <summary> Check box for deep networks. </summary>
        private void DeepNetworkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (radBtnEncog.Checked && deepNetworkBox.Checked)
            {
                layersBar.Enabled = false;
            }
            else
            {
                layersBar.Enabled = true;
            }
        }
    }
}