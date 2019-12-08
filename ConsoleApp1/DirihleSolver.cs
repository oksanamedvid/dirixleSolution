using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using PFMM;

namespace ConsoleApp1
{
    public class DirihleSolver
    {
        private readonly int _m;

        public DirihleSolver(int m)
        {
            _m = m;
        }

        public double[] GetApproximateFmmSolution()
        {
            var fmm = new FastMultipoleMethod();
            var mU = new ZeydelSlarSolver().Gaus(GetMatrix(), GetVector());
            var points = GetMixedPoints(mU);

            var actualSolution = fmm.MethodsSingleLevel(points)
                .Where(p => Math.Abs(p.Key.AdditionalValue) < 0.00001)
                .OrderBy(p => p.Key.Order)
                .Select(p => p.Value / (-2*_m))      
                .ToArray();

            return actualSolution;
        }

        public double[] GetApproximateSolution()
        {
            var points = GetPointsForSolution();
            var result = new double[points.Count];

            var mU = new ZeydelSlarSolver().Gaus(GetMatrix(), GetVector());
            for (var i = 0; i < points.Count; ++i)
            {
                result[i] = TrapezeMethod(points[i], mU);
            }

            return result;
        }

        public double[] GetExactSolution()
        {
            var points = GetPointsForSolution();
            double[] result = new double[points.Count];

            for (int i = 0; i < points.Count; ++i)
            {
                result[i] = Bound.GetFunctionValue(points[i]);
            }

            return result;
        }

        private double[,] GetMatrix()
        {
            var mtr = new double[2 * _m, 2 * _m];

            for (var i = 0; i < (2 * _m); ++i)
            {
                for (var j = 0; j < (2 * _m); ++j)
                {
                    mtr[i, j] = -R(T(i), j) / (2.0) + H_ii(T(i), T(j)) / (2 * _m);
                }
            }

            return mtr;
        }

        private double[] GetVector()
        {
            var vtr = new double[2 * _m];
            for (int i = 0; i < 2 * _m; ++i)
            {
                var point = Bound.GetBound(T(i));
                vtr[i] = Bound.GetFunctionValue(point);
            }

            return vtr;
        }

        private double TrapezeMethod(Point p, IReadOnlyList<double> mU)
        {
            double square = 0;

            for (var j = 0; j < 2 * _m; ++j)
            {
                square +=
                    Math.Log(1.0 / DistanceBetweenTwoPoints(p, Bound.GetBound(T(j)))) *
                    mU[j];
            }

            return square / (2 * _m);
        }

        private double R(double t1, int j)
        {
            double sum = 0;

            for (var i = 1; i < _m; ++i)
            {
                sum += Math.Cos(i * (t1 - T(j))) / i;
            }

            sum += Math.Cos(_m * (t1 - T(j))) / (2 * _m);

            return -(sum / _m);
        }

        private double T(double i)
        {
            return (i * Math.PI) / _m;
        }

        private double DistanceBetweenTwoPoints(Point p1, Point p2)
        {
            return Math.Sqrt(Math.Pow((p1.X - p2.X), 2) + Math.Pow((p1.Y - p2.Y), 2));
        }

        private double Norma(Point p)
        {
            return Math.Sqrt(Math.Pow(p.X, 2) + Math.Pow(p.Y, 2));
        }

        private double H(int i, int l, double t1, double t2)
        {
            var p1 = Bound.GetBound(t1);
            var p2 = Bound.GetBound(t2);

            return (Math.Log(1.0 / Math.Abs(DistanceBetweenTwoPoints(p1, p2))));
        }

        private double H_ii(double t1, double t2)
        {
            if (Math.Abs(t1 - t2) > 0.000000000001)
            {
                return Math.Log(1.0 / DistanceBetweenTwoPoints(Bound.GetBound(t1), Bound.GetBound(t2))) +
                       Math.Log(4.0 * Math.Pow(Math.Sin((t1 - t2) / 2.0), 2)) / 2.0;
            }

            var x = 1.0 / (Math.Pow(Bound.GetDerivativeBound(t2).X, 2) + Math.Pow(Bound.GetDerivativeBound(t2).Y, 2));
            return Math.Log(x) / 2.0;
        }

        private List<PFMM.Point> GetMixedPoints(double[] mU)
        {
            var points = new List<PFMM.Point>();
            for (var i = 0; i < 2 * _m; ++i)
            {
                var boundaryPoint = Bound.GetBound(T(i));
                points.Add(new PFMM.Point
                {
                    X = boundaryPoint.X,
                    Y = boundaryPoint.Y,
                    AdditionalValue = mU[i]
                });
            }

            points.AddRange(GetFMMPointsForSolution());
            return points;
        }

        private List<PFMM.Point> GetFMMPointsForSolution()
        {
            var points = new List<PFMM.Point>();
            var pointsForSolution = GetPointsForSolution();
            foreach (var pointForSolution in pointsForSolution)
            {
                points.Add(new PFMM.Point
                {
                    X = pointForSolution.X,
                    Y = pointForSolution.Y,
                    AdditionalValue = 0,
                    Order = pointForSolution.Order
                });
            }

            return points;
        }

        private List<Point> GetPointsForSolution()
        {
            var points = new List<Point>();

            using (TextReader reader = File.OpenText(@"C:\Users\Oksana\Desktop\дипломна робота\DirixleSolution\ConsoleApp1\Points.txt"))
            {
                string line;
                var i = 0;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] bits = line.Split(' ');

                    points.Add(new Point
                    {
                        X = double.Parse(bits[0]),
                        Y = double.Parse(bits[1]),
                        Order = i
                    });
                    i++;
                }

                return points;
            }
        }

    }
}
