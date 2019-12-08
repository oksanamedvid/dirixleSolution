using System;
using System.Linq;

namespace ConsoleApp1
{
    public class ZeydelSlarSolver
    {
        private double eps = 0.0001;

        public double[] Resolve(double[,] mtr, double[] vtr)
        {
            var u0 = new double[vtr.Length];
            for (int i = 0; i < vtr.Length; i++)
            {
                u0[i] = -1.0;
            }

            var r = SubsctractTwoVector(vtr, MultiplyMatrixOnVector(u0, mtr));
            var lambda = ScalarTwoVector(r, r) / ScalarTwoVector(r, MultiplyMatrixOnVector(r, mtr));
            var u1 = AddTwoVector(u0, MultiplyVectorOnConst(r, lambda));

            do
            {
                u0 = u1;
                r = SubsctractTwoVector(vtr, MultiplyMatrixOnVector(u0, mtr));
                lambda = ScalarTwoVector(r, r) / ScalarTwoVector(r, MultiplyMatrixOnVector(r, mtr));
                u1 = AddTwoVector(u0, MultiplyVectorOnConst(r, lambda));

            } while (Norma(u0, u1) > eps);

            return u1;
        }

        private double[] MultiplyMatrixOnVector(double[] vtr, double[,] mtr)
        {
            var result = new double[mtr.GetLength(0)];
            for (int i = 0; i < mtr.GetLength(0); i++)
            {
                for (int j = 0; j < mtr.GetLength(1); j++)
                {
                    result[i] += vtr[j] * mtr[i, j];
                }
            }

            return result;
        }

        private double[] MultiplyVectorOnConst(double[] vtr, double lambda)
        {
            var result = new double[vtr.Length];
            for (int i = 0; i < vtr.Length; i++)
            {
                result[i] += vtr[i] * lambda;
            }

            return result;
        }

        private double ScalarTwoVector(double[] x, double[] y)
        {
            var sum = 0.0;
            for (int i = 0; i < x.Length; i++)
            {
                sum += x[i] * y[i];
            }

            return sum;
        }

        private double[] SubsctractTwoVector(double[] x, double[] y)
        {
            var result = new double[x.Length];
            for (int i = 0; i < x.Length; i++)
            {
                result[i] = x[i] - y[i];
            }

            return result;
        }

        private double[] AddTwoVector(double[] x, double[] y)
        {
            var result = new double[x.Length];
            for (int i = 0; i < x.Length; i++)
            {
                result[i] = x[i] + y[i];
            }

            return result;
        }

        private double Norma(double[] x, double[] y)
        {
            return x.Select((t, i) => Math.Abs(t - y[i])).Concat(new[] {0.0}).Max();
        }

        public double[] Gaus(double[,] matrix, double[] vector)
        {
            int length = vector.Length;
            double[] numArray = new double[length];
            for (int index1 = 0; index1 < length - 1; ++index1)
            {
                double num1 = matrix[index1, index1];
                int index2 = index1;
                for (int index3 = index1 + 1; index3 < length; ++index3)
                {
                    if (matrix[index3, index1] > num1)
                    {
                        num1 = matrix[index3, index1];
                        index2 = index3;
                    }
                }
                if (index2 != index1)
                {
                    for (int index3 = index1; index3 < length; ++index3)
                    {
                        double num2 = matrix[index2, index3];
                        matrix[index2, index3] = matrix[index1, index3];
                        matrix[index1, index3] = num2;
                    }
                    double num3 = vector[index2];
                    vector[index2] = vector[index1];
                    vector[index1] = num3;
                }
                for (int index3 = index1 + 1; index3 < length; ++index3)
                {
                    double num2 = -(matrix[index3, index1] / matrix[index1, index1]);
                    vector[index3] = vector[index3] + num2 * vector[index1];
                    for (int index4 = index1; index4 < length; ++index4)
                        matrix[index3, index4] = matrix[index3, index4] + num2 * matrix[index1, index4];
                }
            }
            numArray[length - 1] = vector[length - 1] / matrix[length - 1, length - 1];
            for (int index1 = length - 2; index1 >= 0; --index1)
            {
                double num = 0.0;
                for (int index2 = index1 + 1; index2 < length; ++index2)
                    num += matrix[index1, index2] * numArray[index2];
                numArray[index1] = (vector[index1] - num) / matrix[index1, index1];
            }
            return numArray;
        }
    }
}
