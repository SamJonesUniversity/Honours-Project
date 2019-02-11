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
        public override void Create(int input, int output)
        {
            //Setup network, parameters (Activation, bias, number of neurons).
            EncogNetwork = new BasicNetwork();
            EncogNetwork.AddLayer(new BasicLayer(null, true, input)); //Input.
            EncogNetwork.AddLayer(new BasicLayer(new ActivationSigmoid(), true, 10)); //Hidden.
            EncogNetwork.AddLayer(new BasicLayer(new ActivationSigmoid(), true, 10)); //Hidden.
            EncogNetwork.AddLayer(new BasicLayer(new ActivationSigmoid(), false, output)); //Output.
            EncogNetwork.Structure.FinalizeStructure();
            EncogNetwork.Reset();
        }

        public override double Train(Data info, float lr, float mom)
        {
            IMLDataSet data = new BasicMLDataSet(info.InputData, info.OutputData);

            //Train network on data set, parameters (Network, dataset, learning rate, momentum).
            IMLTrain learner = new Backpropagation(EncogNetwork, data, lr, mom);

            //Recording time per tick.
            DateTime start = DateTime.Now;
            DateTime end;

            do
            {
                learner.Iteration();
                end = DateTime.Now;

            } while (((end.Hour * 60 * 60) + (end.Minute * 60) + end.Second) - ((start.Hour * 60 * 60) + (start.Minute * 60) + start.Second) < 10);

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
