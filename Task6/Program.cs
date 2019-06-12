using System;
using System.Collections.Generic;
using System.Linq;

//Ввести а1, а2, а3, М, N, L. Построить последовательность чисел ак = (7 / 3 * ак–1 + ак-2 ) /2 * ак–3. 
//Построить N элементов последовательности, либо найти первые M ее элементов, большие числа L(в зависимости от того, что выполнится раньше).
//Напечатать последовательность и причину остановки.

namespace Task6
{
    class Program
    {
        static List<double> Nums = new List<double>();
        static int currM = 0;
        static int M, N, L;
        static void Main(string[] args)
        {
            bool end = false;
            do
            {
                double a1 = CheckDouble("Введите a1:");
                double a2 = CheckDouble("Введите a2:");
                double a3 = CheckDouble("Введите a3:");
                M = CheckInt(true, "Введите M:", false);
                N = CheckInt(true, "Введите N:", true);
                L = CheckInt(false, "Введите L:", false);
                Nums.Add(a1);
                Nums.Add(a2);
                Nums.Add(a3);
                UpdateCurrM(a1, a2, a3);
                int k = 3;
                Repeat(k);
                PrintList();
                end = CheckKey();                
            } while (!end);
        }
        public static void UpdateCurrM(double a1, double a2, double a3)
        {
            if (a1 > L) currM++;
            if (a2 > L) currM++;
            if (a3 > L) currM++;
        }
        public static void PrintError(string s)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(s);
            Console.ResetColor();
        }
        public static int CheckInt(bool positive, string s, bool isN)
        {
            Console.WriteLine(s);
            int num;
            bool okay = false;
            do
            {
                if (positive) okay = Int32.TryParse(Console.ReadLine(), out num) && (num > 0) ;
                else okay = Int32.TryParse(Console.ReadLine(), out num);
                if (isN) okay = num > 3;
                if (!okay)
                    if (isN) PrintError("Ошибка ввода. Последовательность должна содержать более 3 элементов. Повторите ввод целого положительного числа");
                    else if (positive) PrintError("Ошибка ввода. Повторите ввод целого положительного числа");
                    else PrintError("Ошибка ввода. Повторите ввод целого числа");
            } while (!okay);
            return num;
        }
        public static double CheckDouble(string s)
        {
            Console.WriteLine(s);
            double num;
            bool okay = false;
            do
            {
                okay = Double.TryParse(Console.ReadLine(), out num);
                if (!okay) PrintError("Ошибка ввода. Повторите ввод действительного числа"); 
            } while (!okay);
            return num;
        }
        public static bool CheckKey()
        {
            bool next, end = false;
            int keyNum;
            Console.WriteLine("Для выхода из программы нажмите Esc, для формирования другой последовательности нажмите Enter.");
            do
            {
                keyNum = Console.ReadKey().KeyChar;
                next = (keyNum == 27) || (keyNum == 13);
            } while (!next);
            if (keyNum == 27) end = true;
            Console.Clear();
            Nums.Clear();
            currM = 0;
            return end;
        }
        public static double  NextNum(int k)
        {
            double ak = (7 / 3 * Nums[k - 1] + Nums[k - 2]) / 2 * Nums[k - 3];
            if (ak > L) currM++;
            Nums.Add(ak);
            return ak;
        }
        public static void Repeat(int k)
        {
            if (k >= N && currM >= M) Console.WriteLine("Построены первые {0} элементов и найдены первые {1} элементов больше {2}", N, M, L);
            else if (k >= N) Console.WriteLine("Построены первые {0} элементов", N);
            else if (currM >= M) Console.WriteLine("Найдены первые {0} элементов больше {1}", M, L);
            else
            {
                NextNum(k);
                Repeat(k + 1);
            }
        }
        public static void PrintList()
        {
            for (int i = 0; i < Nums.LongCount(); i++)
            {
                if (Nums[i] > L)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine(Nums[i]);
                    Console.ResetColor();
                }
                else Console.WriteLine(Nums[i]);
            }
        }
    }
}
