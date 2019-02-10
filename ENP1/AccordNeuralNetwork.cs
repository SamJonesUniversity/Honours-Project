﻿using Accord.Neuro;
using Accord.Neuro.Learning;
using System;

namespace ENP1
{
    class AccordNeuralNetwork : NeuralNetwork
    {
        public override void Create(int input, int output)
        {
            //Setup network
            IActivationFunction function = new SigmoidFunction();
            AccordNetwork = new ActivationNetwork(function, input, 5, output); //Activation function, input, hidden, hidden, output.
        }

        public override double Train(Data info, float lr, float mom)
        {
            //Setup trainer using backpropagation.
            BackPropagationLearning teacher = new BackPropagationLearning(AccordNetwork);

            //Train network on data set.
            double error = double.PositiveInfinity;

            //Recording time per tick.
            DateTime start = DateTime.Now;
            DateTime end;

            do
            {
                error = teacher.RunEpoch(info.InputData, info.OutputData);
                end = DateTime.Now;

            } while ((((end.Hour * 60 * 60) + end.Minute * 60) + end.Second) - (((start.Hour * 60 * 60) + start.Minute * 60) + start.Second) < 10);

            return error;
        }

        public override void Save(string fileName)
        {
            AccordNetwork.Save(fileName);
        }
		
		public override void Load(string fileName)
        {
            AccordNetwork = (ActivationNetwork) Network.Load(fileName);
        }
    }
}