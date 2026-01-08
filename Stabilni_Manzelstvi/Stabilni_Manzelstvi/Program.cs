using System;

namespace Stabilni_Manželství
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            manzelstvi Man = new manzelstvi();
        }
    }
    class manzelstvi
    {
        int n = 0; //počet mužu= počet  žen
        int[,] tierlist;
        public manzelstvi()
        {
            string[] input;
            Console.WriteLine("Zadejte preference žen:");
            while (true)
            {
                try
                {
                    int i = 0;
                    do
                    {
                        input = Console.ReadLine().Split();
                        if (n == 0)
                        {
                            n = input.Length;
                            tierlist = new int[2 * n, n];
                        }
                        else if (i == n)
                        {
                            Console.WriteLine("Zadejte preference mužů:");
                        }

                        for (int j = 0; j < n; j++)
                        {
                            tierlist[i, j] = Convert.ToInt32(input[j]);
                        }
                        i++;
                    } while (i < 2 * n);

                }
                catch
                {
                    n = 0;
                    tierlist = null;
                    Console.WriteLine("Došlo k chybě, zkontroluje");
                    continue;
                }
                break;
            }
        }
        public void ShowMap()
        {
            Console.WriteLine("Tabulka preferencí:");
            Console.WriteLine("Ženy:");
            for (int i = 0; i < tierlist.GetLength(0); i++)
            {
                if (i == n)
                {
                    Console.WriteLine("Muži:");
                }
                Console.Write(i + ":");
                for (int j = 0; j < tierlist.GetLength(1); j++)
                {
                    Console.Write("" + tierlist[i, j]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

    }
}
