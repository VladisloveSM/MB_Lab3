using System;

namespace MB2
{
    class Program
    {

        static double E = Math.Pow(10, -10);

        static double[] F = new double[3];

        static double[,] Yakobi = new double[3, 3];

        static double Norma(double[] vec)
        {
            double max = Math.Abs(vec[0]);
            for (int i = 0; i < vec.Length; i++)
            {
                if (max < Math.Abs(vec[i]))
                    max = Math.Abs(vec[i]);
            }
            return max;
        }

        static double Norm(double[] x, double[] prevx)
        {
            double[] answer = new double[x.Length];
            for (int i = 0; i < answer.Length; i++)
            {
                answer[i] = x[i] - prevx[i];
            }
            return Norma(answer);
        }

        static double f1(double x1, double x2, double x3)
        {
            return x1 * x1 + x2 * x2 - x3 - 10;
        }

        static double f2(double x1, double x2, double x3)
        {
            return (x1 - 5) * (x1 - 5) + x2 * x2 + (x3 - 5) * (x3 - 5) - 100;
        }

        static double f3(double x1, double x2, double x3)
        {
            return x1 + x2 + x3 - 10;
        }

        static void SetMatrix(double x1, double x2, double x3)
        {
            Yakobi[0, 0] = 2*x1;      Yakobi[0, 1] = 2*x2;  Yakobi[0, 2] = -1;
            Yakobi[1, 0] = 2*x1-10;   Yakobi[1, 1] = 2*x2;  Yakobi[1, 2] = 2*x3-10;
            Yakobi[2, 0] = 1;         Yakobi[2, 1] = 1;     Yakobi[2, 2] = 1;
        }

        static void SetVector(double x1, double x2, double x3)
        {
            F[0] = f1(x1, x2, x3);
            F[1] = f2(x1, x2, x3);
            F[2] = f3(x1, x2, x3);
        }

        static double[,] LUdecomposition(double[,] A)
        {
            double[,] LU = new double[A.GetLength(0), A.GetLength(1)];
            for (int i = 0; i < A.GetLength(0); i++)
                for (int j = 0; j < A.GetLength(1); j++)
                    LU[i, j] = A[i, j];
            for (int i = 0; i < A.GetLength(1) - 1; i++)
            {
                for (int j = i + 1; j < A.GetLength(0); j++)
                {
                    double buffer = -LU[j, i] / LU[i, i];
                    for (int k = i; k < A.GetLength(1); k++)
                    {
                        LU[j, k] += LU[i,k] * buffer;
                    }
                    LU[j, i] = -buffer;
                }
            }
            return LU;
        }

        static double[] SolveLU(double[,] LU, double[] b)
        {
            double[] x = new double[b.Length];
            for (int i = 0; i < x.Length; i++)
            {
                x[i] = b[i];
                for (int j = 0; j < i; j++)
                {
                    x[i] -= LU[i, j] * x[j];
                }
            }
            double[] answer = new double[b.Length];
            for (int i = x.Length - 1; i >= 0; i--)
            {
                answer[i] = x[i];
                for (int j = x.Length - 1; j > i; j--)
                {
                    answer[i] -= LU[i, j]*answer[j];
                }
                answer[i] /= LU[i, i]; 
            }
            return answer;
        }

        static double[] SolveSLAU(double[,] A, double[] b)
        {
            double[,] LU = LUdecomposition(A);
            double[] x = SolveLU(LU, b);
            return x;
        }

        static double[] Nuthon(double x1, double x2, double x3)
        {
            double[] x = new double[3];
            x[0] = x1; x[1] = x2; x[2] = x3;
            SetVector(x[0], x[1], x[2]);
            SetMatrix(x[0], x[1], x[2]);
            while (Norma(F) > E)
            {
                double[] d = SolveSLAU(Yakobi, F);
                for (int i = 0; i < x.Length; i++)
                    x[i] = x[i] - d[i];
                SetVector(x[0], x[1], x[2]);
                SetMatrix(x[0], x[1], x[2]);
                Console.WriteLine("x1={0:f11}, x2={1:f11}, x3={2:f11}", x[0],x[1],x[2]);
            }
            return x;
        }

        static void Main(string[] args)
        {
            double[] x = new double[3];
            x[0] = 1; x[1] =1; x[2] = 1;
            double[] answer = Nuthon(x[0], x[1], x[2]);
            Console.WriteLine("Nuthon's Method. Roots:");
            for (int i = 0; i < answer.Length; i++)
            {
                Console.WriteLine("{0:f11}", answer[i]);
            }
        }
    }
}
