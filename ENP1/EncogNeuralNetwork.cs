using Accord.Neuro;
using Encog.Engine.Network.Activation;
using Encog.ML.Data;
using Encog.ML.Data.Basic;
using Encog.ML.Train;
using Encog.Neural.Networks;
using Encog.Neural.Networks.Layers;
using Encog.Neural.Networks.Training.Propagation.Back;
using System;
using System.IO;

namespace ENP1
{
    internal class EncogNeuralNetwork : NeuralNetwork
    {
        public override void Create(int input, int layers, int neurons, int output)
        {
            //Setup network, parameters (Activation, bias, number of neurons).
            EncogNetwork = new BasicNetwork();
            EncogNetwork.AddLayer(new BasicLayer(null, true, input)); //Input.

            for (int i = 0; i < layers; i++)
            {
                EncogNetwork.AddLayer(new BasicLayer(new ActivationSigmoid(), true, neurons)); //Hidden.
            }

            EncogNetwork.AddLayer(new BasicLayer(new ActivationSigmoid(), false, output)); //Output.
            EncogNetwork.Structure.FinalizeStructure();
            EncogNetwork.Reset();
        }

        public override double Train(Data info, float lr, float mom)
        {
            IMLDataSet data = new BasicMLDataSet(info.InputData, info.OutputData);

            //Train network on data set, parameters (Network, dataset, learning rate, momentum).
            IMLTrain learner = new Backpropagation(EncogNetwork, data, lr, mom);
            double lastError = double.PositiveInfinity;
            
            //Training loop while error is decreasing by 0.0000001 or more every 1000 iterations.
            do
            {
                //Set last error as error if the network has trained before.
                if (learner.Error != 0)
                {
                    lastError = learner.Error;
                }

                //Do 1000 learning iterations.
                int i = 0;
                while (i < 1000)
                {
                    learner.Iteration();
                    i++;
                }

            } while (lastError - learner.Error > 0.0000001);

            return learner.Error;
        }

        public override void Save(string fileName)
        {
            FileInfo networkFile = new FileInfo(fileName);
            Encog.Persist.EncogDirectoryPersistence.SaveObject(networkFile, EncogNetwork);
        }

		public override void Load(string fileName)
        {
            FileInfo networkFile = new FileInfo(fileName);
            EncogNetwork = (BasicNetwork) Encog.Persist.EncogDirectoryPersistence.LoadObject(networkFile);
        }
    }
}
