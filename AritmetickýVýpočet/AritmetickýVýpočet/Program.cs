using System;
using System.Collections.Generic;

namespace AritmetickýVýpočet
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Pocitadlo pocitadlo = new Pocitadlo();
            float res = pocitadlo.Spocitat("2 3 5 * +");
            Console.WriteLine("Výsledek vašeho příkladu je "+ res);
            res = pocitadlo.Spocitat(" + 2 * 3 5 ");
            Console.WriteLine("Výsledek vašeho příkladu je " + res);
           
        }
    }
    class Pocitadlo
    {
        private Stack<float> operands = new Stack<float>();
        private Stack<string> operators = new Stack<string>();
        public Pocitadlo() { 
        
        }
        public float Spocitat(string vyraz)
        {
            Console.WriteLine("Zadali jste váš výraz ("+vyraz+") v zápisu \"prefix\", \"postfix\" nebo \"infix\"?");
            while(true){
                switch (Console.ReadLine()) {
                    case "prefix":
                        return fromPrefix(vyraz);
                    case "postfix":
                        return fromPostfix(vyraz);
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Zkontrolujte pravopis.");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        }
        private float fromPostfix(string vyraz)
        {
            string[] postfix = vyraz.Split();
            float operand;
            try {
                foreach (string element in postfix) {
                    switch (element) {
                        case "+":
                            operands.Push(operands.Pop() + operands.Pop());
                            break;
                        case "-":
                            operands.Push((operands.Pop() - operands.Pop()) * (-1));
                            break;
                        case "*":
                            operands.Push(operands.Pop() * operands.Pop());
                            break;
                        case "/":
                            operand = operands.Pop();
                            if (operand == 0f)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                throw new DivideByZeroException("Kámo. Fakt dělíš nulou teď? (ಠ_ಠ)");
                                Console.ForegroundColor = ConsoleColor.Gray;
                            }
                            operands.Push(operands.Pop() / operand);
                            break;
                        default:
                            float.TryParse(element, out operand);
                            operands.Push(operand);
                            break;

                    }
                }
            }
            catch(DivideByZeroException) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Kámo. Fakt dělíš nulou teď? (ಠ_ಠ)");
                Console.ForegroundColor = ConsoleColor.Gray;
                return float.PositiveInfinity;
            }
            catch (InvalidOperationException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Máš tam nějakou chybu ve výrazu, kocoure.");
                Console.ForegroundColor = ConsoleColor.Gray;
                return float.NegativeInfinity;
            }
            if (operands.Count > 1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Jejda něco se pokazilo, starý brachu.");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("\n⬜⬜⬛⬜⬛⬛⬛⬛⬜⬛⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜\n⬜⬛⬜⬛⬜⬜⬜⬜⬛⬜⬛⬜⬜⬜⬜⬜⬜⬜⬜⬜\n⬜⬛⬜⬜⬜⬜⬜⬜⬜⬜⬛⬜⬜⬜⬜⬜⬜⬜⬜⬜\n⬜⬛⬜⬜⬜⬜⬜⬜⬜⬜⬜⬛⬜⬜⬜⬜⬜⬜⬛⬜\n⬛⬜⬜⬛⬜⬜⬛⬜⬜⬜⬜⬜⬛⬜⬜⬜⬜⬛⬜⬛\n⬛⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬛⬛⬜⬜⬛⬜⬛\n⬛⬜⬜⬜⬛⬛⬜⬜⬜⬜⬜⬜⬜⬜⬜⬛⬛⬛⬜⬛\n⬛⬜⬛⬜⬛⬜⬜⬛⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬛\n⬛⬜⬜⬛⬛⬛⬛⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬛\n⬛⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬛\n⬛⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬛\n⬛⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬛\n⬛⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬛\n⬛⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬛⬜\n⬜⬛⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬛⬜\n⬜⬛⬜⬜⬛⬛⬜⬜⬛⬛⬛⬛⬜⬜⬛⬛⬜⬜⬛⬜\n⬜⬛⬜⬛⬜⬛⬜⬜⬛⬜⬜⬛⬜⬜⬛⬛⬜⬛⬜⬜\n⬜⬜⬛⬜⬜⬛⬜⬛⬜⬜⬜⬛⬜⬛⬜⬜⬛⬜⬜⬜\n⬜⬜⬜⬜⬜⬜⬛⬜⬜⬜⬜⬜⬛⬜⬜⬜⬜⬜⬜⬜");
                return float.PositiveInfinity;
            }
            float ret = operands.Pop();
            Console.WriteLine(postfixToInfix(vyraz) + " = "+ ret);
            return ret;
        }
        private float fromPrefix(string vyraz)
        {
            string[] prefix = vyraz.Split();
            float operand;
            int operand_counter = 0;
            try
            {
                foreach (string element in prefix)
                {
                    if (float.TryParse(element, out operand))
                    {
                        operands.Push(operand);
                        operand_counter++;
                        if (operand_counter >= 2)
                        {
                        while (operands.Count >= 2)
                            {
                                switch (operators.Pop())
                                {
                                    case "+":
                                        operands.Push(operands.Pop() + operands.Pop());
                                        break;
                                    case "-":
                                        operands.Push((operands.Pop() - operands.Pop()) * (-1));
                                        break;
                                    case "*":
                                        operands.Push(operands.Pop() * operands.Pop());
                                        break;
                                    case "/":
                                        operand = operands.Pop();
                                        if (operand == 0f)
                                        {
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            throw new DivideByZeroException("Kámo. Fakt dělíš nulou teď? (ಠ_ಠ)");
                                            Console.ForegroundColor = ConsoleColor.Gray;
                                        }
                                        operands.Push(operands.Pop() / operand);
                                        break;
                                    default:
                                        Console.ForegroundColor = ConsoleColor.Yellow;
                                        Console.WriteLine("Neplatný znak. Zkosntrolujte prosím zadání.");
                                        Console.ForegroundColor = ConsoleColor.Gray;
                                        break;
                                }
                            }
                        }
                    }
                    else
                    {
                        operand_counter = 0;
                        operators.Push(element);
                    }
                }
            }
            catch (DivideByZeroException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Kámo. Fakt dělíš nulou teď? (ಠ_ಠ)");
                Console.ForegroundColor = ConsoleColor.Gray;
                return float.PositiveInfinity;
            }
            catch (InvalidOperationException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Máš tam nějakou chybu ve výrazu, kocoure.");
                Console.ForegroundColor = ConsoleColor.Gray;
                return float.NegativeInfinity;
            }
            if (operands.Count > 1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Jejda něco se pokazilo, starý brachu.");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("\n⬜⬜⬛⬜⬛⬛⬛⬛⬜⬛⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜\n⬜⬛⬜⬛⬜⬜⬜⬜⬛⬜⬛⬜⬜⬜⬜⬜⬜⬜⬜⬜\n⬜⬛⬜⬜⬜⬜⬜⬜⬜⬜⬛⬜⬜⬜⬜⬜⬜⬜⬜⬜\n⬜⬛⬜⬜⬜⬜⬜⬜⬜⬜⬜⬛⬜⬜⬜⬜⬜⬜⬛⬜\n⬛⬜⬜⬛⬜⬜⬛⬜⬜⬜⬜⬜⬛⬜⬜⬜⬜⬛⬜⬛\n⬛⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬛⬛⬜⬜⬛⬜⬛\n⬛⬜⬜⬜⬛⬛⬜⬜⬜⬜⬜⬜⬜⬜⬜⬛⬛⬛⬜⬛\n⬛⬜⬛⬜⬛⬜⬜⬛⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬛\n⬛⬜⬜⬛⬛⬛⬛⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬛\n⬛⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬛\n⬛⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬛\n⬛⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬛\n⬛⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬛\n⬛⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬛⬜\n⬜⬛⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬛⬜\n⬜⬛⬜⬜⬛⬛⬜⬜⬛⬛⬛⬛⬜⬜⬛⬛⬜⬜⬛⬜\n⬜⬛⬜⬛⬜⬛⬜⬜⬛⬜⬜⬛⬜⬜⬛⬛⬜⬛⬜⬜\n⬜⬜⬛⬜⬜⬛⬜⬛⬜⬜⬜⬛⬜⬛⬜⬜⬛⬜⬜⬜\n⬜⬜⬜⬜⬜⬜⬛⬜⬜⬜⬜⬜⬛⬜⬜⬜⬜⬜⬜⬜");
                return float.PositiveInfinity;
            }
            float ret = operands.Pop();
            Console.WriteLine(prefixToInfix(vyraz) + " = " + ret);
            return ret;
        }
        private string postfixToInfix(string vyraz) {
            Stack<string> elements = new Stack<string>();
            string[] postfix = vyraz.Split();
            string operand;
            try
            {
                foreach (string element in postfix)
                {
                    switch (element)
                    {
                        case "+":
                        case "-":
                            operand = elements.Pop();
                            elements.Push(" ( " + elements.Pop() + " " + element + " " + operand + " ) ");
                            break;
                        case "*":
                        case "/":
                            operand = elements.Pop();
                            elements.Push(" " + elements.Pop() + " " + element + " " + operand + " ");
                            break;
                        default:
                            elements.Push(element);
                            break;
                    }
                }
            return elements.Pop();
            }
            catch (InvalidOperationException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Špatně zapsaný výraz.");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            return "ERROR";
        }
        public string toInfix(string vyraz)
        {
            Console.WriteLine("Je váš výraz, který chete převést na infix: (" + vyraz + ") v zápisu \"prefix\", \"postfix\"?");
            while (true)
            {
                switch (Console.ReadLine())
                {
                    case "prefix":
                        return prefixToInfix(vyraz);
                    case "postfix":
                        return postfixToInfix(vyraz);
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Zkontrolujte pravopis.");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        }
        private string prefixToInfix(string vyraz)
        {
            Stack<string> elements = new Stack<string>();
            string[] prefix = vyraz.Split();
            float f;
            string operand;
            string operat;
            int operand_counter = 0;
            try
            {
                foreach (string element in prefix)
                {
                    if (float.TryParse(element, out f))
                    {
                        elements.Push(element);
                        operand_counter++;
                        if (operand_counter >= 2)
                        {
                            while (elements.Count >= 2)
                            {
                                operat = operators.Pop();
                                switch (operat)
                                {
                                    case "+":
                                    case "-":
                                        operand = elements.Pop();
                                        elements.Push(" ( " + elements.Pop() + " " + operat + " " + operand + " ) ");
                                        break;
                                    case "*":
                                    case "/":
                                        operand = elements.Pop();
                                        elements.Push(" " + elements.Pop() + " " + operat + " " + operand + " ");
                                        break;
                                }
                            }
                        }
                    }
                    else
                    {
                        operand_counter = 0;
                        operators.Push(element);
                    }
                }
            }
            catch (InvalidOperationException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Špatně zapsaný výraz.");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            return elements.Pop();
            return "ERROR";
        }

    }
}
