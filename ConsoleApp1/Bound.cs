using System;

namespace ConsoleApp1
{
    public static class Bound
    {
        public static double YInStart = -2;
        public static double YInEnd = 2;
        public static double XInStart = -1;
        public static double XInEnd = 6;

        public static double YOutStart = -20;
        public static double YOutEnd = 20;
        public static double XOutStart = -10;
        public static double XOutEnd = 60;

        //public static Point YObserve = new Point {X = 100, Y = 100};
        public static Point YObserve = new Point { X = 4, Y = 0 };

        public static Point GetBound(double t)
        {
            return new Point
            {
                X = 3.35 + Math.Cos(t) + 0.65 * Math.Cos(2 * t),
                Y = 1.5 * Math.Sin(t)
            };
        }

        public static Point GetDerivativeBound(double t)
        {
            return new Point
            {
                X = -Math.Sin(t) - 1.3 * Math.Sin(2 * t),
                Y = 1.5 * Math.Cos(t)
            };
        }

        public static double GetFunctionValue(Point p)
        {
            //return 10;
            return Math.Log(1.0 / Math.Sqrt(Math.Pow((YObserve.X - p.X), 2) + Math.Pow((YObserve.Y - p.Y), 2))) / (2*Math.PI);
        }
    }
}
