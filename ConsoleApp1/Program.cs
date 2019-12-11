using System;
using System.IO;
using System.Threading;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            //WriteToFile();
            const int m = 100;
            var eq = new DirihleSolver(m, 3);

            var watch = System.Diagnostics.Stopwatch.StartNew();
            var x1 = eq.GetApproximateSolution();
            Console.WriteLine(watch.ElapsedMilliseconds);
            watch.Stop();
            watch = System.Diagnostics.Stopwatch.StartNew();

            Console.WriteLine("Actual solution:");
            foreach (var t in x1)
            {
                Console.Write(t + "   ");
            }

            Console.WriteLine();
            Console.WriteLine("Actual solution FMM:");
            var x2 = eq.GetApproximateFmmSolution();
            Console.WriteLine(watch.ElapsedMilliseconds);
            watch.Stop();
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
            var results = testFmm.CompareResults();
            Console.WriteLine($"Norma TAK {Norma(results[0], results[1])}");


        }

       static async void WriteToFile()
       {
           using (var sw = new StreamWriter(@"C:\Users\Oksana\Desktop\дипломна робота\DirixleSolution\ConsoleApp1\RandomValues.txt"))
           {
               for (var i = 0; i <= 1000; i++)
               {
                   sw.WriteLine($"{GetRandomNumber(Bound.XOutStart, Bound.XOutEnd)} {GetRandomNumber(Bound.YOutStart, Bound.YOutEnd)} {GetRandomNumber(0, 1)}");
                   Console.WriteLine(i);
                   Thread.Sleep(100);
               }
           }
        }

        static double Norma(double[] x1, double[] x2)
        {
            var max = (Math.Abs(x1[0] - x2[0]) / x1[0]);
            for (int i = 0; i < x1.Length; i++)
            {
                if ((Math.Abs(x1[i] - x2[i]) / x1[i]) > max)
                {
                    max = (Math.Abs(x1[i] - x2[i]) / x1[i]);
                    Console.WriteLine($"FIX {x1[i]} : {x2[i]}  {max}");
                }
            }

            return max;
        }

        static double GetRandomNumber(double minimum, double maximum)
        {
            Random random = new Random();
            return random.NextDouble() * (maximum - minimum) + minimum;
        }
    }
}
