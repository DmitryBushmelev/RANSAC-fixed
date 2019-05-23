using System;
using System.IO;

namespace Generator
{
    class Program
    {
        static void Main()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            Console.WriteLine("Введите величину отклонения:");
            double p = double.Parse(Console.ReadLine());
            Console.WriteLine("Введите количество точек:");
            int N = int.Parse(Console.ReadLine());
            int count = 0;
            Random random = new Random();
            StreamWriter sw = new StreamWriter("output.txt");
            sw.WriteLine(p);
            sw.WriteLine(N);
            double[,] arr = new double[N, 3];

            double A = random.Next(-1000000, 1000000) / 1000000.0;
            double B = random.Next(-1000000, 1000000) / 1000000.0;
            while (Math.Sqrt(A * A + B * B) > 1)
            {
                B = random.Next(-1000000, 1000000) / 1000000.0;
            }
            double C = Math.Sqrt(1 - Math.Pow(A, 2) - Math.Pow(B, 2));
            double d = random.Next(1000000, 100000000) / 1000000.0;

            while (count < Math.Ceiling(N / 2.0))
            {
                double cosa = A / Math.Sqrt(Math.Pow(A, 2) + Math.Pow(B, 2) + Math.Pow(C, 2));
                double cosb = B / Math.Sqrt(Math.Pow(A, 2) + Math.Pow(B, 2) + Math.Pow(C, 2));
                double cosc = C / Math.Sqrt(Math.Pow(A, 2) + Math.Pow(B, 2) + Math.Pow(C, 2));
                //Console.WriteLine("{0} {1} {2}", cosa, cosb, cosc);

                double prop = random.Next(-1000000, 1000000) / 1000000.0;

                double x = random.Next(-100000000, 100000000) / 1000000.0;
                double y = random.Next(-100000000, 100000000) / 1000000.0;
                double K = d - prop * p - x * cosa;
                //double K = d - x * cosa;
                double z = (K - y * cosb) / cosc;
                if (z >= -100 && z <= 100) {
                    arr[count, 0] = x; arr[count, 1] = y; arr[count, 2] = z;
                    count++;
                }
            }

            while (count < N)
            {
                double cosa = A / Math.Sqrt(Math.Pow(A, 2) + Math.Pow(B, 2) + Math.Pow(C, 2));
                double cosb = B / Math.Sqrt(Math.Pow(A, 2) + Math.Pow(B, 2) + Math.Pow(C, 2));
                double cosc = C / Math.Sqrt(Math.Pow(A, 2) + Math.Pow(B, 2) + Math.Pow(C, 2));

                double prop = random.Next(-20000, 20000) / 10000.0;
                //double d = random.Next(1000000, 100000000) / 1000000.0;
                double x = random.Next(-100000000, 100000000) / 1000000.0;
                double y = random.Next(-100000000, 100000000) / 1000000.0;

                double K = d - prop * p - x * cosa;
                //double K = d - x * cosa;
                double z = (K - y * cosb) / cosc;
                if (z >= -100 && z <= 100)
                {
                    if (count != N - 1)
                    {
                        arr[count, 0] = x; arr[count, 1] = y; arr[count, 2] = z;
                        count++;
                    }
                    else
                    {
                        arr[count, 0] = x; arr[count, 1] = y; arr[count, 2] = z;
                        count++;
                    }
                }
            }

            for (int i = 0; i < N; i++)
            {
                int j = random.Next(0, N);
                double temp1 = arr[j, 0]; double temp2 = arr[j, 1]; double temp3 = arr[j, 2];
                arr[j, 0] = arr[i, 0]; arr[j, 1] = arr[i, 1]; arr[j, 2] = arr[i, 2];
                arr[i, 0] = temp1; arr[i, 1] = temp2; arr[i, 2] = temp3;
            }

            for (int i = 0; i < N - 1; i++)
            {
                sw.WriteLine("{0:0.000000}\t{1:0.000000}\t{2:0.000000}", arr[i, 0], arr[i, 1], arr[i, 2]);
            }
            sw.Write("{0:0.000000}\t{1:0.000000}\t{2:0.000000}", arr[N - 1, 0], arr[N - 1, 1], arr[N - 1, 2]);
            sw.Close();
            Console.WriteLine("Готово");
            Console.ReadKey();
        }
    }
}
