using Encog.App.Analyst;
using System;

namespace ENP1
{
    /// <summary> Abstract NeuralNetwork class. </summary>
    abstract class NeuralNetwork
    {
        /// <summary> Creates a new neural network. </summary>
        public abstract void Create(int input, int output);

        public abstract double Train(data info, float lr, float mom);

        public string display(double[][] answers, data info, EncogAnalyst analyst)
        {
            string item = "";

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

                for (int j = 0; j < info.OutputDataSample[i].Length; j++)
                {
                    var outpt = analyst.Script.Normalize.NormalizedFields[analyst.Script.Normalize.NormalizedFields.Count - 1].DeNormalize(info.OutputDataSample[i][j]);
                    var prediction = analyst.Script.Normalize.NormalizedFields[analyst.Script.Normalize.NormalizedFields.Count - 1].DeNormalize(answers[i][j]);

                    item += String.Format(
                        "Predicted Output: {0} Correct Output: {1}\n\n",
                        Math.Round(prediction, 1), outpt
                    );
                }
            }

            return item;
        }

        /// <summary> Saves current neural network to a file. </summary>
        public abstract void Save();
    }
}
