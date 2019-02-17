using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENP1
{
    class BestNetwork
    {
        public double Error { get; set; }

        public float Momentum { get; set; }

        public float LearningRate { get; set; }

        public int Neurons { get; set; }

        public int Layers { get; set; }
    }
}
