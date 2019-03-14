using Encog.Engine.Network.Activation;
using Encog.ML.Data;
using Encog.ML.Data.Basic;
using Encog.ML.Train;
using Encog.ML.Train.Strategy;
using Encog.Neural.Networks;
using Encog.Neural.Networks.Training;
using Encog.Neural.Networks.Training.Anneal;
using Encog.Neural.Networks.Training.Lma;
using Encog.Neural.Pattern;
using System;

namespace ENP1
{
    /// <summary>
    /// Neural Network class for the deep learning version of EncogNeuralNetwork.
    /// Inherits: Save and Load from EncogNeuralNetwork.
    /// </summary>
    class EncogDeepNeuralNetwork : EncogNeuralNetwork
    {
        /// <summary> Create new instance of neural network. Can only contain 1 hidden layer. </summary>
        public override void Create(int input, int layers, int neurons, int output)
        {
            //Setup network, parameters (Activation, bias, number of neurons).
            var elman = new ElmanPattern()
            {
                ActivationFunction = new ActivationSigmoid(),
                InputNeurons = input,
                OutputNeurons = output
            };
            
            elman.AddHiddenLayer(neurons);

            EncogNetwork = (BasicNetwork)elman.Generate();
        }

        /// <summary> Train neural network. </summary>
        public override double Train(Data info, float lr, float mom)
        {
            IMLDataSet data = new BasicMLDataSet(info.InputData, info.OutputData);

            //Train network on data set, parameters (Network, dataset, learning rate, momentum).
            ICalculateScore score = new TrainingSetScore(data);
            IMLTrain trainAlt = new NeuralSimulatedAnnealing(EncogNetwork, score, 10, 2, 1000);
            IMLTrain learner;

            learner = new LevenbergMarquardtTraining(EncogNetwork, data);
            
            learner.AddStrategy(new Greedy());
            learner.AddStrategy(new HybridStrategy(trainAlt));

            //Train network on data set.
            double lastError = double.PositiveInfinity;

            //Training loop while error is decreasing by 0.0000001 or more every 1000 iterations.
            do
            {
                if (learner.Error != 0)
                {
                    lastError = learner.Error;
                }

                //An iteration of this takes too long to run 1000 so 1 as ran instead.
                learner.Iteration();

            } while (lastError - learner.Error > 0.0000001);

            return learner.Error;
        }
    }
}
