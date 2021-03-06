﻿using Accord.Neuro;
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
        //Setup all types of network.
        public BasicNetwork EncogNetwork;
        public DeepBeliefNetwork DeepAccordNetwork;
        public ActivationNetwork AccordNetwork;

        //Context switch 20ns overhead.
        static readonly object Locker = new object();

        //Calculate best momentum for network.
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

            //Create network again to refresh neurons and weights.
            Create(info.InputNumber, layers, neurons, info.OutputNumber);

            //Train network.
            best.Error = Train(info, lr, best.Momentum);

            //Seperate momentum variable so objects momentum is only updated when error is lower.
            float mom = 0.1F;

            //While momentum has not reached and edge case (0.1 or 1.0).
            while (!complete)
            {
                //If first time set to middle for comand and conquer.
                if (firstPass)
                {
                    mom = 0.5F;
                }
                else if (increase)
                {
                    mom += 0.1F;
                }
                else
                {
                    mom -= 0.1F;
                }

                //Rounding as float sometimes shows 0.X...1 instead of 0.X.
                mom = (float)Math.Round(mom, 1);

                if (mom == 0.1F || mom == 1.0F)
                {
                    complete = true;
                }

                double[] mean = new double[2];

                //Create and train the network X times.
                for(int i = 0; i < mean.Length; i++)
                {
                    Create(info.InputNumber, layers, neurons, info.OutputNumber);
                    mean[i] = Train(info, lr, mom);
                }

                double newest = 0;

                //Collate all training into on value.
                for (int i = 0; i < mean.Length; i++)
                {
                    newest += mean[i];
                }

                //Get mean of training.
                newest /= mean.Length;

                if (firstPass)
                {
                    if (newest < best.Error)
                    {
                        //Set new best error.
                        best.Error = newest;
                        best.Momentum = mom;

                        //Set to increase momentum.
                        increase = true;
                    }
                    else if (newest == best.Error)
                    {
                        complete = true;
                    }
                    else
                    {
                        //Set to decrease momentum.
                        increase = false;
                    }

                    //No longer first round.
                    firstPass = false;
                }
                else
                {
                    //
                    if (newest < best.Error)
                    {
                        best.Error = newest;
                        best.Momentum = mom;
                    }
                    else
                    {
                        complete = true;
                    }
                }
            }

            //Lock to prevent data race.
            lock (Locker)
            {
                using (StreamWriter sw = new StreamWriter(path, true))
                {
                    sw.WriteLine(String.Format(
                        "{0},{1},{2},{3},{4},{5}", best.Error, GetType().ToString().Replace("ENP1.", ""), layers, neurons, Math.Round(lr, 1), Math.Round(best.Momentum, 1))); //"ENP1" must change to reflect solution name (name.) if ever changed.
                }
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

                lr = (float)Math.Round(lr, 1);

                if (lr == 0.1F || lr == 1.0F)
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

            BestNetwork best = BestNeurons(info, path, layers);

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

#if DEBUG
            List<string> printer = new List<string>();
#endif

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
                        outpt += Math.Round(analyst.Script.Normalize.NormalizedFields[analyst.Script.Normalize.NormalizedFields.Count + j - count.Count].DeNormalize(info.OutputDataSample[i][j]), 5).ToString();
                        prediction += Math.Round(analyst.Script.Normalize.NormalizedFields[analyst.Script.Normalize.NormalizedFields.Count + j - count.Count].DeNormalize(answers[i][j]), 5).ToString();

#if DEBUG
                        printer.Add(analyst.Script.Normalize.NormalizedFields[analyst.Script.Normalize.NormalizedFields.Count + j - count.Count].DeNormalize(info.OutputDataSample[i][j]).ToString() + ",");
                        printer.Add(analyst.Script.Normalize.NormalizedFields[analyst.Script.Normalize.NormalizedFields.Count + j - count.Count].DeNormalize(answers[i][j]).ToString() + "\n");
#endif
                    }
                    else
                    {
                        double[] temp = new double[count[j]];

                        for (int c = 0; c < count[j]; c++)
                        {
                            temp[c] = info.OutputDataSample[i][c];
                        }

                        outpt += analyst.Script.Normalize.NormalizedFields[analyst.Script.Normalize.NormalizedFields.Count + j - count.Count].DetermineClass(temp).Name;

#if DEBUG
                        printer.Add(analyst.Script.Normalize.NormalizedFields[analyst.Script.Normalize.NormalizedFields.Count + j - count.Count].DetermineClass(temp).Name + ",");
#endif

                        for (int c = 0; c < count[j]; c++)
                        {
                            temp[c] = answers[i][c];
                        }

                        prediction += analyst.Script.Normalize.NormalizedFields[analyst.Script.Normalize.NormalizedFields.Count + j - count.Count].DetermineClass(temp).Name;

#if DEBUG
                        printer.Add(analyst.Script.Normalize.NormalizedFields[analyst.Script.Normalize.NormalizedFields.Count + j - count.Count].DetermineClass(temp).Name + "\n");
#endif
                    }

                        item += String.Format(
                        "Predicted {0} Correct: {1}\n\n",
                        prediction, outpt
                    );
                }
            }

#if DEBUG
            using (var sw = new StreamWriter(path.Replace(".csv", "cross-fold-debug-out.csv"), true))
            {
                foreach (string s in printer)
                {
                    sw.Write(s);
                }
            }
#endif

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
                        prediction += Math.Round(analyst.Script.Normalize.NormalizedFields[analyst.Script.Normalize.NormalizedFields.Count + j - count.Count].DeNormalize(answers[i][j]), 5).ToString();
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
