using System;

namespace BudukovPraktika10
{
    internal class Program
    {
        static void Main(string[] args)
        {
            double a, b, c;
            string res;
            Console.Write("Введите a: ");
            double.TryParse(Console.ReadLine(), out a);
            Console.Write("Введите b: ");
            double.TryParse(Console.ReadLine(), out b);
            Console.Write("Введите c: ");
            double.TryParse(Console.ReadLine(), out c);
            if (a >= b + c || b >= a + c || c >= a + b)
                res = "NONE";
            else
            {
                if (Math.Round((a * a), 4) == Math.Round((b * b) + (c * c), 4) ||
                Math.Round((b * b), 4) == Math.Round((a * a) + (c * c), 4) ||
                Math.Round((c * c), 4) == Math.Round((a * a) + (b * b), 4))
                    res = "RIGHT";
                else
                {
                    if ((a * a) < (b * b) + (c * c) && (b * b) < (a * a) + (c * c) && (c * c) < (a * a) + (b * b))
                        res = "ACUTE";
                    else
                        res = "OBTUSE";
                }
            }
            Console.WriteLine(res);
        }
    }
}
