using System;
using System.Collections.Generic;
using System.IO;

namespace Knapsack
{
    class Program
    {

        
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Knapsack Vikend = new Knapsack();
            Vikend.MakeKnapsack(48);
            Vikend.BestKnapsack();
        }
    }
    class Knapsack
    {
        List<int> Values = new List<int>();
        List<int> Costs = new List<int>();
        int cap;
        int[,] sack;
        public void MakeKnapsack(int _cap)
        {
            cap = _cap;
            ReadItems();
            PrepareItems();
            sack = new int[cap, Values.Count + 1];
            Bestitems();
        }
        public void BestKnapsack()
        {
            int row = sack.GetLength(1) - 1;
            int col = sack.GetLength(0) - 1;

            List<string> BestPrace = new List<string>();
            int vydelek = 0;

            while (true)
            {
                if (row <= 0 || col < 0)
                {
                    break;
                }
                if (sack[col, row] != sack[col, row-1]){
                    BestPrace.Add(prace[row - 1].Split(',')[0]);
                    vydelek += Values[row - 1];
                    col -= Costs[row - 1];
                }
                row -= 1;
            }
            Console.WriteLine();    
            Console.WriteLine("Za útrpný víkend 48 hodin neustálé práce a 0 hodin spánky jsme si vydělali celkem " + vydelek + "Kč.");
            Console.WriteLine("A práce co jsem přijali byli: ");
            for (int i = 0; i < BestPrace.Count; i++)
            {
                Console.Write(BestPrace[i] + ", ");
            }
            Console.WriteLine();
            Console.WriteLine("Doufám že to za to úsilí opravdu stálo.");
        }
        public void PrintKnapsack()
        {
            int rows = sack.GetLength(1);
            int cols = sack.GetLength(0);

            Console.Write("    ");
            for (int c = 0; c < cols; c++)
                Console.Write($"{c,4}");
            Console.WriteLine();

            for (int r = 0; r < rows; r++)
            {
                Console.Write($"{r,4}");
                for (int c = 0; c < cols; c++)
                {
                    Console.Write($"{sack[c, r],4}");
                }
                Console.WriteLine();
            }
        }


        void Bestitems()
        {
            for (int i = 0; i < sack.GetLength(1); i++)
            { 
                for (int j = 0; j < sack.GetLength(0); j++)
                {
                    if (i == 0) 
                        {
                        sack[j, i] = 0;
                        }
                    else {
                         if (j >= Costs[i - 1])
                         {
                            if (sack[j - Costs[i - 1], i-1] + Values[i-1]>= sack[j, i - 1])
                            {
                                sack[j, i] = sack[j - Costs[i - 1], i - 1] + Values[i - 1];
                      
                            }
                            else
                            {
                                sack[j, i] = sack[j, i - 1];
                            }
                         }
                         else {
                            sack[j, i] = sack[j, i - 1];
                        }
                    }
                }
            }
        }
        void PrepareItems()
        {
            string[] item;
            int value;
            int cost;
            foreach(string iteminfo in prace)
            {
                if (iteminfo != null)
                {
                    item = iteminfo.Split(',');
                    if (int.TryParse(item[1], out cost))
                    {
                        Costs.Add(cost);
                        //Console.Write(cost);
                    }
                    if (int.TryParse(item[2], out value))
                    {
                        Values.Add(value);
                        //Console.WriteLine(value);
                    }
                }
            }
        }

        public List<string> prace = new List<string>();
        public void ReadItems()
        {
            string line;
            try
            {
                StreamReader sr = new StreamReader("c:/users/janko/onedrive/dokumenty/github/skola/knapsack/Knapsack/Prace.txt");
                line = sr.ReadLine();
                while (line != null)
                {
                    line = sr.ReadLine();
                    //Console.WriteLine(line);
                    prace.Add(line);
                }
                sr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }
    }
}
