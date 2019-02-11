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

        /// <summary> Creates a new neural network. </summary>
        public abstract void Create(int input, int output);

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

                item += String.Format(
                        "Prediction Number: {0}\n\n",
                        i + 1
                );

                for (int j = 0; j < count.Count; j++)
                {
                    string prediction = "";
                    string outpt = "";
                    if (count[j] == 1)
                    {
                        outpt = Math.Round(analyst.Script.Normalize.NormalizedFields[analyst.Script.Normalize.NormalizedFields.Count + j - count.Count].DeNormalize(info.OutputDataSample[i][j]), 1).ToString();
                        prediction = Math.Round(analyst.Script.Normalize.NormalizedFields[analyst.Script.Normalize.NormalizedFields.Count + j - count.Count].DeNormalize(answers[i][j]), 1).ToString();
                    }
                    else
                    {
                        double[] temp = new double[count[j]];

                        for (int c = 0; c < count[j]; c++)
                        {
                            temp[c] = info.OutputDataSample[i][c];
                        }

                        outpt = analyst.Script.Normalize.NormalizedFields[analyst.Script.Normalize.NormalizedFields.Count + j - count.Count].DetermineClass(temp).Name;

                        for (int c = 0; c < count[j]; c++)
                        {
                            temp[c] = answers[i][c];
                        }

                        prediction = analyst.Script.Normalize.NormalizedFields[analyst.Script.Normalize.NormalizedFields.Count + j - count.Count].DetermineClass(temp).Name;
                    }

                    item += String.Format(
                        "Predicted Output: {0} Correct Output: {1}\n\n",
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
                    string prediction = "";

                    if (count[j] == 1)
                    {
                        prediction = Math.Round(analyst.Script.Normalize.NormalizedFields[analyst.Script.Normalize.NormalizedFields.Count + j - count.Count].DeNormalize(answers[i][j]), 1).ToString();
                    }
                    else
                    {
                        double[] temp = new double[count[j]];

                        for (int c = 0; c < count[j]; c++)
                        {
                            temp[c] = answers[i][c];
                        }

                        prediction = analyst.Script.Normalize.NormalizedFields[analyst.Script.Normalize.NormalizedFields.Count + j - count.Count].DetermineClass(temp).Name;
                    }

                    item += String.Format(
                        "Predicted Output: {0}\n\n",
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
