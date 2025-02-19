using System;
using System.Collections.Generic;
using System.Linq;


namespace Zásobníky
{
    class Program
    {
        static void Main(string[] args)
        {
            zavorky();
        }
        public static bool zavorky()
        {
            char[] Ozavorky = { '(', '[', '{' };
            char[] Uzavorky = { ')', ']', '}' };
            string zavorky = Console.ReadLine();
            Stack<char> zasobnik = new Stack<char>();
            foreach(char znak in zavorky) {
                if (Ozavorky.Contains(znak))
                {
                    zasobnik.Push(znak);
                }
                else if (Uzavorky.Contains(znak))
                {
                    if (zasobnik.Peek() == Ozavorky[Array.IndexOf(Uzavorky, znak)])
                    {
                        zasobnik.Pop();
                    }
                    else
                    {
                        Console.WriteLine("Nejedná se o správný zápis závorek.");
                        return false;
                    }
                }
            }
            return true;  
        }
        public void rozklad()
        {
            int cislo = Convert.ToInt32(Console.ReadLine());
            Stack<int> turned = new Stack<int>();
            Stack<int> unturned = new Stack<int>();
            Stack<int> final = new Stack<int>();
            for (int a = 0; a < cislo; a++)
            {
                unturned.Push(1);
            }
            int n = 1;
            int m = 1;
            while (n <= cislo)
            {
                n += 1;
                 turned.Push(unturned.Pop() +unturned.Pop());
            }
        }
        public void vypis(Stack<int> zasobnik)
        {
            Console.Write(zasobnik.Pop());
            foreach (int piece in zasobnik)
            {
                Console.Write("+"+piece);
            }
        }
    }
}
