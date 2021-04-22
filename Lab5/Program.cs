using System;
using System.Collections.Generic;
using Microsoft.VisualBasic;

namespace Lab5
{
    internal static class Program
    {
        private const double N = 1000000;
        private const double A = 0.0;
        private const double B = 2.0;
        private const double Lambda = 1.5;
        private const double E = 2.71828182845904523536;
        public static void Main(string[] args)
        {
            var random = new Random();
            var Xi = new List<double>();
            for (var i = 0; i < N; i++)
            {
                var Zi = random.NextDouble();
                Xi.Add(A + (B - A) * Zi);
            }

            var f = First(B) - First(A);
            
            Console.WriteLine($"Площадь интеграла по первообразной: {f}");

            var pXi = new List<double>();

            for(var i = 0; i < N; i++)
            {
                pXi.Add(P_x(Xi[i]));
            }

            var R1 = 0.0;
            var R2 = 0.0;

            for (var i = 0; i < N; i++)
            {
                R1 += pXi[i];
                R2 += pXi[i] * pXi[i];
            }

            var I = R1 / N;
            Console.WriteLine($"Площадь интеграла по методу Монте-Карло: {I}");

            var s_p = (R2 - N * I * I) / (N - 1);
            var sigma = 1.96 * Math.Sqrt(s_p)/ Math.Sqrt(N);
            Console.WriteLine($"Доверительный интервал от {I - sigma} до {I + sigma}");
        }

        private static double First(double x)
        {
            return - (Math.Pow(E, -Lambda * x) * ( x * x / Lambda + 2 * x / (Lambda * Lambda) + 2 / (Lambda * Lambda * Lambda) ));
        }

        private static double P_x(double x)
        {
            return (B - A) * (x * x * Math.Pow(E, -Lambda * x));
        }
    }
}