using System;

namespace MB
{
    class Program
    {

        static private double E = Math.Pow(10, -12);

        static double Func(double x)
        {
            return x + Math.Pow(x, -1) - 10 * Math.Cos(x);
        }

        static double DevFunc(double x)
        {
            return 1.0 - Math.Pow(x, -2) + 10 * Math.Sin(x);
        }

        static double Dihatomia(double a, double b)
        {
            double x = (a + b) / 2;
            bool increase = false;
            if (Func(a) < Func(b))
                increase = true;
            while (Math.Abs(Func(x)) > E)
            {
                if(Func(x) < 0)
                {
                    if (increase)
                        a = x;
                    else
                        b = x;
                }
                else
                {
                    if (increase)
                        b = x;
                    else
                        a = x;
                }
                x = (a + b) / 2;
                Console.Write("{0:f30}", Math.Abs(Func(x)));
                Console.WriteLine(',');
            }
            return x;
        }

        static double Nuthon(double x0)
        {
            double x = x0;
            while(Math.Abs(Func(x)) > E)
            {
                x = x - (Func(x) / DevFunc(x));
                Console.Write("{0:f30}", Math.Abs(Func(x)));
                Console.WriteLine(',');
            }
            return x;
        }

        static void Main(string[] args)
        {
            const int amount = 8;
            double[] gaps = new double[amount * 2];
            gaps[0] = -10.0; gaps[1] = - 9.1;
            gaps[2] = -9.0; gaps[3] = -8.5;
            gaps[4] = -5.0; gaps[5] = -4.0;
            gaps[6] = -2.0; gaps[7] = -1.0;
            gaps[8] = 0.1; gaps[9] = 0.2;
            gaps[10] = 1.0; gaps[11] = 2.0;
            gaps[12] = 5.0; gaps[13] = 6.0;
            gaps[14] = 6.5; gaps[15] = 7.5;
            Console.WriteLine("Method Dihatomia: ");
            for (int i = 0; i < amount; i++)
            {
                Console.WriteLine("Iteration for x"+(i+1)+"\n[");
                Console.WriteLine("Root: {0:f12}",Dihatomia(gaps[i*2], gaps[i * 2 + 1]));
                Console.WriteLine("]");
            }
            Console.WriteLine("Method Nuthona: ");
            for (int i = 0; i < amount; i++)
            {
                Console.WriteLine("Iteration for x"+(i+1)+"\n[");
                Console.WriteLine("{0:f12}", Nuthon((gaps[i*2] + gaps[i*2 + 1]) / 2));
                Console.WriteLine("]");
            }

        }
    }
}
