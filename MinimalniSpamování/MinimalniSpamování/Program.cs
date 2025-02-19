using System;
using System.Collections.Generic;

namespace MinimalniSpamování
{
    class Program
    {
        static void Main(string[] args)
        {
            //sebere vstup
            Console.WriteLine("Zadejte tabulku vzdáleností.");
            int[,,] vztahy = ReadMatrix();
            int N = vztahy.GetLength(0);
            //smyčka pro vyjimky, když uživatel napíše vstup špatně
            if (N >= 2)
            {

                Console.WriteLine();
                //vypíše krásnou tabulku
                if (true) //vše v true podmince pro připad že si to rozmysĺím a tabulka uź mi nepřijde tak hezká. Vypnutí v debugu
                {
                    Console.Write("0  ");
                    for (int b = 0; b < N; b++) //vrchni řádek tabulky
                    {
                        for (int i = Convert.ToInt32(Math.Floor(Math.Log10(b + 1) + 1)); i < Convert.ToInt32(Math.Floor(Math.Log10(N) + 1)); i++)
                        { // ve for smyčce je kód, aby tabulka byla vždy vyrovnaná, a to i v případě, že tabulka má několik cifer
                            Console.Write(" ");
                        }
                        Console.Write((b + 1) + " ");
                    }
                    Console.WriteLine();
                    for (int a = 0; a < N; a++)
                    {
                        Console.Write((a + 1) + " ");
                        for (int i = Convert.ToInt32(Math.Floor(Math.Log10(a + 1) + 1)); i < Convert.ToInt32(Math.Floor(Math.Log10(N) + 1)); i++)
                        {
                            Console.Write(" ");
                        }
                        for (int b = 0; b < N; b++)
                        {
                            if (a == b)
                            {
                                Console.ForegroundColor = ConsoleColor.DarkGray;
                                Console.Write(" # ");
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                            else
                            {
                                if (vztahy[a, b, 0] > 0)
                                {
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.Write(" " + vztahy[a, b, 0] + " ");
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.DarkGray;
                                    Console.Write(vztahy[a, b, 0] + " ");
                                }
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                        }
                        Console.WriteLine();
                    }
                    Console.WriteLine();
                }//konec kodu pro tabulku
                string[] jmena = new string[N];
                while (true)
                {
                    Console.WriteLine("Napište jmena lidí, které chcete spamovat. Jména napište ve stejném pořadí jako jsou v tabulce a oddělte středníkem.");
                    jmena = Console.ReadLine().Split(";");
                    if (jmena.Length != N)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Zadejte všechny účastníky.");
                        Console.ForegroundColor = ConsoleColor.White;
                        continue;
                    }
                    break;
                }
                if (true)
                {
                    for (int i = 0; i > jmena.Length; i++)
                    {
                        Console.WriteLine(i + ". " + jmena[i]);
                    }
                }
                //Najde mezi kým chceme spo najít
                Console.WriteLine("Zadejte svoje jméno.");
                int start = 0;
                int cil = 0;
                while (true)
                {
                    try
                    {
                        start = Array.IndexOf(jmena, Console.ReadLine());
                    }
                    catch
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Zadejte jedno z jmen z předem daného seznamu.");
                        Console.ForegroundColor = ConsoleColor.White;
                        continue;
                    }
                    break;
                }

                //BFS pro nalezeni jestli existuje spoj
                Queue<int> toSearch = new Queue<int>();
                Queue<int> distances = new Queue<int>();
                toSearch.Enqueue(start);    
                distances.Enqueue(0);
                int current = start;
                int dist = 0;
                while (toSearch.Count > 0)
                {
                    for (int a = 0; a < N; a++) //iteruje tabulku
                    {
                        if (vztahy[current, a, 0] > 0 && (vztahy[current, a, 1] == 0 || vztahy[current, a, 1] > dist + vztahy[current, a, 0]))
                        {
                            toSearch.Enqueue(a); //přidá prvek do fronty, pokud sousedí s current
                            distances.Enqueue(dist + dist + vztahy[current, a, 0]);
                            vztahy[current, a, 1] = dist + vztahy[current, a, 0]; //určuje, jak daleko je od startu
                            vztahy[a, current, 1] = dist + vztahy[current, a, 0];
                            //vztahy[a, current, 2] = current;
                        }
                    }
                    current = toSearch.Dequeue();
                    dist = distances.Dequeue();
                }


                showMatrix(vztahy, 1);

                toSearch.Enqueue(start);
                distances.Enqueue(0);
                current = start;
                dist = 0;
                while (toSearch.Count > 0)
                {
                    for (int a = 0; a < N; a++) //iteruje tabulku
                    {
                        if (vztahy[current, a, 0] > 0 && dist + vztahy[current, a, 0] == vztahy[current, a, 1])
                        {
                            toSearch.Enqueue(a); //přidá prvek do fronty, pokud sousedí s current
                            distances.Enqueue(vztahy[current, a, 1]);
                            vztahy[current, a, 2] = 1;
                        }
                    }
                    current = toSearch.Dequeue();
                    dist = distances.Dequeue();
                }
                //vypíše nejkratší cestu
                showMatrix(vztahy, 2);
            }

            void showMatrix(int[,,] vztahy, int c)
            {
                if (true)
                {
                    Console.Write("0  ");
                    for (int b = 0; b < N; b++) //vrchni řádek tabulky
                    {
                        for (int i = Convert.ToInt32(Math.Floor(Math.Log10(b + 1) + 1)); i < Convert.ToInt32(Math.Floor(Math.Log10(N) + 1)); i++)
                        { // ve for smyčce je kód, aby tabulka byla vždy vyrovnaná, a to i v případě, že tabulka má několik cifer
                            Console.Write(" ");
                        }
                        Console.Write((b + 1) + " ");
                    }
                    Console.WriteLine();
                    for (int a = 0; a < N; a++)
                    {
                        Console.Write((a + 1) + " ");
                        for (int i = Convert.ToInt32(Math.Floor(Math.Log10(a + 1) + 1)); i < Convert.ToInt32(Math.Floor(Math.Log10(N) + 1)); i++)
                        {
                            Console.Write(" ");
                        }
                        for (int b = 0; b < N; b++)
                        {
                            if (a == b)
                            {
                                Console.ForegroundColor = ConsoleColor.DarkGray;
                                Console.Write(" # ");
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                            else
                            {
                                if (vztahy[a, b, c] > 0)
                                {
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.Write(" " + vztahy[a, b, c] + " ");
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.DarkGray;
                                    Console.Write(vztahy[a, b, c] + " ");
                                }
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                        }
                        Console.WriteLine();
                    }
                    Console.WriteLine();
                }//konec kodu pro tabulku
            }
        int[,,] ReadMatrix()
            {
                    string rawline = Console.ReadLine().Trim();
                    N = rawline.Split().Length;
                    string[] line = rawline.Split();
                    int[,,] vztahy = new int[N, N, 3];

                    for (int a = 0; a < N; a++)
                    {
                        vztahy[0, a, 0] = int.Parse(line[a]);
                        vztahy[0, a, 1] = 0;
                        vztahy[0, a, 2] = 0;
                    }
                    for (int a = 1; a < N; a++)
                    {
                     rawline = Console.ReadLine().Trim();
                     line = rawline.Split();
                        for (int i = 0; i < N; i++)
                        {
                            vztahy[a, i, 0] = int.Parse(line[i]);
                            vztahy[a, i, 1] = 0;
                            vztahy[a, i, 2] = 0;
                        }
                    }
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Tabulka úspěšně načtena.");
                    Console.ForegroundColor = ConsoleColor.White;
                    return vztahy;  
            }
        }
    }
}