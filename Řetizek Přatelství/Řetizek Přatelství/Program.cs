using System;
using System.Collections.Generic;

namespace Řetizek_Přatelství
{
    class Program
    {
        static void Main(string[] args)
        {
            //sebere vstup
            Console.WriteLine("Kolik lidí chcete prozoumat?");
            int N = 0;
            //smyčka pro vyjimky, když uživatel napíše vstup špatně
            while (true) {
                if (Int32.TryParse(Console.ReadLine(), out N)) {//používa Int32.Parse, jde taky použít jenom try
                    break;
                }
                else {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Zadejte pouze číslem.");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            if (N >= 2)
            {
                int[,,] vztahy = MapRelations(N);//funkce pro vytvoření 3D pole

                Console.WriteLine();
                //vypíše krásnou tabulku
                if (true) //vše v true podmince pro připad že si to rozmysĺím a tabulka uź mi nepřijde tak hezká. Vypnutí v debugu
                {
                    Console.Write("0 ");
                    for (int b = 0; b < N; b++) //vrchni řádek tabulky
                    {
                        Console.Write((b + 1) + " ");
                    }
                    Console.WriteLine();
                    for (int a = 0; a < N; a++)
                    {
                        Console.Write((a + 1) + " ");
                        for (int b = 0; b < N; b++)
                        {
                            if (a == b)
                            {
                                Console.ForegroundColor = ConsoleColor.DarkGray;
                                Console.Write("# ");
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                            else
                            {
                                if (vztahy[a, b, 0] == 1)
                                {
                                    Console.ForegroundColor = ConsoleColor.Green;
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.DarkGray;
                                }

                                Console.Write(vztahy[a, b, 0] + " ");
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                        }
                        Console.WriteLine();
                    }
                    Console.WriteLine();
                }//konec kodu pro tabulku


            //Najde mezi kým chceme spo najít
            Console.WriteLine("Napište mezi kterými dvouma lidmi chcete najít. Např ve formátmu \"7-2\"");
            int start = 0;
            int cil = 0;

            while (true) {
                try {
                    string vstup = Console.ReadLine();
                    string[] startcil;

                    if (vstup.Contains("-")){ //ošetření pro to jestli uživatel napíše vstup jako "1-7" nebo "1 7"
                            startcil = vstup.Split("-");
                        }
                    else {
                         startcil = vstup.Split();
                    }

                    start = Convert.ToInt32(startcil[0])-1;
                    cil = Convert.ToInt32(startcil[1])-1;
                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Nenapsali jste ve správném formátů. Zku ste lépe a radostněji.");
                    Console.ForegroundColor = ConsoleColor.White;
                    continue;
                }
                break;
            }

            //BFS pro nalezeni jestli existuje spoj
            int current = start;
            Queue<int> toSearch = new Queue<int>();
            bool notfound = false;
            int step = 0;
            while (current != cil)
            {
                step++;
                for (int a = 0; a < N; a++) //iteruje tabulku
                {
                    if (vztahy[current, a, 0] == 1 && vztahy[current, a, 1] == 0)
                    {
                        toSearch.Enqueue(a); //přidá prvek do fronty, pokud sousedí s current
                        vztahy[current, a, 1] = 1;
                        vztahy[current, a, 2] = step; //step určuje, jak daleko je od startu
                        vztahy[a, current, 1] = 1;
                        vztahy[a, current, 2] = step;
                        }
                }
                if (toSearch.Count == 0) //pokud je fronta prázdńa nenalezne nic
                {
                        notfound = true;

                        Console.ForegroundColor = ConsoleColor.DarkYellow; //barvičky
                        Console.WriteLine("Nenalezeno");
                        Console.ForegroundColor = ConsoleColor.White;

                        break;
                }
                current = toSearch.Dequeue();
            }

            //pokud nalezne začne backtracovat, a vypíše nejkratší cestu
            if (notfound == false)
            {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Nalezeno");
                    Console.ForegroundColor = ConsoleColor.White;

                    List<int> path = new List<int>();
                current = cil;
                while (current != start)
                {
                    for (int a = 0; a < N; a++)
                    {
                        if (current == start)
                            {
                                break;//pokud dojde ke startu
                            }
                        if (vztahy[current, a, 0] == 1 && vztahy[current, a, 1] == 1 && vztahy[current, a, 2] == step)//podmínka která sleduje step, je jen pro ujištění ze vypíše nejratŠí cestu, od by však fungoval perfektně bez ní
                        {
                            vztahy[current, a, 1] = 2;
                            vztahy[a, current, 1] = 2;
                            path.Add(a);
                            current = a;
                            break;//prolomí smyčku, aby nedělal zbytečné mezikroky
                        }
                    }
                    step--;
                }
                //vypíše nejkratší cestu z path
                Console.Write((cil+1));
                foreach (int tile in path)
                {
                    Console.Write("-"+(tile+1));
                }
                Console.ReadKey();
            }

            else//vypíše když nenajde žádnou spojitost
            {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("Mezi lidmi " + (start+1) + " a " + (cil+1) + " neexistuje žádná spojitost.");
                    Console.ForegroundColor = ConsoleColor.White;
                }
        }
            else //podmínka pokud zadá méně jak dva lidi
            {
                Console.WriteLine("Zdá se źe nechete zkoumat žádne lidi. Moje práce zde končí, nashledanou.");
            }

            int[,,] MapRelations(int N)
            {
                int[,,] relations = new int[N, N, 3]; //Vytvoří 3D array spojů, NxN strana grafu je pro mapu spojů,
                                                      //zbylé 2 vrstvy ve třetim rozměru si pamatují jestli je pole už navštíveno, aby se kód nezasmyčkoval
                                                      //třetí vstrva si pamatuje vzdálenost od začátku, aby vždy vypsal jenom tu nejkratší cestu, krom toho j všek pole zbytné
                Console.WriteLine("Vypište všechny dvojice které chete zmapovat. Např.: \"3-5 1-5\"");
                //zeptá se na input
                while (true)
                {
                    try
                    {
                        //vytvoří NxNx3 matici, kde je každé pole rovno nule
                        for (int a= 0; a<N;a++)
                        {
                            for (int b = 0; b<N; b++)
                            {
                                relations[a, b, 0] = 0;
                                relations[b, a, 0] = 0;
                                relations[a, b, 1] = 0;
                                relations[b, a, 1] = 0;
                                relations[a, b, 2] = 0;
                                relations[b, a, 2] = 0;
                                }
                        }
                        //zpracuje uživatelův vstup
                        string[] dvojice = Console.ReadLine().Split(); //rozdělí podle mezer
                        string[] prvek;                                //placeholder hodnota pro dosazování do matice
                        foreach (string prvky in dvojice)
                        {
                            prvek = prvky.Split('-');                  //Rozdělí podle pomlčky
                            relations[Convert.ToInt32(prvek[0]) - 1, Convert.ToInt32(prvek[1]) - 1, 0] = 1;
                            relations[Convert.ToInt32(prvek[1]) - 1, Convert.ToInt32(prvek[0]) - 1, 0] = 1;
                        }
                    }
                    catch //kód celé funkce je v try{}, pokud ted uživatel zadal funcki špatně zeptá se o vstup znovu
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Dvojice napsány ve špatném formátu. Zkontrolujte jestli máte zápis správně a zkuste znovu;");
                        Console.ForegroundColor = ConsoleColor.White;
                        continue;
                    }
                    break;
                }
                return relations;
            }
        }
    }
}
