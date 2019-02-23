using Accord.Neuro;
using Accord.Neuro.Networks;
using Encog.App.Analyst;
using Encog.Neural.Networks;
using System;
using System.Collections.Generic;
using System.IO;

namespace ENP1
{
    /// <summary> Abstract NeuralNetwork class. </summary>
    internal abstract class NeuralNetwork
    {
        public BasicNetwork EncogNetwork;
        public DeepBeliefNetwork DeepAccordNetwork;
        public ActivationNetwork AccordNetwork;

        private BestNetwork BestMomentum(Data info, string path, int layers, int neurons, float lr)
        {
            bool complete = false;
            bool firstPass = true;
            bool increase = true;

            BestNetwork best = new BestNetwork
            {
                Momentum = 0.1F,
                LearningRate = lr,
                Neurons = neurons,
                Layers = layers
            };

            best.Error = Train(info, lr, best.Momentum);

            while (!complete)
            {
                if (firstPass)
                {
                    best.Momentum = 0.5F;
                }
                else if (increase)
                {
                    best.Momentum += 0.1F;
                }
                else
                {
                    best.Momentum -= 0.1F;
                }

                if(Math.Round(best.Momentum, 1) == 0.1F || Math.Round(best.Momentum, 1) == 1.0F)
                {
                    complete = true;
                }

                double newest = Train(info, lr, best.Momentum);

                if (firstPass)
                {
                    if (newest < best.Error)
                    {
                        best.Error = newest;
                        increase = true;
                    }
                    else if (newest == best.Error)
                    {
                        complete = true;
                    }
                    else
                    {
                        increase = false;
                    }

                    firstPass = false;
                }
                else
                {
                    if (newest < best.Error)
                    {
                        best.Error = newest;
                    }
                    else
                    {
                        complete = true;
                    }
                }
            }

            using (StreamWriter sw = new StreamWriter(path, true))
            {
                sw.WriteLine(String.Format(
                    "{0},{1},{2},{3},{4},{5}", best.Error, GetType().ToString().Replace("ENP1.", ""), layers, neurons, Math.Round(lr, 1), Math.Round(best.Momentum, 1))); //"ENP1" must change to reflect solution name (name.) if ever changed.
            }

            return best;
        }

        private BestNetwork BestLearningRate(Data info, string path, int layers, int neurons)
        {
            float lr = 0.1F;
            bool complete = false;
            bool firstPass = true;
            bool increase = true;

            BestNetwork best = BestMomentum(info, path, layers, neurons, lr);

            while (!complete)
            {
                if (firstPass)
                {
                    lr = 0.5F;
                }
                else if (increase)
                {
                    lr += 0.1F;
                }
                else
                {
                    lr -= 0.1F;
                }

                if (Math.Round(lr, 1) == 0.1F || Math.Round(lr, 1) == 1.0F)
                {
                    complete = true;
                }

                BestNetwork newest = BestMomentum(info, path, layers, neurons, lr);

                if (firstPass)
                {
                    if (newest.Error < best.Error)
                    {
                        best = newest;
                        increase = true;
                    }
                    else if (newest.Error == best.Error)
                    {
                        complete = true;
                    }
                    else
                    {
                        increase = false;
                    }

                    firstPass = false;
                }
                else
                {
                    if (newest.Error < best.Error)
                    {
                        best = newest;
                    }
                    else
                    {
                        complete = true;
                    }
                }
            }

            return best;
        }

        private BestNetwork BestNeurons(Data info, string path, int layers)
        {
            int neurons = 5;
            bool complete = false;
            bool firstPass = true;
            bool increase = true;

            Create(info.InputNumber, layers, neurons, info.OutputNumber);
            BestNetwork best = BestLearningRate(info, path, layers, neurons);

            while (!complete)
            {
                if (firstPass)
                {
                    neurons = 25;
                }
                else if (increase)
                {
                    neurons += 5;
                }
                else
                {
                    neurons -= 5;
                }

                if (neurons == 5 || neurons == 50)
                {
                    complete = true;
                }

                Create(info.InputNumber, layers, neurons, info.OutputNumber);
                BestNetwork newest = BestLearningRate(info, path, layers, neurons);

                if (firstPass)
                {
                    if (newest.Error < best.Error)
                    {
                        best = newest;
                        increase = true;
                    }
                    else if (newest.Error == best.Error)
                    {
                        complete = true;
                    }
                    else
                    {
                        increase = false;
                    }

                    firstPass = false;
                }
                else
                {
                    if (newest.Error < best.Error)
                    {
                        best = newest;
                    }
                    else
                    {
                        complete = true;
                    }
                }
            }

            return best;
        }

        public BestNetwork CalculateBest(Data info, string path)
        {
            int layers = 1;
            bool complete = false;
            bool firstPass = true;
            bool increase = true;
            
            BestNetwork best = BestNeurons(info, path, layers); ;

            if(GetType().ToString().Replace("ENP1.", "") == "EncogDeepNeuralNetwork")
            {
                return best;
            }

            while (!complete)
            {
                if (firstPass)
                {
                    layers = 2;
                }
                else if (increase)
                {
                    layers += 1;
                }
                else
                {
                    layers -= 1;
                }

                if (layers == 1 || layers == 5)
                {
                    complete = true;
                }

                BestNetwork newest = BestNeurons(info, path, layers);

                if (firstPass)
                {
                    if (newest.Error < best.Error)
                    {
                        best = newest;
                        increase = true;
                    }
                    else if (newest.Error == best.Error)
                    {
                        complete = true;
                    }
                    else
                    {
                        increase = false;
                    }

                    firstPass = false;
                }
                else
                {
                    if (newest.Error < best.Error)
                    {
                        best = newest;
                    }
                    else
                    {
                        complete = true;
                    }
                }
            }

            return best;
        }

