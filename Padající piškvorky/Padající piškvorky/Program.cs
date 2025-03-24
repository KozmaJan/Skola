using System;

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
            //int[,] board = new int[x,y];
            //int[] players = new int[pCount]; 
            int[,] board = new int[6, 7]{
    { 0, 0, 0, 0, 1, 0, 0 },
    { 0, 0, 0, 1, 2, 0, 0 },
    { 0, 0, 1, 1, 1, 0, 0 },
    { 0, 1, 1, 1, 2, 2, 2 },
    { 1, 1, 2, 1, 2, 2, 2 },
    { 1, 2, 2, 1, 2, 2, 2 }
};
            int[] position = { 0, 4 };

            if (Check(board, position, 1))
            {
                Console.WriteLine("Vyhráli jste!");
            }
        }
        public int[,] PlaceToken(int[,] board, int player)
        {
            int col = -1;
            bool placed = false;
            while (true)
            {
                Console.WriteLine("Zadejte číslo sloupce, kam chcete dát umístit žeton.");
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
            return board;
        }
        public static bool Check(int[,] board, int[] pos, int player, int toWin = 5)
        {
            return CheckRow(board, pos, player, toWin) || CheckCol(board, pos, player, toWin) || CheckDiag(board, pos, player, toWin);
        }
        public static bool CheckRow(int[,] board, int[] pos, int player, int toWin = 5)
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
                if (pos[1] + i >= 0 && board[pos[0], pos[1] + i] == player){
                    con++;
                }
                else{
                    break;
                }
                Console.WriteLine(board[pos[0], pos[1] + i]);
            }
            return (con >= toWin ? true : false);
        }
        public static bool CheckCol(int[,] board, int[] pos, int player, int toWin = 5)
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
                if (pos[0] + i >= 0 && board[pos[0] + i, pos[1] ] == player)
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
        public static bool CheckDiag(int[,] board, int[] pos, int player, int toWin = 5)
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
            if(con >= toWin)
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
