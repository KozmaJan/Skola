﻿using System;
using System.Collections.Generic;

namespace AritmetickýVýpočet
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Pocitadlo pocitadlo = new Pocitadlo();
            float res;

            Console.WriteLine("(Zadejte Postfix:)");
            res = pocitadlo.Spocitat("/ * + 1.5 2.5 - 4.4 2.2 + 3.0 1.0");
            Console.WriteLine("Výsledek vašeho příkladu je "+ res);

            Console.WriteLine("(Zadejte Prefix:)");
            res = pocitadlo.Spocitat(" + 2 * 3 5 ");
            Console.WriteLine("Výsledek vašeho příkladu je " + res);

            Console.WriteLine("(Zadejte infix:)");
            res = pocitadlo.Spocitat("2 + 3 * 5");
            Console.WriteLine("Výsledek vašeho příkladu je "+ res);
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
            Console.WriteLine("Zadali jste váš výraz ("+vyraz+") v zápisu \"prefix\" (1), \"postfix\" (2) nebo \"infix\" (3)?");
            while(true){
                switch (Console.ReadLine()) {
                    case "prefix":
                    case "1":
                        return fromPrefix(vyraz);
                    case "postfix":
                    case "2":
                        return fromPostfix(vyraz);
                    case "3":
                    case "infix":
                        return fromPostfix(infixToPostfix(vyraz));
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Zkontrolujte pravopis.");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        }
        private float fromPostfix(string vyraz)
        {
            string[] postfix = vyraz.Replace('.', ',').Split(' ');
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
                        case "%":
                            operand = operands.Pop();
                            if (operand == 0f)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                throw new DivideByZeroException("Kámo. Fakt dělíš nulou teď? (ಠ_ಠ)");
                                Console.ForegroundColor = ConsoleColor.Gray;
                            }
                            operands.Push(operands.Pop() % operand);
                            break;
                        case "^":
                            operand = operands.Pop();
                            operands.Push((float)(Math.Pow((double)(new decimal(operands.Pop())), (double)(new decimal(operand)))));
                            break;
                        default:
                            float.TryParse(element, out operand);
                            operands.Push(operand);
                            break;

                    }
                    Console.WriteLine(operands.Peek());
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
                Console.WriteLine("   /\\_/\\  The council of wise owls is confused!   /\\_/\\\n  ((@v@))      Prosím zkontrolujte zadání!       ((@v@))\n ():::::()                                      ():::::()\n   VV-VV          /\\_/\\         /\\_/\\             VV-VV\n                 ((@v@))       ((@v@))\n                ():::::()     ():::::()\n                  VV-VV         VV-VV\n ");
                return float.PositiveInfinity;
            }
            float ret = operands.Pop();
            if (ret == -0)
            {
                ret = 0;
            }
            Console.WriteLine("Infix: "+postfixToInfix(vyraz) + " = "+ ret);
            return ret;
        }
        private float fromPrefix(string vyraz)
        {
            string[] prefix = vyraz.Replace('.', ',').Split(' ');
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
                                    case "%":
                                        operand = operands.Pop();
                                        if (operand == 0f)
                                        {
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            throw new DivideByZeroException("Kámo. Fakt dělíš nulou teď? (ಠ_ಠ)");
                                            Console.ForegroundColor = ConsoleColor.Gray;
                                        }
                                        operands.Push(operands.Pop() % operand);
                                        break;
                                    case "^":
                                        operand = operands.Pop();
                                        operands.Push((float)(Math.Pow((double)(new decimal(operands.Pop())), (double)(new decimal(operand)))));
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
                Console.WriteLine("   /\\_/\\  The council of wise owls is confused!   /\\_/\\\n  ((@v@))      Prosím zkontrolujte zadání!       ((@v@))\n ():::::()                                      ():::::()\n   VV-VV          /\\_/\\         /\\_/\\             VV-VV\n                 ((@v@))       ((@v@))\n                ():::::()     ():::::()\n                  VV-VV         VV-VV\n ");
                return float.PositiveInfinity;
            }
            float ret = operands.Pop();
            if (ret == -0)
            {
                ret = 0;
            }
            Console.WriteLine("Infix: "+prefixToInfix(vyraz) + " = " + ret);
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
                        case "^":
                        case "%":
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
            Console.WriteLine("Je váš výraz, který chete převést na infix: (" + vyraz + ") v zápisu \"prefix\" (1), \"postfix\" (2)?");
            while (true)
            {
                switch (Console.ReadLine())
                {
                    case "prefix":
                    case "1":
                        return prefixToInfix(vyraz);
                    case "postfix":
                    case "2":
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
            string[] prefix = vyraz.Replace('.', ',').Split(' ');
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
                                    case "%":
                                    case "^":
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
        private int prednost(string oper)
        {
            if (oper == "*" || oper == "/" || oper == "%")
                return 3;
            else if (oper == "+" || oper == "-")
                return 2;
            else if (oper == "^")
                return 1;
            else return -1;
        }
        public string infixToPostfix(string vyraz) {
            vyraz.Replace("((", "( (");
            vyraz.Replace("))", ") )");
            string[] infix = vyraz.Replace('.', ',').Split(' ');
            Stack<string> elements = new Stack<string>();
            string postfix = "";
            string _out;
            try { 
            foreach (string oper in infix)
            {

                if (float.TryParse(oper,out float result))
                {
                    postfix += oper + " ";
                }
                else
                {
                    switch (oper)
                    {
                        case "+":
                        case "-":
                        case "*":
                        case "/":
                        case "%":
                        case "^":
                            while (elements.Count > 0 && prednost(oper) <= prednost(elements.Peek()))
                            {
                                _out = elements.Peek();
                                elements.Pop();
                                postfix += _out + " ";
                            }
                            elements.Push(oper);
                            break;
                        case "(":
                            elements.Push(oper);
                            break;
                        case ")":
                            while (elements.Count > 0 && (_out = elements.Peek()) != "(")
                            {
                                elements.Pop();
                                postfix += _out + " ";
                            }
                            if (elements.Count > 0 && (_out = elements.Peek()) == "(")
                                elements.Pop();
                            break;
                    }
                }
            }
            while (elements.Count > 0)
            {
                _out = elements.Peek();
                elements.Pop();
                postfix += _out + " ";
            }
            return postfix.Trim();
            }
            catch (InvalidOperationException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Špatně zapsaný výraz.");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            return "ERROR";
        }
    }
}
