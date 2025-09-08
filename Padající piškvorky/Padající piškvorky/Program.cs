using System;
using System.Collections.Generic;
using System.Linq;
namespace Padající_piškvorky
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            CreateGame();
        }
        public static void CreateGame()
        {
            Console.WriteLine("Jak chcete mít velké hrací pole?");
            Console.Write("Počet řádků: ");
            int x = Convert.ToInt32(Console.ReadLine());
            Console.Write("Počet Sloupců: ");
            int y = Convert.ToInt32(Console.ReadLine());
            int[,] board = new int[x,y]; 
        } 
        class Player
        {
            string name;
            int wins;
            char symbol;
            int order;
            ConsoleColor color;

            public Player(int _order = 1, string _name = "def", ConsoleColor _color = ConsoleColor.DarkRed, char _symbol = 'O')//Předefinovaný variably aby šlo hráče vytvořit rovnou v main a ne vždycky manualně při kaźdém spuštění.
            {
                order = _order;
                name = _name;
                symbol = _symbol;
                color = _color;
            }
        }
        struct Pos
        {
            int x;
            int y;
        }
        class Game
        {
            int[,] board;
            int toWin = 4;
            public Player[] players;
            public Player[] GeneratePlayersAuto(int players)
            {
                Player[] playerList = new Player[players];
                ConsoleColor[] colors = new ConsoleColor[]
              {
                ConsoleColor.Red,
                ConsoleColor.Green,
                ConsoleColor.Blue,
                ConsoleColor.Yellow,
                ConsoleColor.Cyan,
                ConsoleColor.Magenta,
                ConsoleColor.White,
                ConsoleColor.Gray,
                ConsoleColor.DarkRed,
                ConsoleColor.DarkGreen,
                ConsoleColor.DarkBlue,
                ConsoleColor.DarkCyan
              };
                
                for (int i = 0; i < players; i++)
                {
                    playerList[i] = new Player(i, "Hráč" + (i+1), colors[i]);
                }
                return playerList;
            }
            public Player[] GeneratePlayer(int Players)
            {
                List<ConsoleColor> colors = new List<ConsoleColor>()
                {
                ConsoleColor.Red,
                ConsoleColor.Green,
                ConsoleColor.Blue,
                ConsoleColor.Yellow,
                ConsoleColor.Cyan,
                ConsoleColor.Magenta,
                ConsoleColor.White,
                ConsoleColor.Gray,
                ConsoleColor.DarkRed,
                ConsoleColor.DarkGreen,
                ConsoleColor.DarkBlue,
                ConsoleColor.DarkCyan
                };

                List<string> colors_names = new List<string>()
               {
                "Červená",
                "Zelená",
                "Modrá",
                "Žlutá",
                "Cyan",
                "Fialová",
                "Bílá",
                "Šedá",
                "Tmavě červená",
                "Tmavě zelená",
                "Tmavě modrá",
                "Tmavě cyanová"
               };

                for (int order = 0; order < Players; order++)
                {
                    Console.WriteLine("Vítejte hráči číslo " + (order + 1) + ". Zadejte svoji přezdívku.");
                    string _name = Console.ReadLine();
                    Console.WriteLine("Vaše jméno je nastaveno jako: " + _name);
                    Console.WriteLine("Jaký chcete mít symbol na hrací ploše? Zadejte pouze jeden charakter.");
                    while (true)
                    {
                        try
                        {
                            char _symbol = Console.ReadLine()[0];
                            if (_symbol == ' ' || _symbol == '\n' || _symbol == '.')
                            {
                                Console.WriteLine("Zadejte pouze jedním symbolem. Symbol \'.\', není k dispozici.");
                                continue;
                            }
                            Console.WriteLine("Váš symbol nastaven jako: " + _symbol);
                        }
                        catch
                        {
                            Console.WriteLine("Zadejte pouze jednim charakterem.");
                            continue;
                        }
                        break;
                    }
                    Console.WriteLine("Vyberte svoji barvu.");
                    for (int i = 0; i < colors.Count; i++)
                    {
                        Console.WriteLine((1 + i) + " " + colors[i]);
                    }
                    if(Int32.TryParse(Console.ReadLine(), out int N))
                    {
                        if (N<= colors.Count+1 && N > 0)
                        {
                            ConsoleColor _color = colors[N - 1];
                            colors.RemoveAt(N - 1);
                            colors_names.RemoveAt(N - 1);
                        }
                    }
                }
                return player;
            }
            void PlaceToken(int player)
             {
                int col = -1;
                bool placed = false;
                while (true)
                {
                    Console.WriteLine("Zadejte číslo sloupce, kam chcete umístit žeton.");
                    try
                    {
                        col = Convert.ToInt32(Console.ReadLine());
                    }
                    catch
                    {
                        Console.WriteLine("Zadejte pouze čislem.");
                        continue;
                    }
                    if (col < 0 || col > board.GetLength(0))
                    {
                        Console.WriteLine("Zadejte pouze číslo sloupečku na hrací desce.");
                        continue;
                    }
                    for (int i = 0; i < board.GetLength(col); i++)
                    {
                        if (board[col, i] == 0)
                        {
                            board[col, i] = player;
                            placed = true;
                            break;
                        }
                    }
                    if (placed == false)
                    {
                        Console.WriteLine("Sloupec je plný. Vyberte jiný.");
                        continue;
                    }
                    break;
                }
                }
            }
            public bool Check(int[] pos, int player)
            {
                return CheckRow(pos, player) || CheckCol(pos, player) || CheckDiag(pos, player);
            }
            bool CheckRow(int[] pos, int player)
            {
                int con = 1; //kolik v Řadě napočítánů
                for (int i = 1; i < toWin; i++)
                {
                    if (pos[1] + i < board.GetLength(1) && board[pos[0], pos[1] + i] == player)
                    {
                        con++;
                    }
                    else
                    {
                        break;
                    }
                    Console.WriteLine(board[pos[0], pos[1] + i]);
                }
                for (int i = -1; i < toWin; i--)
                {
                    if (pos[1] + i >= 0 && board[pos[0], pos[1] + i] == player)
                    {
                        con++;
                    }
                    else
                    {
                        break;
                    }
                    Console.WriteLine(board[pos[0], pos[1] + i]);
                }
                return (con >= toWin ? true : false);
            }
            bool CheckCol(int[] pos, int player)
            {
                int con = 1; //kolik v Řadě napočítánů
                for (int i = 1; i < toWin; i++)
                {
                    if (pos[0] + i < board.GetLength(0) && board[pos[0] + i, pos[1]] == player)
                    {
                        con++;
                    }
                    else
                    {
                        break;
                    }
                    Console.WriteLine(board[pos[0] + i, pos[1]]);
                }
                for (int i = -1; i < toWin; i--)
                {
                    if (pos[0] + i >= 0 && board[pos[0] + i, pos[1]] == player)
                    {
                        con++;
                    }
                    else
                    {
                        break;
                    }
                    Console.WriteLine(board[pos[0] + i, pos[1]]);
                }
                return (con >= toWin ? true : false);
            }
            bool CheckDiag(int[] pos, int player)
            {
                int con = 1; //kolik v Řadě napočítánů
                for (int i = 1; i < toWin; i++)
                {
                    if (pos[1] + i < board.GetLength(1) && pos[0] + i < board.GetLength(0) && board[pos[0] + i, pos[1] + i] == player)
                    {
                        con++;
                    }
                    else
                    {
                        break;
                    }
                }
                for (int i = 1; i < toWin; i++)
                {
                    if (pos[1] - i < 0 && pos[0] - i < 0 && board[pos[0] - i, pos[1] - i] == player)
                    {
                        con++;
                    }
                    else
                    {
                        break;
                    }
                }
                if (con >= toWin)
                {
                    return true;
                }
                int con2 = 1; //kolik v Řadě napočítánů
                for (int i = 1; i < toWin; i++)
                {
                    if (pos[1] + i < board.GetLength(0) && pos[0] - i >= 0 && board[pos[0] - i, pos[1] + i] == player)
                    {
                        con2++;
                    }
                    else
                    {
                        break;
                    }
                }
                for (int i = 1; i < toWin; i++)
                {
                    if (pos[1] - i >= 0 && pos[0] + i < board.GetLength(1) && board[pos[0] + i, pos[1] - i] == player)
                    {
                        con2++;
                    }
                    else
                    {
                        break;
                    }
                }
                return (con2 >= toWin ? true : false);
            }
        }
    }
}
