﻿using Accord.Neuro;
using Accord.Neuro.ActivationFunctions;
using Accord.Neuro.Learning;
using Accord.Neuro.Networks;
using System;

namespace ENP1
{
    /// <summary>
    /// Neural Network class for the deep learning version of AccordNeuralNetwork.
    /// </summary>
    internal class AccordDeepNeuralNetwork : NeuralNetwork
    {
        /// <summary> Create new instance of neural network. </summary>
        public override void Create(int input, int layers, int neurons, int output)
        {
            //Neuron function.
            IStochasticFunction function = new GaussianFunction();

            //Setup network
            switch (layers)
            {
                case 1:
                    DeepAccordNetwork = new DeepBeliefNetwork(function, input, neurons, output); //Activation function, input, hidden, hidden, output.
                    break;

                case 2:
                    DeepAccordNetwork = new DeepBeliefNetwork(function, input, neurons, neurons, output); //Activation function, input, hidden, hidden, output.
                    break;

                case 3:
                    DeepAccordNetwork = new DeepBeliefNetwork(function, input, neurons, neurons, neurons, output); //Activation function, input, hidden, hidden, output.
                    break;

                case 4:
                    DeepAccordNetwork = new DeepBeliefNetwork(function, input, neurons, neurons, neurons, neurons, output); //Activation function, input, hidden, hidden, output.
                    break;

                case 5:
                    DeepAccordNetwork = new DeepBeliefNetwork(function, input, neurons, neurons, neurons, neurons, neurons, output); //Activation function, input, hidden, hidden, output.
                    break;
            }

            //Weights.
            new GaussianWeights(DeepAccordNetwork, 0.1).Randomize();
            DeepAccordNetwork.UpdateVisibleWeights();
        }

        /// <summary> Train neural network. </summary>
        public override double Train(Data info, float lr, float mom)
        {
            //Setup trainer using backpropagation.
            DeepBeliefNetworkLearning teacher = new DeepBeliefNetworkLearning(DeepAccordNetwork)
            {
                Algorithm = (h, v, _) => new ContrastiveDivergenceLearning(h, v)
                {
                    LearningRate = lr,
                    Momentum = mom,
                    Decay = 0.001,
                }
            };

            // Setup batches of input for learning.
            int batchCount = Math.Max(1, info.InputData.Length / 100);

            // Create mini-batches to speed learning.
            int[] groups = Accord.Statistics.Classes.Random(info.InputData.Length, batchCount);
            double[][][] batches = Accord.Statistics.Classes.Separate(info.InputData, groups);

            // Learning data for the specified layer.
            double[][][] layerData;

            double error = double.PositiveInfinity;
            double lastError;

            // Unsupervised learning on each hidden layer, except for the output.
            for (int layerIndex = 0; layerIndex < DeepAccordNetwork.Machines.Count - 1; layerIndex++)
            {
                teacher.LayerIndex = layerIndex;
                layerData = teacher.GetLayerInput(batches);

                do
                {
                    lastError = error;
                    int i = 0;

                    while (i < 100)
                    {
                        error = teacher.RunEpoch(layerData);
                        i++;
                    }

                } while (lastError - error > 0.0000001);
            }

            //Setup trainer using backpropagation.
            BackPropagationLearning teacher2 = new BackPropagationLearning(DeepAccordNetwork)
            {
                LearningRate = lr,
                Momentum = mom
            };

            //Supervised training.
            do
            {
                lastError = error;
                int i = 0;

                while (i < 1000)
                {
                    error = teacher2.RunEpoch(info.InputData, info.OutputData);
                    i++;
                }

            } while (lastError - error > 0.0000001);

            return error;
        }

        /// <summary> Save current neural network. </summary>
        public override void Save(string fileName)
        {
            DeepAccordNetwork.Save(fileName);
        }

        /// <summary> Load existing neural network. </summary>
        public override void Load(string fileName)
        {
            DeepAccordNetwork = (DeepBeliefNetwork)Network.Load(fileName);
        }
    }
}
