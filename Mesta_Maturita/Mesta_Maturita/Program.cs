using System;
using System.Collections.Generic;

namespace Mesta_Maturita
{
    class Program
    {
        static void Main(string[] args)
        {

        }
    }
        class Map
    {
        int[,,] map;
        int cities;
        int roads;
        public Map()
        {
                try
                {
                    Console.WriteLine("Zadejte počet měst a cest.");
                    string[] input = Console.ReadLine().Split();
                    cities = Convert.ToInt32(input[0]);
                    roads = Convert.ToInt32(input[1]);
                    map = new int[cities, cities, 2];
                    for (int i = 0; i < cities; i++)
                    {
                    for (int j = 0; j < cities; j++)
                    {
                        map[i, j, 0] = 0;
                        map[i, j, 1] = 0;
                    }
                    }
                    for (int i = 0; i < roads; i++)
                    {
                        input = Console.ReadLine().Split();
                        int cityFrom = Convert.ToInt32(input[0]);
                        int cityTo = Convert.ToInt32(input[1]);
                        int roadLen = Convert.ToInt32(input[2]);
                        int toll = Convert.ToInt32(input[3]);
                        if (input[0] == input[1])
                        {
                            Convert.ToInt32("a");
                        }
                        map[cityFrom, cityTo, 0] = roadLen;
                        map[cityFrom, cityTo, 1] = toll;
                        map[cityTo, cityFrom, 0] = roadLen;
                        map[cityTo, cityFrom, 1] = toll;
                }
                }
                catch
                {
                    Console.WriteLine("Zadán chybný vstup.");
                    System.Environment.Exit(0);
                }
        }
        public void ShortestPath()
        {
            Console.WriteLine("Zadejte počáteční město.");
            string[] input = Console.ReadLine().Split();
            int start = Convert.ToInt32(input[0]);
            int target = Convert.ToInt32(input[1]);
            //int{city, total path lenght, from, toll}
            Queue<int[]> toSearch = new Queue<int[]>();
            //pamatuje si nekrajtši cestu, do každého města, uloźenou v poli [i,i] v map, jednu s 

            int current = start;
            int prev = start;
            while (true)
            {
                for (int i = 0; i < cities; i++)
                {
                    if (map[current, i, 0] != 0 && i != prev && i != current)
                    {
                         
                    }
                }
            }
        }
    }
}
