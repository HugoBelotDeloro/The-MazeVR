using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedforwardNN
{
    public class network
    {
        private int size; //number of layers in the network; input layer excluded
        private layer[] layers;
        public network(int[] LayerSizes, Random rnd)
        {
            this.size = LayerSizes.Length - 1;
            layers = new layer[size];
            for (int i = 0; i < this.size; i++)
            {
                layers[i] = new layer(LayerSizes[i], LayerSizes[i + 1], rnd);
            }
        }

        public double[] feed(double[] signal) //cary signal trough the network
        {
            for (int i = 0; i < this.size; i++)
            {
                signal = layers[i].activate(signal);
            }
            return signal;
        }

        public void mutate(double rate, double intensity, Random rnd)
        {
            foreach (layer layer in layers)
            {
                layer.mutate(rate, intensity, rnd);
            }
        }
    }
}
