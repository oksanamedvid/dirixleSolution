using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using PFMM;

namespace ConsoleApp1
{
    public class TestFmm
    {
        public Dictionary<PFMM.Point, double> ActualSolution()
        {
            var fmm = new FastMultipoleMethod();

            var points = GetPointsForSolution();

            return fmm.MethodsSingleLevel(points);
        }

        public Dictionary<PFMM.Point, double> ExpectedSolution()
        {
            var points = GetPointsForSolution();
            var interaction = new double[points.Count];
            var resDictionary = new Dictionary<PFMM.Point, double>();

            for (int i = 0; i < points.Count; i++)
            {
                for (int j = 0; j < points.Count; j++)
                {
                    if (i != j)
                    {
                        interaction[i] += Kernel(points[i], points[j]) * points[j].AdditionalValue;
                    }
                }

                resDictionary.Add(points[i], interaction[i]);
            }

            return resDictionary;
        }


        public void CompareResults()
        {
            Console.WriteLine();
            Console.WriteLine("Actual Result");
            var actualSolution = ActualSolution();
            var expectedSolution = ExpectedSolution();

            foreach (var solution in actualSolution)
            {
                var expectedSol = expectedSolution.FirstOrDefault(e => Math.Abs(e.Key.X - solution.Key.X) < 0.0001 && Math.Abs(e.Key.Y - solution.Key.Y) < 0.0001);

                Console.WriteLine($"{solution.Key.X} {solution.Key.Y} {expectedSol.Key.X} {expectedSol.Key.Y}  {solution.Value} {expectedSol.Value}");
            }

            //Console.WriteLine("Expected Result");
            //var expectedSolution = ExpectedSolution();
            //foreach (var solution in expectedSolution)
            //{
            //    Console.WriteLine($"{solution.Key.X} {solution.Key.Y} {solution.Value}");
            //}
        }

        private double Kernel(PFMM.Point p1, PFMM.Point p2)
        {
            return Math.Log(Math.Sqrt(Math.Pow((p1.X - p2.X), 2) + Math.Pow((p1.Y - p2.Y), 2)));
        }

        private List<PFMM.Point> GetPointsForSolution()
        {
            var points = new List<PFMM.Point>();

            using (TextReader reader = File.OpenText(@"C:\Users\Oksana\Desktop\дипломна робота\DirixleSolution\ConsoleApp1\RandomValues.txt"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] bits = line.Split(' ');

                    points.Add(new PFMM.Point
                    {
                        X = double.Parse(bits[0]),
                        Y = double.Parse(bits[1]),
                        AdditionalValue = double.Parse(bits[2])
                    });
                }

                return points;
            }
        }
    }
}
