using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedforwardNN
{
    class Program
    {
        static void Main(string[] args)
        {
            //test
            Random rnd = new Random();
            int[] LayerSizes = { 2, 100, 100, 1 };
            network nn = new network(LayerSizes, rnd);

            while (true)
            {
                double[] input = new double[2];
                for (int i = 0; i < 2; i++)
                {
                    input[i] = rnd.NextDouble();
                }

                foreach (double d in nn.feed(input))
                {
                    Console.WriteLine(d);
                }
                Console.Write("\n");
                Console.ReadKey();
            }
        }
    }
}
