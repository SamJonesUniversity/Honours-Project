using Encog.Engine.Network.Activation;
using Encog.ML.Data;
using Encog.ML.Data.Basic;
using Encog.ML.Train;
using Encog.Neural.Networks;
using Encog.Neural.Networks.Layers;
using Encog.Neural.Networks.Training.Propagation.Back;
using System;

namespace ENP1
{
    class EncogNetwork : NeuralNetwork
    {
        public BasicNetwork network;

        public override void Create(int input, int output)
        {
            //Setup network, parameters (Activation, bias, number of neurons).
            network = new BasicNetwork();
            network.AddLayer(new BasicLayer(null, true, input)); //Input.
            network.AddLayer(new BasicLayer(new ActivationSigmoid(), true, 10)); //Hidden.
            network.AddLayer(new BasicLayer(new ActivationSigmoid(), true, 10)); //Hidden.
            network.AddLayer(new BasicLayer(new ActivationSigmoid(), false, output)); //Output.
            network.Structure.FinalizeStructure();
            network.Reset();
        }

        public override double Train(data info, float lr, float mom)
        {
            IMLDataSet data = new BasicMLDataSet(info.InputData, info.OutputData);
            IMLDataSet sampleData = new BasicMLDataSet(info.InputDataSample, info.OutputDataSample);

            //Train network on data set, parameters (Network, dataset, learning rate, momentum).
            IMLTrain learner = new Backpropagation(network, data, lr, mom);

            //Recording time per tick.
            DateTime start = DateTime.Now;
            DateTime end;

            do
            {
                learner.Iteration();
                end = DateTime.Now;

            } while ((((end.Hour * 60 * 60) + end.Minute * 60) + end.Second) - (((start.Hour * 60 * 60) + start.Minute * 60) + start.Second) < 10);

            return learner.Error;
        }

        public override void Save()
        {
            throw new NotImplementedException();
        }
    }
}
