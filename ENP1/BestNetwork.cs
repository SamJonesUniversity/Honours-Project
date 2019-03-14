namespace ENP1
{
    /// <summary>
    /// Class to hold the information when calculating the best network settings.
    /// </summary>
    class BestNetwork
    {
        public double Error { get; set; }

        public float Momentum { get; set; }

        public float LearningRate { get; set; }

        public int Neurons { get; set; }

        public int Layers { get; set; }
    }
}
