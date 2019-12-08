using System;
using System.IO;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            //WriteToFile();
            const int m = 1000;
            var eq = new DirihleSolver(m);

            var x1 = eq.GetApproximateSolution();
            Console.WriteLine("Actual solution:");
            foreach (var t in x1)
            {
                Console.Write(t + "   ");
            }

            Console.WriteLine();
            Console.WriteLine("Actual solution FMM:");
            var x2 = eq.GetApproximateFmmSolution();
            foreach (var t in x2)
            {
                Console.Write(t + "   ");
            }

            Console.WriteLine();
            Console.WriteLine("Expected solution:");
            var xt = eq.GetExactSolution();
            foreach (var t in xt)
            {
                Console.Write(t + "   ");
            }

            Console.WriteLine();
            Console.WriteLine($"FMM Norma {Norma(xt, x2)}");

            Console.WriteLine($"Norma {Norma(xt, x1)}");

            var testFmm = new TestFmm();
            testFmm.CompareResults();
        }

       static async void WriteToFile()
       {
           using (var sw = new StreamWriter(@"C:\Users\Oksana\Desktop\дипломна робота\DirixleSolution\ConsoleApp1\RandomValues.txt"))
           {
               for (var i = 0; i <= 100; i++)
               {
                   sw.WriteLine($"{GetRandomNumber(Bound.XOutStart, Bound.XOutEnd)} {GetRandomNumber(Bound.YOutStart, Bound.YOutEnd)} {GetRandomNumber(-1, 1)}");
               }
           }
        }


        static double Norma(double[] x1, double[] x2)
        {
            var max = (Math.Abs(x1[0] - x2[0]) / x1[0]);
            for (int i = 0; i < x1.Length; i++)
            {
                if ((Math.Abs(x1[i] - x2[i]) / x1[i]) * 100 > max)
                {
                    max = (Math.Abs(x1[i] - x2[i]) / x1[i]);
                }
            }

            return max;
        }

        static string[] GetPoints()
        {
            var points = new string[100];
            for (var i = 0; i < 100; ++i)
            {
                points[i] =
                    $"{GetRandomNumber(Bound.XOutStart, Bound.XOutEnd)} {GetRandomNumber(Bound.YOutStart, Bound.YOutEnd)} {GetRandomNumber(-1, 1)}";
            }

            return points;
        }

        static double GetRandomNumber(double minimum, double maximum)
        {
            Random random = new Random();
            return random.NextDouble() * (maximum - minimum) + minimum;
        }
    }
}
