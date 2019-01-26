using Accord.Math;
using Accord.Neuro;
using Accord.Neuro.ActivationFunctions;
using Accord.Neuro.Learning;
using Accord.Neuro.Networks;
using System;

namespace ENP1
{
    class AccordDeepNetwork : NeuralNetwork
    {
        public DeepBeliefNetwork network;

        public override void Create(int input, int output)
        {
            IStochasticFunction function = new GaussianFunction();

            //Setup network
            network = new DeepBeliefNetwork(function, input, 20, output);

            new GaussianWeights(network, 0.1).Randomize();
            network.UpdateVisibleWeights();
        }

        public override double Train(data info, float lr, float mom)
        {
            //Setup trainer using backpropagation.
            DeepBeliefNetworkLearning teacher = new DeepBeliefNetworkLearning(network)
            {
                Algorithm = (h, v, i) => new ContrastiveDivergenceLearning(h, v)
                {
                    LearningRate = lr,
                    Momentum = mom,
                    Decay = 0.001,
                }
            };

            // Setup batches of input for learning.
            int batchCount = Math.Max(1, info.InputData.Length / 100);
            // Create mini-batches to speed learning.
            int[] groups = Accord.Statistics.Tools.RandomGroups(info.InputData.Length, batchCount);
            double[][][] batches = info.InputData.Subgroups(groups);
            // Learning data for the specified layer.
            double[][][] layerData;

            double unsupervisedError = 0.0;

            // Unsupervised learning on each hidden layer, except for the output.
            for (int layerIndex = 0; layerIndex < network.Machines.Count - 1; layerIndex++)
            {
                teacher.LayerIndex = layerIndex;
                layerData = teacher.GetLayerInput(batches);
                for (int i = 0; i < 10000; i++)
                {
                    unsupervisedError = teacher.RunEpoch(layerData) / info.InputData.Length;
                }
            }

            //Setup trainer using backpropagation.
            BackPropagationLearning teacher2 = new BackPropagationLearning(network);

            teacher2.LearningRate = lr;
            teacher2.Momentum = mom;

            double error = 0.0;

            // Run supervised learning.
            for (int i = 0; i < 100000; i++)
            {
                error = teacher2.RunEpoch(info.InputData, info.OutputData) / info.InputData.Length;
            }

            return error;
        }

        public override void Save()
        {
            throw new NotImplementedException();
        }
    }
}
