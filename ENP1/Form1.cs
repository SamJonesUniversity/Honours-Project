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
using Accord.MachineLearning;
using Accord.Neuro.Networks;
using Accord.Neuro.Learning;
using Accord.Math;

namespace ENP1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //Global path variable for working directory.
        public string path;
        public string dataFile;
        List<string> outputTiltes = new List<string>();

        //On form load.
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void fileBtn_Click(object sender, EventArgs e)
        {

            //Get csv path and create normalized version path.
            DialogResult dialogResult = openFileDialog1.ShowDialog();
            dataFile = openFileDialog1.SafeFileName;
            path = openFileDialog1.FileName.Replace(openFileDialog1.SafeFileName, "");
            var sourcefile = new FileInfo(openFileDialog1.FileName);
            var normalfile = new FileInfo(openFileDialog1.FileName.Replace(".csv", "Normal.csv"));

            //Setup analyst with orignal csv.
            var analyst = new EncogAnalyst();
            var wizard = new AnalystWizard(analyst);
            wizard.Wizard(sourcefile, true, AnalystFileFormat.DecpntComma);

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
        }

        //ANN using Engoc library.
        private void engocBtn_Click(object sender, EventArgs e)
        {
            if (dataFile == null || !dataFile.Contains(".csv"))
            {
                MessageBox.Show("You must select a file first, if you have it is not the correct format. The file must be .csv", "File Access Error");
                return;
            }

            //Setup training dataset.
            data info = new data(); info = info.return_info(path + dataFile.Replace(".csv", "Normal.csv"), outputTiltes, sampleBar.Value);

            IMLDataSet data = new BasicMLDataSet(info.InputData, info.OutputData);
            IMLDataSet sampleData = new BasicMLDataSet(info.InputDataSample, info.OutputDataSample);

            //Setup network, parameters (Activation, bias, number of neurons).
            BasicNetwork network = new BasicNetwork();
            network.AddLayer(new BasicLayer(null, true, info.InputNumber)); //Input.
            network.AddLayer(new BasicLayer(new ActivationSigmoid(), true, 10)); //Hidden.
            network.AddLayer(new BasicLayer(new ActivationSigmoid(), true, 10)); //Hidden.
            network.AddLayer(new BasicLayer(new ActivationSigmoid(), false, info.OutputNumber)); //Output.
            network.Structure.FinalizeStructure();
            network.Reset();

            //Train network on data set, parameters (Network, dataset, learning rate, momentum).
            IMLTrain learner = new Backpropagation(network, data, ((float)(learningRateBar.Value) / 10), ((float)(momentumBar.Value) / 10));

            //Recording time per tick.
            DateTime start = DateTime.Now;
            DateTime end;

            do
            {
                learner.Iteration();
                end = DateTime.Now;

            } while (((end.Minute * 60) + end.Second) - ((start.Minute * 60) + start.Second) < 10);

            //Load analyst from earlier.
            var analyst = new EncogAnalyst();
            analyst.Load(new FileInfo(path + @"\normalizationData" + dataFile.Replace(".csv", "") + ".ega"));

            var sourcefile = new FileInfo(path + dataFile);
            
            var norm = new AnalystNormalizeCSV();
            norm.Analyze(sourcefile, true, CSVFormat.English, analyst);

            Flowers.Items.Add("----------------------------------------------------------Encog----------------------------------------------------------\n");

            foreach (BasicMLDataPair pair in sampleData)
            {
                IMLData result = network.Compute(pair.Input);

                string item = "";
                
                /*for (int j = 0; j < analyst.Script.Normalize.NormalizedFields.Count; j++)
                {
                    item += String.Format(
                        "Input {0}: [{1}] ", j + 1,
                        Math.Round(analyst.Script.Normalize.NormalizedFields[j].DeNormalize(pair.Input[j]), 2)
                    );
                }*/

                for (int j = 0; j < info.OutputDataSample[0].Length; j++)
                {
                    var outpt = analyst.Script.Normalize.NormalizedFields[analyst.Script.Normalize.NormalizedFields.Count - 1].DeNormalize(pair.Ideal[j]); //??????????????????????
                    var prediction = analyst.Script.Normalize.NormalizedFields[analyst.Script.Normalize.NormalizedFields.Count - 1].DeNormalize(result[j]);

                    item += String.Format(
                        "Predicted Output: [{0}] Correct Output: {1}\n Error = [{2}]",
                        prediction, outpt, learner.Error
                    );
                }

                Flowers.Items.Add(item);
            }

            Flowers.Items.Add("\n");
        }

        //ANN using Accord.NET library.
        private void accordBtn_Click(object sender, EventArgs e)
        {
            if (dataFile == null || !dataFile.Contains(".csv"))
            {
                MessageBox.Show("You must select a file first, if you have it is not the correct format. The file must be .csv", "File Access Error");
                return;
            }

            // initialize input and output values.
            data info = new data(); info = info.return_info(path + dataFile.Replace(".csv", "Normal.csv"), outputTiltes, sampleBar.Value);

            //Setup network
            Accord.Neuro.IActivationFunction function = new SigmoidFunction(); 
            ActivationNetwork network = new ActivationNetwork(function, info.InputNumber, 5, info.OutputNumber); //Activation function, input, hidden, hidden, output.
            //DeepBeliefNetwork network = new DeepBeliefNetwork(info.InputNumber, 10, 2);

            //Setup trainer using backpropagation.
            BackPropagationLearning teacher = new BackPropagationLearning(network);
            //DeepNeuralNetworkLearning teacher = new DeepNeuralNetworkLearning(network);
            //ActivationNetworkLearningConfigurationFunction algorit = new ActivationNetworkLearningConfigurationFunction(network, 1);
            //teacher.Algorithm = function;
            //teacher.LayerCount = 2;
            teacher.LearningRate = ((float)(learningRateBar.Value) / 10);
            teacher.Momentum = ((float)(momentumBar.Value) / 10);

            //Train network on data set.
            double error = double.PositiveInfinity;

            //Recording time per tick.
            DateTime start = DateTime.Now;
            DateTime end;

            do
            {
                error = teacher.RunEpoch(info.InputData, info.OutputData);
                end = DateTime.Now;

            } while (((end.Minute * 60) + end.Second) - ((start.Minute * 60) + start.Second) < 10);

            //Load analyst from earlier.
            var analyst = new EncogAnalyst();
            analyst.Load(new FileInfo(path + @"\normalizationData" + dataFile.Replace(".csv", "") + ".ega"));

            var sourcefile = new FileInfo(path + dataFile);

            var norm = new AnalystNormalizeCSV();
            norm.Analyze(sourcefile, true, CSVFormat.English, analyst);

            //Predict outputs.
            double[][] answers = info.InputDataSample.Apply(network.Compute);

            Flowers.Items.Add("----------------------------------------------------------Accord.NET----------------------------------------------------------\n");

            for (int i = 0; i < answers.Length; i++)
            {
                string item = "";

                /*for (int j = 0; j < info.InputDataSample[0].Length; j++)
                {
                    double input = Math.Round(analyst.Script.Normalize.NormalizedFields[count].DeNormalize(info.InputDataSample[i][j]), 2);

                    if (input > 0)
                    {
                        item += String.Format(
                        "Input {0}: [{1}] ", j + 1,
                        input
                        );

                        count++;
                    }
                }*/

                for (int j = 0; j < answers[i].Length; j++)
                {
                    var outpt = analyst.Script.Normalize.NormalizedFields[analyst.Script.Normalize.NormalizedFields.Count - 1].DeNormalize(info.OutputDataSample[i][j]); //??????????????????????
                    var prediction = analyst.Script.Normalize.NormalizedFields[analyst.Script.Normalize.NormalizedFields.Count - 1].DeNormalize(answers[i][j]);
                   
                    item += String.Format(
                        "Predicted Output: [{0}] = [{1}]\n Error = [{2}]",
                        prediction, outpt, error
                    );
                }

                Flowers.Items.Add(item);
            }

            Flowers.Items.Add("\n");
        }

        private void sampleBar_Scroll(object sender, EventArgs e)
        {
            sampleLbl.Text = String.Format((sampleBar.Value * 10).ToString() + "%");
        }

        private void learningRateBar_Scroll(object sender, EventArgs e)
        {
            learningRateLbl.Text = String.Format(((float)(learningRateBar.Value) / 10).ToString());
        }

        private void momentumBar_Scroll(object sender, EventArgs e)
        {
            momentumLbl.Text = String.Format(((float)(momentumBar.Value) / 10).ToString());
        }

        private void rateTestBtn_Click(object sender, EventArgs e)
        {
            if (dataFile == null || !dataFile.Contains(".csv"))
            {
                MessageBox.Show("You must select a file first, if you have it is not the correct format. The file must be .csv", "File Access Error");
                return;
            }

            string test = path + "Results.csv";

            using (StreamWriter sw = new StreamWriter(test))
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
                            data infoAccord = new data(); infoAccord = infoAccord.return_info(path + dataFile.Replace(".csv", "Normal.csv"), outputTiltes, sampleBar.Value);

                            //Setup network
                            Accord.Neuro.IActivationFunction function = new SigmoidFunction();
                            ActivationNetwork networkAccord = new ActivationNetwork(function, infoAccord.InputNumber, neurons, infoAccord.OutputNumber);

                            switch (layers)
                            {
                                case 2:
                                    networkAccord = new ActivationNetwork(function, infoAccord.InputNumber, neurons, neurons, infoAccord.OutputNumber); //Activation function, input, hidden, hidden, output.
                                    break;

                                case 3:
                                    networkAccord = new ActivationNetwork(function, infoAccord.InputNumber, neurons, neurons, neurons, infoAccord.OutputNumber); //Activation function, input, hidden, hidden, output.
                                    break;

                                case 4:
                                    networkAccord = new ActivationNetwork(function, infoAccord.InputNumber, neurons, neurons, neurons, neurons, infoAccord.OutputNumber); //Activation function, input, hidden, hidden, output.
                                    break;

                                case 5:
                                    networkAccord = new ActivationNetwork(function, infoAccord.InputNumber, neurons, neurons, neurons, neurons, neurons, infoAccord.OutputNumber); //Activation function, input, hidden, hidden, output.
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
                                error = teacher.RunEpoch(infoAccord.InputData, infoAccord.OutputData);
                                end = DateTime.Now;

                            } while ((((end.Hour * 60 * 60) + end.Minute * 60) + end.Second) - (((start.Hour * 60 * 60) + start.Minute * 60) + start.Second) < 1);

                            //Setup training dataset.
                            data infoEncog = new data(); infoEncog = infoEncog.return_info(path + dataFile.Replace(".csv", "Normal.csv"), outputTiltes, sampleBar.Value);

                            IMLDataSet data = new BasicMLDataSet(infoEncog.InputData, infoEncog.OutputData);
                            IMLDataSet sampleData = new BasicMLDataSet(infoEncog.InputDataSample, infoEncog.OutputDataSample);

                            //Setup network, parameters (Activation, bias, number of neurons).
                            BasicNetwork networkEncog = new BasicNetwork();
                            networkEncog.AddLayer(new BasicLayer(null, true, infoEncog.InputNumber)); //Input.

                            for(int i = 0; i < layers; i++)
                            {
                                networkEncog.AddLayer(new BasicLayer(new ActivationSigmoid(), true, neurons)); //Hidden.
                            }

                            networkEncog.AddLayer(new BasicLayer(new ActivationSigmoid(), false, infoEncog.OutputNumber)); //Output.
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
    }
}