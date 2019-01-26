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
    class EncogDeepNetwork : NeuralNetwork
    {
        public BasicNetwork network;

        public override void Create(int input, int output)
        {
            //Setup network, parameters (Activation, bias, number of neurons).
            var elman = new ElmanPattern()
            {
                ActivationFunction = new ActivationSigmoid(),
                InputNeurons = input,
                OutputNeurons = output
            };

            elman.AddHiddenLayer(5);

            network = (BasicNetwork)elman.Generate();
        }

        public override double Train(data info, float lr, float mom)
        {
            IMLDataSet data = new BasicMLDataSet(info.InputData, info.OutputData);
            IMLDataSet sampleData = new BasicMLDataSet(info.InputDataSample, info.OutputDataSample);

            //Train network on data set, parameters (Network, dataset, learning rate, momentum).
            ICalculateScore score = new TrainingSetScore(data);
            IMLTrain trainAlt = new NeuralSimulatedAnnealing(network, score, 10, 2, 1000);
            IMLTrain learner;

            learner = new LevenbergMarquardtTraining(network, data);

            var stop = new StopTrainingStrategy();
            learner.AddStrategy(new Greedy());
            learner.AddStrategy(new HybridStrategy(trainAlt));
            learner.AddStrategy(stop);

            do
            {
                learner.Iteration();

            } while (!stop.ShouldStop());

            return learner.Error;
        }

        public override void Save()
        {
            throw new NotImplementedException();
        }
    }
}