        /// <summary> Creates a new neural network. </summary>
        public abstract void Create(int input, int layers, int neurons, int output);

		/// <summary> Trains current neural network. </summary>
        public abstract double Train(Data info, float lr, float mom);

		/// <summary> Returns string to display current neural network (used when correct output is know). </summary>
        public string Display(double[][] answers, EncogAnalyst analyst, Data info, List<string> titles, string path)
        {
            string item = "";

            List<int> count = new List<int>();

            for (int c = 0; c < titles.Count; c++)
            {
                count.Add(0);
            }

            //Get length of CSV, Inputs and Outputs.
            using (var reader = new StreamReader(path))
            {
                var line = reader.ReadLine();
                var values = line.Split(',');

                for (int v = 0; v < values.Length; v++)
                {
                    for (int k = 0; k < titles.Count; k++)
                    {
                        if (values[v].Contains(titles[k]))
                        {
                            count[k]++;
                        }
                    }
                }
            }

            for (int i = 0; i < info.OutputDataSample.Length; i++)
            {
                item += String.Format(
                        "Prediction Number: {0}\n\n",
                        i + 1
                );

                for (int j = 0; j < count.Count; j++)
                {
                    string prediction = analyst.Script.Normalize.NormalizedFields[analyst.Script.Normalize.NormalizedFields.Count + j - count.Count].Name + ": ";
                    string outpt = "";

                    if (count[j] == 1)
                    {
                        outpt += Math.Round(analyst.Script.Normalize.NormalizedFields[analyst.Script.Normalize.NormalizedFields.Count + j - count.Count].DeNormalize(info.OutputDataSample[i][j]), 1).ToString();
                        prediction += Math.Round(analyst.Script.Normalize.NormalizedFields[analyst.Script.Normalize.NormalizedFields.Count + j - count.Count].DeNormalize(answers[i][j]), 1).ToString();
                    }
                    else
                    {
                        double[] temp = new double[count[j]];

                        for (int c = 0; c < count[j]; c++)
                        {
                            temp[c] = info.OutputDataSample[i][c];
                        }

                        outpt += analyst.Script.Normalize.NormalizedFields[analyst.Script.Normalize.NormalizedFields.Count + j - count.Count].DetermineClass(temp).Name;

                        for (int c = 0; c < count[j]; c++)
                        {
                            temp[c] = answers[i][c];
                        }

                        prediction += analyst.Script.Normalize.NormalizedFields[analyst.Script.Normalize.NormalizedFields.Count + j - count.Count].DetermineClass(temp).Name;
                    }

                    item += String.Format(
                        "Predicted {0} Correct: {1}\n\n",
                        prediction, outpt
                    );
                }
            }

            return item;
        }

		/// <summary> Returns string to display current neural network. </summary>
        public string Display(double[][] answers, EncogAnalyst analyst, List<string> titles, string path)
        {
            string item = "";

            List<int> count = new List<int>();

            for (int c = 0; c < titles.Count; c++)
            {
                count.Add(0);
            }

            //Get length of CSV, Inputs and Outputs.
            using (var reader = new StreamReader(path))
            {
                var line = reader.ReadLine();
                var values = line.Split(',');

                for (int v = 0; v < values.Length; v++)
                {
                    for (int k = 0; k < titles.Count; k++)
                    {
                        if (values[v].Contains(titles[k]))
                        {
                            count[k]++;
                        }
                    }
                }
            }

            for (int i = 0; i < answers.Length; i++)
            {
                item += String.Format(
                        "Prediction Number: {0}\n\n",
                        i + 1
                );

                for (int j = 0; j < count.Count; j++)
                {
                    string prediction = analyst.Script.Normalize.NormalizedFields[analyst.Script.Normalize.NormalizedFields.Count + j - count.Count].Name + ": ";

                    if (count[j] == 1)
                    {
                        prediction += Math.Round(analyst.Script.Normalize.NormalizedFields[analyst.Script.Normalize.NormalizedFields.Count + j - count.Count].DeNormalize(answers[i][j]), 1).ToString();
                    }
                    else
                    {
                        double[] temp = new double[count[j]];

                        for (int c = 0; c < count[j]; c++)
                        {
                            temp[c] = answers[i][c];
                        }

                        prediction += analyst.Script.Normalize.NormalizedFields[analyst.Script.Normalize.NormalizedFields.Count + j - count.Count].DetermineClass(temp).Name;
                    }

                    item += String.Format(
                        "Predicted {0}\n\n",
                        prediction
                    );
                }
            }

            return item;
        }

        /// <summary> Saves current neural network to a file. </summary>
        public abstract void Save(string fileName);

        public abstract void Load(string fileName);
    }
}
