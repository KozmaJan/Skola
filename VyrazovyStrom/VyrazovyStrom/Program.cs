using System;
using System.Collections.Generic;

namespace VyrazovyStrom
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
    class Node
    {
        string value;
        Node leftSon;
        Node rightSon;
        public Node(string _value, Node _rightSon = null, Node _leftSon = null )
        {
            value = _value;
            rightSon = _rightSon;
            leftSon = _leftSon;
        }
        public static Node postfixToTree(string vyraz)
        {
            string[] postfix = vyraz.Split();
            Stack<Node> operands = new Stack<Node>();
            try
            {
                foreach (string element in postfix)
                {
                    switch (element)
                    {
                        case "+":
                        case "-":
                        case "*":
                        case "/":
                        case "%":
                        case "^":
                            if (operands.Count >= 2)
                            {
                                operands.Push(new Node(element, operands.Pop(), operands.Pop()));
                            }
                            else if (operands.Count == 1)
                            {
                                operands.Push(new Node(element, operands.Pop()));
                            }
                            break;
                        default:
                            if (float.TryParse(element, out float f))
                            {
                                operands.Push(new Node(element));
                            }
                            break;
                    }
                }
            }
            catch (DivideByZeroException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Kámo. Fakt dělíš nulou teď? (ಠ_ಠ)");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            catch (InvalidOperationException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Jejda něco se pokazilo, starý brachu.");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("   /\\_/\\  The council of wise owls is confused!   /\\_/\\\n  ((@v@))      Prosím zkontrolujte zadání!       ((@v@))\n ():::::()                                      ():::::()\n   VV-VV          /\\_/\\         /\\_/\\             VV-VV\n                 ((@v@))       ((@v@))\n                ():::::()     ():::::()\n                  VV-VV         VV-VV\n ");
                return null;
            }
            return null;
        }
    }
}
