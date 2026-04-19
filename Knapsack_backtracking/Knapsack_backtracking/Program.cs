using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace Knapsack_backtracking
{
    class Program
    {
        static int bestValue;
        static List<int> bestItems;

        static void Main()
        {
            while (true)
            {
              

                

                List<int> profit = InputToArray();
                List<int> vaha = InputToArray();
                int capacita = 0;
                while (true)
                {
                    if (int.TryParse(Console.ReadLine(), out int cap))
                    {
                        capacita = cap;
                        break;
                    }
                    Console.WriteLine("Zadejte prosím pouze číslo");
                    continue;
                }

                bestValue = 0;
                bestItems = new List<int>();

                Backtrack(profit, vaha, capacita, 0, 0, 0, new List<int>());

                Console.WriteLine("-> " + bestValue);
                Console.WriteLine("-> " + string.Join(" ", bestItems.Select(i => i + 1)));
            }
        }
        static List<int> InputToArray()
        {
            List<int> values = new List<int>();
            while (true) {
                string line = Console.ReadLine();
                try {
                    values = line.Split().Select(int.Parse).ToList();
                }
                catch
                {
                    Console.WriteLine("Zadejte pouze čísla");
                    continue;
                }
                break;
            }

            return values;
        }
       
        static void Backtrack(List<int> values, List<int> weights, int capacity,
                              int index, int currentWeight, int currentValue,
                              List<int> currentItems)
        {
            if (currentWeight > capacity)
                return;

            if (index == values.Count)
            {
                if (currentValue > bestValue)
                {
                    bestValue = currentValue;
                    bestItems = new List<int>(currentItems);
                }
                return;
            }

            // vezmi položku
            currentItems.Add(index);
            Backtrack(values, weights, capacity,
                      index + 1,
                      currentWeight + weights[index],
                      currentValue + values[index],
                      currentItems);

            currentItems.RemoveAt(currentItems.Count - 1);

            // nevezmi položku
            Backtrack(values, weights, capacity,
                      index + 1,
                      currentWeight,
                      currentValue,
                      currentItems);
        }
    }
}
