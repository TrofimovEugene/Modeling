using System;
using System.Linq;

namespace Lab4
{
    public class Program
    {
        public const int Experts = 4;
        public const int Comp = 5;
        public static void Main(string[] args)
        {
            var rankEx = new decimal [Comp, Experts]
            {
                {1, 2, 1, 1},
                {4, 1, 2, 2},
                {3, 3, 3, 4},
                {2, 5, 5, 3},
                {5, 4, 4, 5}
            };
            var sumsComp = new decimal [Comp] {0, 0, 0, 0, 0};
            var sumsExp = new decimal [Experts] {0, 0, 0, 0};
            for (var i = 0; i < Comp; i++)
            {
                for (var j = 0; j < Experts; j++)
                {
                    sumsComp[i] += rankEx[i, j];
                }
                Console.Write(sumsComp[i] + " ");
            }
            Console.WriteLine("\n");
            for (var i = 0; i < Experts; i++)
            {
                for (var j = 0; j < Comp; j++)
                {
                    sumsExp[i] += rankEx[j, i];
                }
                Console.Write(sumsExp[i] + " ");
            }
            Console.WriteLine("\n");
            var sumRank = sumsExp.Sum();
            var x = new decimal [Comp] {0, 0, 0, 0, 0};
            for (var i = 0; i < Comp; i++)
            {
                x[i] = sumsComp[i] / sumRank;
                Console.WriteLine(x[i]);
            }

            var S = new decimal(0.0);
            for (var i = 0; i < Comp; i++)
            { 
                S += (sumsComp[i] - 12) * (sumsComp[i] - 12);
            }

            var V = 12 * S / (Experts * Experts*(Comp * Comp * Comp - Comp));
            Console.WriteLine(V);
        }
    }
}