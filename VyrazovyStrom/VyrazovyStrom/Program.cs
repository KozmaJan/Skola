using System;
using System.Collections.Generic;

namespace VyrazovyStrom
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Zadejte výraz jako postfix:");
            Node root = Node.postfixToTree("6 2 / 3 -");
            root.PrintAll();
            Node.PrintTree(root);
        }
    }
    class Node
    {
        public string value;
        public Node leftSon;
        public Node rightSon;
        public Node(string _value, Node _rightSon = null, Node _leftSon = null)
        {
            value = _value;
            rightSon = _rightSon;
            leftSon = _leftSon;
        }
        public static Node postfixToTree(string vyraz)
        {
            string[] postfix = vyraz.Replace('.', ',').Split();
            Stack<Node> operands = new Stack<Node>();
            try
            {
                foreach (string element in postfix)
                {
                    //postfix: 5.5 2.2 + 3.1 4.8 - * 6.0 /
                    //prefix: / * + 1.5 2.5 - 4.4 2.2 + 3.0 1.0 
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
            return operands.Pop();
        }
        public void PrintAll()
        {
            Prefix();
            PostFix();
            Infix();
            fromPostfix(PostFix());
        }
        public string PostFix() {

            string output = " ";

            void inorder(Node root)
            {
                if (root == null) return;

                inorder(root.leftSon);
                inorder(root.rightSon);
                //Console.Write(root.value + " ");
                output += (root.value +" ");
            }
            Console.Write("Postfix: ");

            inorder(leftSon);
            inorder(rightSon);
            //Console.Write(value + " ");
            output += (value +" ");

            Console.WriteLine(output);

            return output;
        }
        public void Prefix(){
            void inorder(Node root)
            {
                if (root == null) return;

                Console.Write(root.value + " ");
                inorder(root.leftSon);
                inorder(root.rightSon);
            }
            Console.Write("Postfix: ");

            Console.Write(value + " ");
            inorder(leftSon);
            inorder(rightSon);

            Console.WriteLine();
        }
        public void Infix (){

            void inorder(Node root)
            {
                if (root == null) return;

                inorder(root.leftSon);
                Console.Write(root.value + " ");
                inorder(root.rightSon);
            }
            Console.Write("Infix: ");

            inorder(leftSon);
            Console.Write(value + " ");
            inorder(rightSon);

            Console.WriteLine();
        }
   

        public static void PrintTree(Node root)
        {
            var maxDepth = GetMaxDepth(root);
            List<string> levels = new List<string>();
            //přepíše pomocí DFS binární strom do listu listů stringů, každý string je řádek
            for (int i = 0; i <= maxDepth; i++)
            {
                levels.Add("Á");
            }

            void toList(Node node, int depth){
                if (node == null)
                {
                    if (depth <= maxDepth)
                    { //sice neefektivnivní, ale pokud je strom nevyvážený, tak dá symbol mezery. Všechny prvky se oddělují pomocí "Á", neboť lidé, kteří v matematice pouźívají háčky a čárky pro neznámé si vykreslený strom nezaslouží
                        toList(null, depth+1);
                        levels[depth] += " Á";
                        toList(null, depth+1);
                    }
                    return;
                }

                toList(node.leftSon, depth+1);
                levels[depth] += node.value+"Á";
                toList(node.rightSon,depth+1);

            }

            toList(root, 0);

            int spacing = (int)Math.Pow(2, maxDepth);
            for (int i = 0; i < levels.Count; i++)
            {
                Console.Write(levels[i].Replace("Á", new string(' ', spacing/(2+(i*2)))));
                Console.WriteLine();

                if (i +1<levels.Count) {
                    string[] twigs = levels[i + 1].Split('Á');
                    bool odd = true;
                    Console.Write(new string(' ', spacing / (2 + ((i + 1) * 2))));
                    for (int a = 1; a< twigs.Length -1; a++)
                    {
                        if (twigs[a] != " ")
                        {
                            if (odd)
                            {
                                Console.Write(" /");
                            }
                            else
                            {
                                Console.Write("\\ ");
                            }
                        }
                        odd = !odd;
                        Console.Write(new string(' ',-1+spacing / (2 + ((i+1) * 2))));
                    }
                    Console.WriteLine();
                }
            }

        }
        private float fromPostfix(string vyraz)
        {
            Stack<float> operands = new Stack<float>();
            Stack<string> operators = new Stack<string>();
            string[] postfix = vyraz.Trim().Replace('.', ',').Split(' ');
            float operand;
            try
            {
                foreach (string element in postfix)
                {
                    switch (element)
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
                            float.TryParse(element, out operand);
                            operands.Push(operand);
                            break;
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
            Console.WriteLine("Výsledek: " + ret);
            return ret;
        }

        private static int GetMaxDepth(Node node)
        {
            if (node == null) return 0;
            return 1 + Math.Max(GetMaxDepth(node.leftSon), GetMaxDepth(node.rightSon));
        }
    }
}
