using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedforwardNN
{
    class neuron
    {
        private synapse[] synapses;

        public neuron(synapse[] input)
        {
            this.synapses = input;
        }
        public double activate(double[] InVect)
        {
            double o = 0;
            for (int i = 0; i < synapses.Length; i++) //carry signal trough synapses
            {
                o += synapses[i].activate(InVect[i]);
            }
            //return 1 / (1 + Math.Exp(-o)); //sigmoid activation function
            return Math.Tanh(o); //tanh activtion function
                                 //return o;       
        }


        public void mutate(double rate, double intensity, Random rnd)
        {
            foreach (synapse synapse in synapses)
            {
                synapse.mutate(rate, intensity, rnd);
            }
    }
    }
}
