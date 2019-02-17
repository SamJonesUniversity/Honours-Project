using Accord.Neuro;
using Accord.Neuro.Learning;
using System;

namespace ENP1
{
    class AccordNeuralNetwork : NeuralNetwork
    {
        public override void Create(int input, int layers, int neurons, int output)
        {
            //Setup network
            IActivationFunction function = new SigmoidFunction();

            switch (layers)
            {
                case 1:
                    AccordNetwork = new ActivationNetwork(function, input, neurons, output); //Activation function, input, hidden, hidden, output.
                    break;

                case 2:
                    AccordNetwork = new ActivationNetwork(function, input, neurons, neurons, output); //Activation function, input, hidden, hidden, output.
                    break;

                case 3:
                    AccordNetwork = new ActivationNetwork(function, input, neurons, neurons, neurons, output); //Activation function, input, hidden, hidden, output.
                    break;

                case 4:
                    AccordNetwork = new ActivationNetwork(function, input, neurons, neurons, neurons, neurons, output); //Activation function, input, hidden, hidden, output.
                    break;

                case 5:
                    AccordNetwork = new ActivationNetwork(function, input, neurons, neurons, neurons, neurons, neurons, output); //Activation function, input, hidden, hidden, output.
                    break;
            }
        }

        public override double Train(Data info, float lr, float mom)
        {
            //Setup trainer using backpropagation.
            BackPropagationLearning teacher = new BackPropagationLearning(AccordNetwork);

            //Train network on data set.
            double error = double.PositiveInfinity;

            double lastError;

            do
            {
                lastError = error;
                int i = 0;

                while (i < 1000)
                {
                    error = teacher.RunEpoch(info.InputData, info.OutputData);
                    i++;
                }

            } while (lastError - error > 0.0000001);
            

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
