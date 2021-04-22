using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab6
{
    public class Program
    {
        public static int n = 1200;
        public static int m = 1500;
        public static double a = 3;
        public static double b = 7;
        public static double lambda =  0.3;
        public static void Main(string[] args)
        {
            var X = new List<double>();
            var Y = new List<double>();
            var X_m = 0.0;
            var Y_m = 0.0;
            var S_x = 0.0;
            var S_y = 0.0;
            var rand = new Random();
            for (var i = 0; i < n; i++)
            {
                X.Add(a+(b-a)* rand.NextDouble());
            }

            X_m =  X.Sum() / n;
            Console.WriteLine($"X_m = {X_m}");
            for (var i = 0; i < n; i++)
            {
                S_x += X[i] * X[i];
            }

            S_x = (S_x - n * X_m * X_m) / (n - 1);
            Console.WriteLine($"S_x = {S_x}");
            
            for (var i = 0; i < m; i++)
            {
                Y.Add(- Math.Log(rand.NextDouble()) / lambda);
            }
            
            Y_m =  Y.Sum() / n;
            Console.WriteLine($"Y_m = {Y_m}");
            
            for (var i = 0; i < m; i++)
            {
                S_y += Y[i] * Y[i];
            }

            S_y = (S_y - m * Y_m * Y_m) / (m - 1);
            Console.WriteLine($"S_y = {S_y}");

            var Fp = S_y / S_x;
            Console.WriteLine($"Fp {Fp}");
            
            var S_2 = ((n-1)*S_x + (m-1) * S_y) / (n + m - 2);
            var tp = (X_m - Y_m) / (Math.Sqrt(S_2) * Math.Sqrt(1.0 / n + 1.0 / m));
            Console.WriteLine($"tp {tp}");
        }
    }
}