using System;
using System.Collections.Generic;

namespace BeastInLabyrinth
{
    class Program
    {
        static void Main(string[] args)
        {
            Labyrinth labyrinth = new Labyrinth();
            labyrinth.Play(50);
        }
    }
    class Labyrinth {
        char[,] Map { get; set; }
        int[] beastpos; //pamatuje si pozici zviřete
        public Labyrinth() {
            //Vytvoří labyrint podle konzole
            int col = 0;
            int row = 0;

            while (true)
            {
                try
                {
                    col = Convert.ToInt32(Console.ReadLine());//Ošetření vyjímek
                    row = Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Parametry pište pouze číslem.");
                    continue;
                }

                Map = new char[row,col];
                for (int i = 0; i < row; i++)
                {
                    //Převede Console Readline do arraye cahrakterů
                    char[] rowChars = Console.ReadLine().ToCharArray();

                    if (rowChars.Length > col)
                    {
                            Console.WriteLine("Chyba vstupu. Zkuste ještě jednou.");
                            continue;
                    }

                    //Řádek po řádku zadá charakter po charakteru pole mapy
                    for (int j = 0; j < col; j++)
                    {
                        Map[i, j] = rowChars[j];
                    }

            }
                break;
            }
            Console.WriteLine();
        }
        public int[] FindBeast() {
            //Skript pro nalezení zvířete, vrátí x, y pozici zvířete a číslo od 1 do 4, které značí jeho natočení v prostoru (po směru hodin)
            int[] pos = { -1, -1, -1}; //Pokud zvíře nenajde vrátí arr{-1, -1, -1}

            for (int i = 0; i < Map.GetLength(0); i++) //projde skrz všechny polÍćka dokud nenajde zvíře
            {
                for (int j = 0; j < Map.GetLength(1); j++)
                {
                    if (Map[i, j] != 'X' && Map[i, j] != '.') {
                        switch (Map[i, j]){//zjistí jeho orientaci v prostoru
                            case '^':
                                pos = new int[3] { i, j, 1};
                                break;
                            case '>':
                                pos = new int[3] { i, j, 2 };
                                break;
                            case 'v':
                                pos = new int[3] { i, j, 3 };
                                break;
                            case '<':
                                pos = new int[3] { i, j, 4 };
                                break;
                        }
                    }
                }
            }
            return pos;
        }
        public void MoveBeast()
        {
            //slovníky, určují kudy je v jistém natočením zvířete vpravo a dopředu
            Dictionary<int, int[]> right = new Dictionary<int, int[]>();
            right.Add(1, new int[2] { 0, 1 });
            right.Add(2, new int[2] { 1, 0 });
            right.Add(3, new int[2] { 0, -1 });
            right.Add(4, new int[2] { -1, 0 });

            Dictionary<int, int[]> front = new Dictionary<int, int[]>();
            front.Add(1, new int[2] { -1, 0 });
            front.Add(2, new int[2] { 0, 1 });
            front.Add(3, new int[2] { 1, 0 });
            front.Add(4, new int[2] { 0, -1 });

            //Které natočení odpovídá graficky kterému symbolu
            Dictionary<int, char> symbol = new Dictionary<int, char>();
            symbol.Add(1, '^');
            symbol.Add(2, '>');
            symbol.Add(3, 'v');
            symbol.Add(4, '<');
            
            //pohybové funkce
            void goForward()
            {
                Map[beastpos[0], beastpos[1]] = '.';
                beastpos[0] += front[beastpos[2]][0];
                beastpos[1] += front[beastpos[2]][1];
                Map[beastpos[0], beastpos[1]] = symbol[beastpos[2]];
            }
            void turnRight()
            {
                beastpos[2]++;
                if (beastpos[2] > 4)
                    beastpos[2] = 1;
                Map[beastpos[0], beastpos[1]] = symbol[beastpos[2]];
            }

            void turnLeft()
            {
                beastpos[2]--;
                if (beastpos[2] < 1)
                    beastpos[2] = 4;
                Map[beastpos[0], beastpos[1]] = symbol[beastpos[2]];
            }

            //zkusí najít zvíře, vrátí error, pokud v plánku žádné není
            if (beastpos == null)
                beastpos = FindBeast();
            if (beastpos == new int[3] { -1, -1, -1 }) {
                Console.WriteLine("Error 404: Beast not found.");
                return;
            }

            //Logika pohybu zvířete:
            //1. Pokud máš vpravo stěnu a předsebou volno, jdi dopředu
            //2. Pokud máš vpravo stěnu, vepředu stěnu a vlevo volno, otoč se vlevo
            //3. Pokud máš je vepředu v pravo od tebe stěna (šel si podél stěny a zrovna ses otočil vpravo), a máš před sebou volno jdi dopředu
            //4. Jinak se otoč vpravo
            if (Map[beastpos[0] + right[beastpos[2]][0], beastpos[1] + right[beastpos[2]][1]] == 'X')
            {
                if(Map[beastpos[0] + front[beastpos[2]][0], beastpos[1] + front[beastpos[2]][1]] == '.')
                {//1.
                    goForward();
                    return;
                }
                else if (Map[beastpos[0] + front[beastpos[2]][0], beastpos[1] + front[beastpos[2]][1]] == 'X' && Map[beastpos[0] + right[beastpos[2]][0]*(-1), beastpos[1] + right[beastpos[2]][1]*(-1)] == '.')
                {//2.
                    turnLeft();
                    return;
                }
            }
            else if(Map[beastpos[0] + right[beastpos[2]][0] + front[beastpos[2]][0], beastpos[1] + right[beastpos[2]][1] + front[beastpos[2]][1]] == 'X')
            {
                if (Map[beastpos[0] + front[beastpos[2]][0], beastpos[1] + front[beastpos[2]][1]] == '.')
                {//3.
                    goForward();
                    return;
                }
            }
            //4.
            turnRight();
        }

        public void ShowMap() {//vykreslení labyrinthu do konzole
            for (int i = 0; i < Map.GetLength(0); i++)
            {
                for (int j = 0; j < Map.GetLength(1); j++)
                {
                    Console.Write(Map[i, j]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
        public void Play(int x) //pohyb zvířete+vykreslení mapy
        {
            for (int i = 0; i < x; i++)
            {
                MoveBeast();
                Console.WriteLine("Krok: " + (i+1));
                ShowMap();
            }
        }
        }
}
