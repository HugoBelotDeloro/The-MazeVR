using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedforwardNN
{
    class synapse
    {
        private double weight;
        private double bias;
        public synapse(Random rnd)
        {
            this.weight = rnd.NextDouble() * 2 - 1;
            this.bias = rnd.NextDouble() * 2 - 1;
        }
        public double activate(double signal)
        {
            return signal * weight + bias;
        }

        public void mutate(double rate, double intensity, Random rnd)
        {
            if (rnd.NextDouble()>rate)
            {
                weight += (rnd.NextDouble()*2-1)*intensity;
            }
            if (rnd.NextDouble() > rate)
            {
                bias += (rnd.NextDouble() * 2 - 1) * intensity;
            }

        }
    }
}
