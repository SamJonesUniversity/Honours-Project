//System/Form libraries.
using System;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;

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
using Encog.App.Analyst.Wizard;
using Encog.App.Analyst.CSV.Normalize;
using Encog.App.Analyst.Script.Normalize;

//Accord libraries.
using Accord.Neuro;
using Accord.Neuro.Learning;
using Accord.Math;

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
        List<string> outputTiltes = new List<string>();

        /// <summary> need comments </summary>
        private void Form1_Load(object sender, EventArgs e) { }

        /// Buttons ///

        /// <summary> need comments </summary>
        private void fileBtn_Click(object sender, EventArgs e)
        {
            //Reset paths.
            path = null; dataFile = null;

            //Get csv path and create normalized version path.
            openFileDialog1.Filter = "csv files (*.csv)|*.csv";
            DialogResult dialogResult = openFileDialog1.ShowDialog();
            
            //Exit function if file selection was cancelled.
            if (dialogResult == DialogResult.Cancel)
            {
                Output.Text += "Cancelling File Selection. . .\n";
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
            var sourcefile = new FileInfo(openFileDialog1.FileName);
            var normalfile = new FileInfo(openFileDialog1.FileName.Replace(".csv", "Normal.csv"));

            //Setup analyst with orignal csv.
            var analyst = new EncogAnalyst();
            var wizard = new AnalystWizard(analyst);

            //Additional validation to check that the file is not empty.
            try
            {
                wizard.Wizard(sourcefile, true, AnalystFileFormat.DecpntComma);
            }
            catch (Exception)
            {
                MessageBox.Show("The file you have selected is empty.", "File Access Error");
                return;
            }

            //Setup max and min range for normalization.
            foreach (AnalystField field in analyst.Script.Normalize.NormalizedFields)
            {
                field.NormalizedHigh = 1;
                field.NormalizedLow = 0;
                //field.Action = Encog.Util.Arrayutil.NormalizationAction.OneOf; //Use this to change normalizaiton type.
            }

            //Normalization.
            var norm = new AnalystNormalizeCSV();
            norm.Analyze(sourcefile, true, CSVFormat.English, analyst);
            norm.ProduceOutputHeaders = true;
            norm.Normalize(normalfile);

            for (int i = 1; i + analyst.Script.Fields.Length > analyst.Script.Fields.Length; i--)
            { 
                outputTiltes.Add(analyst.Script.Fields[analyst.Script.Fields.Length - 1].Name);
            }

            //Save configuration to be used later.
            analyst.Save(new FileInfo(path + @"\normalizationData"+ dataFile.Replace(".csv", "") + ".ega"));

            Output.Text += "Loading File: " + dataFile + ". . .\n";
        }

        /// <summary> need comments </summary>
        private void networkBtn_Click(object sender, EventArgs e)
        {
            if (dataFile == null)
            {
                MessageBox.Show("You must select a file first.", "File Access Error");
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
            data info = new data(); info = info.returnInfo(path + dataFile.Replace(".csv", "Normal.csv"), outputTiltes, sampleBar.Value, validation);

            //Load analyst from earlier.
            var analyst = new EncogAnalyst();
            analyst.Load(new FileInfo(path + @"\normalizationData" + dataFile.Replace(".csv", "") + ".ega"));

            var sourcefile = new FileInfo(path + dataFile);

            var norm = new AnalystNormalizeCSV();
            norm.Analyze(sourcefile, true, CSVFormat.English, analyst);

            if (radBtnEncog.Checked)
            {
                if (!deepNetworkBox.Checked)
                {
                    Output.Text += ("\n@Encog:\n\n");
                    EncogNetwork encogNetwork = new EncogNetwork();
                    encogNetwork.Create(info.InputNumber, info.OutputNumber);

                    if (validation)
                    {
                        int poolSize = (info.InputData.Length * sampleBar.Value) / 10;

                        double[][] arrayIn = info.InputData; double[][] arrayOut = info.OutputData;

                        info.InputData = data.CreateArray<double>(poolSize, info.InputData[0].Length);
                        info.OutputData = data.CreateArray<double>(poolSize, info.OutputData[0].Length);

                        Random rnd = new Random();

                        int[] index = new int[arrayIn.Length - 1];

                        for (int j = 0; j < info.InputData.Length; j++)
                        {
                            index[j] = rnd.Next(0, arrayIn.Length);
                            info.InputData[j] = arrayIn[index[j]]; info.OutputData[j] = arrayOut[index[j]];
                        }

                        arrayIn = data.RemoveFromArray(arrayIn, index, poolSize);
                        arrayOut = data.RemoveFromArray(arrayOut, index, poolSize);

                        for (int i = 0; i < arrayIn.Length / poolSize; i++)
                        {
                            info.InputDataSample = data.CreateArray<double>(poolSize, arrayIn[0].Length);
                            info.OutputDataSample = data.CreateArray<double>(poolSize, arrayOut[0].Length);

                            for (int j = 0; j < info.InputDataSample.Length; j++)
                            {
                                index[j] = rnd.Next(0, arrayIn.Length);
                                info.InputDataSample[j] = arrayIn[index[j]]; info.OutputDataSample[j] = arrayOut[index[j]];
                            }

                            arrayIn = data.RemoveFromArray(arrayIn, index, poolSize);
                            arrayOut = data.RemoveFromArray(arrayOut, index, poolSize);

                            Output.Text += "Training complete with an inaccuracy of: " + Math.Round(encogNetwork.Train(info, ((float)(learningRateBar.Value) / 10), ((float)(momentumBar.Value) / 10)), 10) + "\n\n";

                            double[][] answers = data.CreateArray<double>(poolSize, info.InputData[0].Length);

                            for (int j = 0; j < answers.Length; j++)
                            {
                                encogNetwork.network.Compute(info.InputDataSample[j], answers[j]);
                            }

                            Output.Text += encogNetwork.display(answers, info, analyst);
                        }
                    }
                    else
                    {
                        Output.Text += "Training complete with an inaccuracy of: " + Math.Round(encogNetwork.Train(info, ((float)(learningRateBar.Value) / 10), ((float)(momentumBar.Value) / 10)), 5) + "\n\n";

                        double[][] answers = data.CreateArray<double>(info.InputDataSample.Length, info.InputDataSample[0].Length);

                        for (int i = 0; i < answers.Length; i++)
                        {
                            encogNetwork.network.Compute(info.InputDataSample[i], answers[i]);
                        }

                        Output.Text += encogNetwork.display(answers, info, analyst);
                    }

                    
                }
                else
                {
                    Output.Text += ("\n@Deep Encog:\n\n");
                    EncogDeepNetwork encogNetwork = new EncogDeepNetwork();
                    encogNetwork.Create(info.InputNumber, info.OutputNumber);

                    if (validation)
                    {
                        int poolSize = (info.InputData.Length * sampleBar.Value) / 10;

                        double[][] arrayIn = info.InputData; double[][] arrayOut = info.OutputData;

                        info.InputData = data.CreateArray<double>(poolSize, info.InputData[0].Length);
                        info.OutputData = data.CreateArray<double>(poolSize, info.OutputData[0].Length);

                        Random rnd = new Random();

                        int[] index = new int[arrayIn.Length - 1];

                        for (int j = 0; j < info.InputData.Length; j++)
                        {
                            index[j] = rnd.Next(0, arrayIn.Length);
                            info.InputData[j] = arrayIn[index[j]]; info.OutputData[j] = arrayOut[index[j]];
                        }

                        arrayIn = data.RemoveFromArray(arrayIn, index, poolSize);
                        arrayOut = data.RemoveFromArray(arrayOut, index, poolSize);

                        for (int i = 0; i < arrayIn.Length / poolSize; i++)
                        {
                            info.InputDataSample = data.CreateArray<double>(poolSize, arrayIn[0].Length);
                            info.OutputDataSample = data.CreateArray<double>(poolSize, arrayOut[0].Length);

                            for (int j = 0; j < info.InputDataSample.Length; j++)
                            {
                                index[j] = rnd.Next(0, arrayIn.Length);
                                info.InputDataSample[j] = arrayIn[index[j]]; info.OutputDataSample[j] = arrayOut[index[j]];
                            }

                            arrayIn = data.RemoveFromArray(arrayIn, index, poolSize);
                            arrayOut = data.RemoveFromArray(arrayOut, index, poolSize);

                            Output.Text += "Training complete with an inaccuracy of: " + Math.Round(encogNetwork.Train(info, ((float)(learningRateBar.Value) / 10), ((float)(momentumBar.Value) / 10)), 10) + "\n\n";

                            double[][] answers = data.CreateArray<double>(poolSize, info.InputData[0].Length);

                            for (int j = 0; j < answers.Length; j++)
                            {
                                encogNetwork.network.Compute(info.InputDataSample[j], answers[j]);
                            }

                            Output.Text += encogNetwork.display(answers, info, analyst);
                        }
                    }
                    else
                    {
                        Output.Text += "Training complete with an inaccuracy of: " + Math.Round(encogNetwork.Train(info, ((float)(learningRateBar.Value) / 10), ((float)(momentumBar.Value) / 10)), 5) + "\n\n";

                        double[][] answers = data.CreateArray<double>(info.InputDataSample.Length, info.InputDataSample[0].Length);

                        for (int i = 0; i < answers.Length; i++)
                        {
                            encogNetwork.network.Compute(info.InputDataSample[i], answers[i]);
                        }

                        Output.Text += encogNetwork.display(answers, info, analyst);
                    }
                }
            }
            else if(radBtnAccord.Checked)
            {
                if (!deepNetworkBox.Checked)
                {
                    Output.Text += ("\n@Accord:\n\n");
                    AccordNetwork accordNetwork = new AccordNetwork();
                    accordNetwork.Create(info.InputNumber, info.OutputNumber);

                    if (validation)
                    {
                        int poolSize = (info.InputData.Length * sampleBar.Value) / 10;

                        double[][] arrayIn = info.InputData; double[][] arrayOut = info.OutputData;

                        info.InputData = data.CreateArray<double>(poolSize, info.InputData[0].Length);
                        info.OutputData = data.CreateArray<double>(poolSize, info.OutputData[0].Length);

                        Random rnd = new Random();

                        int[] index = new int[arrayIn.Length - 1];

                        for (int j = 0; j < info.InputData.Length; j++)
                        {
                            index[j] = rnd.Next(0, arrayIn.Length);
                            info.InputData[j] = arrayIn[index[j]]; info.OutputData[j] = arrayOut[index[j]];
                        }

                        arrayIn = data.RemoveFromArray(arrayIn, index, poolSize);
                        arrayOut = data.RemoveFromArray(arrayOut, index, poolSize);

                        for (int i = 0; i < arrayIn.Length / poolSize; i++)
                        {
                            info.InputDataSample = data.CreateArray<double>(poolSize, arrayIn[0].Length);
                            info.OutputDataSample = data.CreateArray<double>(poolSize, arrayOut[0].Length);

                            for (int j = 0; j < info.InputDataSample.Length; j++)
                            {
                                index[j] = rnd.Next(0, arrayIn.Length);
                                info.InputDataSample[j] = arrayIn[index[j]]; info.OutputDataSample[j] = arrayOut[index[j]];
                            }

                            arrayIn = data.RemoveFromArray(arrayIn, index, poolSize);
                            arrayOut = data.RemoveFromArray(arrayOut, index, poolSize);
                            
                            Output.Text += "Training complete with an inaccuracy of: " + Math.Round(accordNetwork.Train(info, ((float)(learningRateBar.Value) / 10), ((float)(momentumBar.Value) / 10)), 5) + "\n\n";

                            double[][] answers = info.InputDataSample.Apply(accordNetwork.network.Compute);

                            Output.Text += accordNetwork.display(answers, info, analyst);
                        }
                    }
                    else
                    {
                        Output.Text += "Training complete with an inaccuracy of: " + Math.Round(accordNetwork.Train(info, ((float)(learningRateBar.Value) / 10), ((float)(momentumBar.Value) / 10)), 5) + "\n\n";

                        double[][] answers = info.InputDataSample.Apply(accordNetwork.network.Compute);

                        Output.Text += accordNetwork.display(answers, info, analyst);
                    }
                }
                else
                {
                    Output.Text += ("\n@Deep Accord:\n\n");
                    AccordNetwork accordNetwork = new AccordNetwork();
                    accordNetwork.Create(info.InputNumber, info.OutputNumber);

                    if (validation)
                    {
                        int poolSize = (info.InputData.Length * sampleBar.Value) / 10;

                        double[][] arrayIn = info.InputData; double[][] arrayOut = info.OutputData;

                        info.InputData = data.CreateArray<double>(poolSize, info.InputData[0].Length);
                        info.OutputData = data.CreateArray<double>(poolSize, info.OutputData[0].Length);

                        Random rnd = new Random();

                        int[] index = new int[arrayIn.Length - 1];

                        for (int j = 0; j < info.InputData.Length; j++)
                        {
                            index[j] = rnd.Next(0, arrayIn.Length);
                            info.InputData[j] = arrayIn[index[j]]; info.OutputData[j] = arrayOut[index[j]];
                        }

                        arrayIn = data.RemoveFromArray(arrayIn, index, poolSize);
                        arrayOut = data.RemoveFromArray(arrayOut, index, poolSize);

                        for (int i = 0; i < arrayIn.Length / poolSize; i++)
                        {
                            info.InputDataSample = data.CreateArray<double>(poolSize, arrayIn[0].Length);
                            info.OutputDataSample = data.CreateArray<double>(poolSize, arrayOut[0].Length);

                            for (int j = 0; j < info.InputDataSample.Length; j++)
                            {
                                index[j] = rnd.Next(0, arrayIn.Length);
                                info.InputDataSample[j] = arrayIn[index[j]]; info.OutputDataSample[j] = arrayOut[index[j]];
                            }

                            arrayIn = data.RemoveFromArray(arrayIn, index, poolSize);
                            arrayOut = data.RemoveFromArray(arrayOut, index, poolSize);

                            Output.Text += "Training complete with an inaccuracy of: " + Math.Round(accordNetwork.Train(info, ((float)(learningRateBar.Value) / 10), ((float)(momentumBar.Value) / 10)), 5) + "\n\n";

                            double[][] answers = data.CreateArray<double>(info.InputDataSample.Length, info.InputDataSample[0].Length);

                            for (int j = 0; j < answers.Length; j++)
                            {
                                answers[j] = accordNetwork.network.Compute(info.InputDataSample[j]);
                            }

                            Output.Text += accordNetwork.display(answers, info, analyst);
                        }
                    }
                    else
                    {
                        Output.Text += "Training complete with an inaccuracy of: " + Math.Round(accordNetwork.Train(info, ((float)(learningRateBar.Value) / 10), ((float)(momentumBar.Value) / 10)), 5) + "\n\n";

                        double[][] answers = data.CreateArray<double>(info.InputDataSample.Length, info.InputDataSample[0].Length);

                        for (int i = 0; i < answers.Length; i++)
                        {
                            answers[i] = accordNetwork.network.Compute(info.InputDataSample[i]);
                        }

                        Output.Text += accordNetwork.display(answers, info, analyst);
                    }
                }
            }
        }

        /// <summary> need comments </summary>
        private void rateTestBtn_Click(object sender, EventArgs e)
        {
            if (dataFile == null || !dataFile.Contains(".csv"))
            {
                MessageBox.Show("You must select a file first.", "File Access Error");
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
                            data info = new data(); info = info.returnInfo(path + dataFile.Replace(".csv", "Normal.csv"), outputTiltes, sampleBar.Value, validation);

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
                            BackPropagationLearning teacher = new BackPropagationLearning(networkAccord);
                            teacher.LearningRate = lr; 
                            teacher.Momentum = m; 

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

        /// <summary> need comments </summary>
        private void advancedBtn_Click(object sender, EventArgs e)
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
        private void sampleBar_Scroll(object sender, EventArgs e)
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
        private void learningRateBar_Scroll(object sender, EventArgs e)
        {
            learningRateLbl.Text = String.Format("Learning Rate: " + ((float)(learningRateBar.Value) / 10).ToString());
        }

        /// <summary> need comments </summary>
        private void momentumBar_Scroll(object sender, EventArgs e)
        {
            momentumLbl.Text = String.Format("Momentum: " + ((float)(momentumBar.Value) / 10).ToString());
        }
        
        /// Radio buttons ///

        /// <summary> need comments </summary>
        private void radBtnSplit_CheckedChanged(object sender, EventArgs e)
        {
            if(radBtnSplit.Checked)
            {
                radBtnSplit.AutoCheck = false;
                radBtnCrossVal.Checked = false;
                sampleLbl.Text = String.Format("Sample Data: " + (sampleBar.Value * 10).ToString() + "%");
            }
            else
            {
                radBtnSplit.AutoCheck = true;
                radBtnCrossVal.AutoCheck = false;
                radBtnSplit.Checked = false;
            }
        }

        /// <summary> need comments </summary>
        private void radBtnCrossVal_CheckedChanged(object sender, EventArgs e)
        {
            if (radBtnCrossVal.Checked)
            {
                radBtnCrossVal.AutoCheck = false;
                radBtnSplit.Checked = false;
                sampleLbl.Text = String.Format("Pool Size: " + (sampleBar.Value * 10).ToString() + "%");
            }
            else
            {
                radBtnCrossVal.AutoCheck = true;
                radBtnSplit.AutoCheck = false;
                radBtnCrossVal.Checked = false;
            }
        }

        /// <summary> need comments </summary>
        private void radBtnEncog_CheckedChanged(object sender, EventArgs e)
        {
            if (radBtnEncog.Checked)
            {
                radBtnEncog.AutoCheck = false;
                radBtnAccord.Checked = false;
            }
            else
            {
                radBtnEncog.AutoCheck = true;
                radBtnAccord.AutoCheck = false;
                radBtnEncog.Checked = false;
            }
        }

        /// <summary> need comments </summary>
        private void radBtnAccord_CheckedChanged(object sender, EventArgs e)
        {
            if (radBtnAccord.Checked)
            {
                radBtnAccord.AutoCheck = false;
                radBtnEncog.Checked = false;
            }
            else
            {
                radBtnAccord.AutoCheck = true;
                radBtnEncog.AutoCheck = false;
                radBtnAccord.Checked = false;
            }
        }
    }
}