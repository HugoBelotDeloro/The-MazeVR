using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedforwardNN
{
    class layer
    {
        private int output_size;
        private int input_size;
        private neuron[] neurons;
        public layer(int input_size, int output_size, Random rnd)
        {
            this.input_size = input_size;
            this.output_size = output_size;
            this.neurons = new neuron[output_size];
            for (int i = 0; i < output_size; i++)
            {
                synapse[] synapses = new synapse[input_size];
                for (int j = 0;j < input_size; j++)
                {
                    synapses[j] = new synapse(rnd);
                }
                neurons[i] = new neuron(synapses);
            }
        }

        public double[] activate(double[] InVect)
        {
            double[] OutVect = new double[output_size];
            for (int i = 0; i < output_size; i++)
            {
                OutVect[i] = neurons[i].activate(InVect);
            }
            return OutVect;
        }

        public void mutate(double rate,double intensity, Random rnd) //mutation rate out of 1000
        {
            foreach (neuron neuron in neurons)
            {
                neuron.mutate(rate, intensity, rnd);
            }
        }
    }
}
