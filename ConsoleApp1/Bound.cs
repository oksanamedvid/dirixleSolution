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
        public static Point YObserve = new Point {X = 4, Y = 0};

        private static Point GetFirstBound(double t)
        {
            return new Point
            {
                X = 3.35 + Math.Cos(t) + 0.65 * Math.Cos(2 * t),
                Y = 1.5 * Math.Sin(t)
            };
        }

        private static Point GetSecondBound(double t)
        {
            return new Point
            {
                X = 1.5 * Math.Cos(t),
                Y = 1.5 * Math.Sin(t) + 2
            };
        }

        private static Point GetThirdBound(double t)
        {
            return new Point
            {
                X = Math.Cos(t),
                Y = Math.Sin(t) - 2
            };
        }

        private static Point GetFirstDerivativeBound(double t)
        {
            return new Point
            {
                X = -Math.Sin(t) - 1.3 * Math.Sin(2 * t),
                Y = 1.5 * Math.Cos(t)
            };
        }

        private static Point GetSecondDerivativeBound(double t)
        {
            return new Point
            {
                X = -1.5 * Math.Sin(t),
                Y = 1.5 * Math.Cos(t)
            };
        }

        private static Point GetThirdDerivativeBound(double t)
        {
            return new Point
            {
                X = -Math.Sin(t),
                Y = Math.Cos(t)
            };
        }

        private static double GetFirstFunction(Point p)
        {
            //return 10;
            return Math.Log(1.0 / Math.Sqrt(Math.Pow((YObserve.X - p.X), 2) + Math.Pow((YObserve.Y - p.Y), 2))) /
                   (2 * Math.PI);
        }

        private static double GetSecondFunction(Point p)
        {
            //return 10;
            return Math.Log(1.0 / Math.Sqrt(Math.Pow((YObserve.X - p.X), 2) + Math.Pow((YObserve.Y - p.Y), 2))) /
                   (2 * Math.PI);
        }

        private static double GetThirdFunction(Point p)
        {
            //return 10;
            return Math.Log(1.0 / Math.Sqrt(Math.Pow((YObserve.X - p.X), 2) + Math.Pow((YObserve.Y - p.Y), 2))) /
                   (2 * Math.PI);
        }

        public static Point GetBound(int k, double t)
        {
            switch (k)
            {
                case 0:
                    return GetFirstBound(t);
                case 1:
                    return GetSecondBound(t);
                case 2:
                    return GetThirdBound(t);
                default:
                    return new Point();
            }
        }

        public static Point GetDerivativeBound(int k, double t)
        {
            switch (k)
            {
                case 0:
                    return GetFirstDerivativeBound(t);
                case 1:
                    return GetSecondDerivativeBound(t);
                case 2:
                    return GetThirdDerivativeBound(t);
                default:
                    return new Point();
            }
        }

        public static double GetFunction(int k, Point t)
        {
            switch (k)
            {
                case 0:
                    return GetFirstFunction(t);
                case 1:
                    return GetSecondFunction(t);
                case 2:
                    return GetThirdFunction(t);
                default:
                    return 0;
            }
        }
    }
}
