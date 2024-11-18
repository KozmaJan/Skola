using System;
using System.Collections.Generic;

namespace Řetizek_Přatelství
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Kolik lidí chcete prozoumat?");
            int N = 0;
            while (true) {
                if (Int32.TryParse(Console.ReadLine(), out N)) {
                    break;
                }
                else {
                    Console.WriteLine("Zadejte pouze číslem.");
                }
            }
            if (N > 0)
            {

                int[,,] vztahy = MapRelations(N);
                Console.Write("0 ");
                for (int b = 0; b < N; b++)
                {
                    Console.Write((b + 1) + " ");
                }
                Console.WriteLine();
                for (int a = 0; a < N; a++) {
                    Console.Write((a + 1) +" ");
                for (int b = 0; b < N; b++)
                {
                    if (a == b)
                    {
                        Console.Write("# ");
                    }
                    else {
                        Console.Write(vztahy[a, b, 0] + " ");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("Napište mezi kterými dvouma lidmi chcete najít. Např ve formátmu \"7-2\"");
            int start = 0;
            int cil = 0;
            while (true) {
                try {
                    string[] startcil = Console.ReadLine().Split("-");
                    start = Convert.ToInt32(startcil[0])-1;
                    cil = Convert.ToInt32(startcil[1])-1;
                }
                catch
                {
                    Console.WriteLine("Nenapsali jste ve správném formátů. Zkuste lépe a radostněji.");
                    continue;
                }
                break;
            }
            int current = start;
            List<int> toSearch = new List<int>();
            bool notfound = false;
            int step = 0;
            while (current != cil)
            {
                step++;
                for (int a = 0; a < N; a++)
                {
                    if (vztahy[current, a, 0] == 1 && vztahy[current, a, 1] == 0)
                    {
                        toSearch.Add(a);
                        vztahy[current, a, 1] = 1;
                        vztahy[current, a, 2] = step;
                        vztahy[a, current, 1] = 1;
                        vztahy[a, current, 2] = step;
                        }
                }
                if (toSearch.Count == 0)
                {
                    notfound = true;
                    Console.WriteLine("Nenalezeno");
                    break;
                }
                current = toSearch[0];
                toSearch.RemoveAt(0);
            }
            if (notfound == false)
            {
                Console.WriteLine("Nalezeno");
                List<int> path = new List<int>();
                current = cil;
                while (current != start)
                {
                    for (int a = 0; a < N; a++)
                    {
                        if (current == start)
                            {
                                break;
                            }
                        if (vztahy[current, a, 0] == 1 && vztahy[current, a, 1] == 1)
                        {
                            vztahy[current, a, 1] = 2;
                            vztahy[a, current, 1] = 2;
                            path.Add(a);
                            current = a;
                            
                        }
                    }
                    step--;
                }
                    Console.Write((cil+1));
                    foreach (int tile in path)
                {
                    Console.Write("-"+(tile+1));
                }
            }
            else
            {
                Console.WriteLine("Mezi lidmi " + start + " a " + cil + " neexistuje žádná spojitost.");
            }
        }
            else
            {
                Console.WriteLine("Zdá se źe nechete zkoumat žádne lidi. Moje práce zde končí, nashledanou.");
            }
            int[,,] MapRelations(int N)
            {
                int[,,] relations = new int[N, N, 3];
                Console.WriteLine("Vypište všechny dvojice které chete zmapovat. Např.: \"3-5 1-5\"");
                while (true)
                {
                    try
                    {
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
                        string[] dvojice = Console.ReadLine().Split();
                        string[] prvek;
                        foreach (string prvky in dvojice)
                        {
                            prvek = prvky.Split('-');
                            relations[Convert.ToInt32(prvek[0]) - 1, Convert.ToInt32(prvek[1]) - 1, 0] = 1;
                            relations[Convert.ToInt32(prvek[1]) - 1, Convert.ToInt32(prvek[0]) - 1, 0] = 1;
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Dvojice napsány ve špatném formátu. Zkontrolujte jestli máte zápis správně a zkuste znovu;");
                        continue;
                    }
                    break;
                }
                return relations;
            }
        }
    }
}
