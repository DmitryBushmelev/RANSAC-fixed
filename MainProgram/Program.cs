using System;
using System.Linq;
using System.IO;

namespace ConsoleApp1
{
    public class Program
    {
        public static Tuple<double, double, double, double> equationPlane(double x1, double y1, double z1,
                                 double x2, double y2, double z2,
                                 double x3, double y3, double z3)
        {
            double a1 = x2 - x1;
            double b1 = y2 - y1;
            double c1 = z2 - z1;
            double a2 = x3 - x1;
            double b2 = y3 - y1;
            double c2 = z3 - z1;
            double a = b1 * c2 - b2 * c1;
            double b = a2 * c1 - a1 * c2;
            double c = a1 * b2 - b1 * a2;
            double d = -a * x1 - b * y1 - c * z1;
            return Tuple.Create(a, b, c, d);
        }

        public static double distance(double A, double B, double C, double D,
                        double x, double y, double z)
        {
            double distance = Math.Abs(A * x + B * y + C * z + D) / Math.Sqrt(Math.Pow(A, 2) + Math.Pow(B, 2) + Math.Pow(C, 2));
            return distance;
        }

        static void Main()
        {
            // Задание десятичным разделителем точку
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            StreamReader sr = new StreamReader(@"C:\Users\dmitr\OneDrive\Рабочий стол\input.txt");
            double p = double.Parse(sr.ReadLine());
            int N = int.Parse(sr.ReadLine());
            double[,] arr = new double[N, 3];
            for (int i = 0; i < N; i++)
            {
                string[] c = sr.ReadLine().Split('\t');
                arr[i, 0] = double.Parse(c[0]);
                arr[i, 1] = double.Parse(c[1]);
                arr[i, 2] = double.Parse(c[2]);
            }

            //Количество заходов 
            int T = (int)Math.Ceiling(Math.Log10(1 - 0.99) / Math.Log10(1 - Math.Pow(0.5, 3)));
            // Массив для хранения коэффициентов в текущей итерации c
            double[] bestPlane = new double[4];

            int P = 10; // Количество плоскостей для поиска
            double[,] Planes = new double[P, 4]; // Коэффициенты всех лучших плоскостей
            int[] Points = new int[P]; // Наибольшее количество удовлетворяющих точек в итерации с
            Random random = new Random();

            // Цикл поиска P лучших плоскостей
            for (int c = 0; c < P; c++)
            {
                // коэффициенты наиболее подходящей плоскости в данной итерации
                bestPlane[0] = 0; bestPlane[1] = 0; bestPlane[2] = 0; bestPlane[3] = 0;
                int bestPoints = (int)Math.Ceiling(N / 2.0);

                for (int i = 0; i < T; i++)
                {
                    int currentPoints = 0;
                    int randPoint1 = random.Next(0, N);
                    int randPoint2 = random.Next(0, N);
                    int randPoint3 = random.Next(0, N);
                    while (randPoint1 == randPoint2)
                    {
                        randPoint2 = random.Next(0, N);
                    }
                    while (randPoint1 == randPoint3 || randPoint2 == randPoint3)
                    {
                        randPoint3 = random.Next(0, N);
                    }
                    double x1 = arr[randPoint1, 0], y1 = arr[randPoint1, 1], z1 = arr[randPoint1, 2];
                    double x2 = arr[randPoint2, 0], y2 = arr[randPoint2, 1], z2 = arr[randPoint2, 2];
                    double x3 = arr[randPoint3, 0], y3 = arr[randPoint3, 1], z3 = arr[randPoint3, 2];
                    var coef = equationPlane(x1, y1, z1, x2, y2, z2, x3, y3, z3);
                    double A = coef.Item1;
                    double B = coef.Item2;
                    double C = coef.Item3;
                    double D = coef.Item4;

                    for (int j = 0; j < N; j++)
                    {
                        //Console.WriteLine(distance(A, B, C, D, arr[j, 0], arr[j, 1], arr[j, 2]));
                        if (distance(A, B, C, D, arr[j, 0], arr[j, 1], arr[j, 2]) < p)
                        {
                            currentPoints++;
                        }
                    }
                    if (currentPoints >= bestPoints)
                    {
                        bestPoints = currentPoints;
                        bestPlane[0] = A; bestPlane[1] = B; bestPlane[2] = C; bestPlane[3] = D;
                    }
                }
                Points[c] = bestPoints;
                Planes[c, 0] = bestPlane[0]; Planes[c, 1] = bestPlane[1]; Planes[c, 2] = bestPlane[2]; Planes[c, 3] = bestPlane[3];
            }

            //Находит первое максимальное значение с нулями
            int maxVal = Points.Max();
            int indexMax = Array.IndexOf(Points, maxVal);

            // Нормализация плоскости
            double mu = 1 / Math.Sqrt(Math.Pow(Planes[indexMax, 0], 2) + Math.Pow(Planes[indexMax, 1], 2) + Math.Pow(Planes[indexMax, 2], 2));

            Console.WriteLine("{0:0.000000} {1:0.000000} {2:0.000000} {3:0.000000}",
                Math.Round(mu * Planes[indexMax, 0], 6), Math.Round(mu * Planes[indexMax, 1], 6),
                Math.Round(mu * Planes[indexMax, 2], 6), Math.Round(mu * Planes[indexMax, 3], 6));

            Console.ReadKey();
        }
    }
}
