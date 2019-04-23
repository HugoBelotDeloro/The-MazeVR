using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeedforwardNN;

namespace NeuroEvolution
{
    class Program
    {
        static void Main(string[] args)
        {
            //network settings
            int[] LayerSizes = { 1, 20, 1 };
            //ga setings
            int popsize = 1000;
            double rate = 100;
            int tournamentsize = 1; //number of trials before selecting individuals
            double intensity = 0.1; //maximal value added or substracted when mutating wieghts and biases
            double quality = 0.001;

            network[] pop = new network[popsize];
            Random rnd = new Random();

            for (int i = 0; i < popsize; i++)
            {
                pop[i] = new network(LayerSizes, rnd);
            }
            double[] error = new double[popsize];
            double[] input = new double[1];
            double[] outputs = new double[popsize];

            while (true)
            {
                double toterror = 0;
                input[0] = rnd.NextDouble();
                for (int j = 0; j < tournamentsize; j++) //compute error for sinus function out of several essays
                {
                    for (int i = 0; i < popsize; i++)
                    {
                        error[i] = Math.Abs(pop[i].feed(input)[0] - Math.Sin(input[0])) / Math.Sin(input[0]);
                        toterror += error[i];
                    }
                }

                Console.WriteLine("gen errors sum is " + toterror);

                //select best networks
                List<network> NextGen = new List<network>();
                Array.Sort(error, pop);
                if (error[0]<=quality)
                {
                    break;
                }

                for (int i = 0; i < 10; i++) //add top ten
                {
                    NextGen.Add(pop[i]);
                }
                while (NextGen.Count < popsize) //then individuals in top 0.1
                {
                    NextGen.Add(pop[rnd.Next((int)(popsize * 0.1))]);
                }
                foreach (network nn in NextGen) //mutate
                {
                    nn.mutate(rate,intensity,rnd);
                }
                pop = NextGen.ToArray(); //replace pop
            }

            //test selected nn
            for (int i = 0; i < 100; i++)
            {
                input[0] = rnd.NextDouble();

                error[0] = Math.Abs(pop[0].feed(input)[0] - Math.Sin(input[0])) / Math.Sin(input[0]);
                Console.WriteLine(error[0]);
            }
            Console.ReadKey();
        }
    }
}
