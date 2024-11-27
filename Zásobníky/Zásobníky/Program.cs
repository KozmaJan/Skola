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
            Stack<int> zasobnik = new Stack<int>();
        }
    }
}
