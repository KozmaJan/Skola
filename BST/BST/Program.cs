using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BST
{
    class Program
    {
        // binární strom -> kaźdá větev má 0-2 syny -> prvek vlevo je nižźší neź vpravo
        static void Main(string[] args)
        {
            {
        static void Main(string[] args)
        {
            // odtud by mělo být přístupné jen to nejdůležitější, žádné vnitřní pomocné implementace.
            // Strom a jeho metody mají fungovat jako černá skříňka, která nám nabízí nějaké úkoly a my se nemusíme starat o to, jakým postupem budou splněny.
            // rozhodně také nechceme mít možnost datovou stukturu nějak měnit jinak, než je dovoleno (třeba nějakým jiným způsobem moct přidat nebo odebrat uzly, aniž by platili invarianty struktury)

            tree<Student> tree = new tree<Student>(null);

            // čteme data z CSV souboru se studenty (soubor je uložen ve složce projektu bin/Debug u exe souboru)
            // CSV je formát, kdy ukládáme jednotlivé hodnoty oddělené čárkou
            // v tomto případě: Id,Jméno,Příjmení,Věk,Třída
            using(StreamReader streamReader  = new StreamReader("studenti_shuffled.csv"))
            {   
                string line = streamReader.ReadLine();
                while (line != null)
                {
                    string[] studentData = line.Split(',');

                    Student student = new Student(
                        Convert.ToInt32(studentData[0]),    // Id
                        studentData[1],                     // Jméno
                        studentData[2],                     // Příjmení
                        Convert.ToInt16(studentData[3]),    // Věk
                        studentData[4]);                    // Třída

                    // vložíme studenta do stromu, jako klíč slouží jeho Id
                    tree.Insert(student.Id, student);
                    line = streamReader.ReadLine();
                }
            }
            Console.WriteLine(tree.Find(20).value);         

        }
    }
        }
    }
    class tree<T>
    {
        int length;
        public Node<T> root;
        public tree(Node<T> _root)
        {
            root = _root;
        }
            public void Insert(int newKey, T newValue) // chceme,a by nikdo zvenku nemusel specifikovat kořen stromu, atrom sám ví, co je jeho kořen => rozdělíme na public Insert a rekurzivní private _insert
            {
                Node<T> _add(Node<T> node, int newKey, T newValue)
                {
                if (node.key == newKey)
                {
                    return node;
                }
                   if(node.key > newKey){
                    if (node.children[0] == null){
                        node.children[0] = new Node<T>(newKey, newValue);
                        return node;
                    }
                    else{
                        return _add(node.children[0], newKey, newValue);
                    }
                }
                else
                {
                    if (node.children[1] == null){
                        node.children[1] = new Node<T>(newKey, newValue);
                        return node;
                    }
                    else{
                        return _add(node.children[1], newKey, newValue);
                    }
                }
            }


            if (root == null)
            {
                root = new Node<T>(newKey, newValue);
            }
            else
            {
               root = _add(root, newKey, newValue);
            }
            }
        public void Del(int delKey) // chceme,a by nikdo zvenku nemusel specifikovat kořen stromu, atrom sám ví, co je jeho kořen => rozdělíme na public Insert a rekurzivní private _insert
        {
            Node<T> _del(Node<T> node, int delKey)
            {
                if (node.key == delKey)
                {
                    if (node.children[0] == null && node.children[1] == null)
                    {
                        node = null;
                        return node;
                    }
                    else if (node.children[0] == null || node.children[1] == null)
                    {
                        if (node.children[0] == null)
                        {
                            node = node.children[0];
                            return node;
                        }
                        else
                        {
                            node = node.children[1];
                            return node;
                        }
                    }
                    else
                    {
                        Node<T> replace = FindMin(node.children[1]);
                        node.value = replace.value;
                        node.key = replace.key;
                        node = _del(replace, replace.key);
                        return node;
                    }
                }
                if (node.key > delKey)
                {
                    if (node.children[0] != null)
                    {
                        node.children[0] = _del(node.children[0], delKey);
                    }
                    return node;
                }
                else
                {
                    if (node.children[1] != null)
                    {
                        node.children[1] = _del(node.children[1], delKey);
                    }
                    return node;
                }
            }
            root = _del(root, delKey);
        }
            public Node<T> Find(int key)
        {
            Node<T> _find(int key, Node <T> node){
                
            if (node == null)
                return null;
            if (key == node.key)
                return node;
            if (key < node.key)
                return _find(key, node.children[0]);
            else
                return _find(key, node.children[1]);
            }
            return _find(key, root);
        }

        
        public Node<T> FindMin(Node<T> node)
        {
            if (node.children[0] == null)
                return node;
            return FindMin(node.children[0]);
        }
        public Node<T> FindMax(Node<T> node)
        {
            if (node.children[1] == null)
                return node;
            return FindMin(node.children[1]);
        }
        public void Show()
        {
            string output = "";

            void _show(Node<T> vrchol)
            {
                if (vrchol == null)
                {
                    return;
                }

                _show(vrchol.children[0]);
               output += vrchol.value.ToString();
                _show(vrchol.children[1]);
            }

            
            _show(root);
            Console.WriteLine(output);

            if (root == null)
                Console.WriteLine("strom je prázdný");
                return;
        }
        public void PrintTree()
        {
            int[,] _printTree(int[,] matrix, Node<T> node, int depth)
            {
                return null;
            }
            int depth = FindDepth();
            int powDepth = Convert.ToInt32(Math.Pow(Convert.ToDouble(2), Convert.ToDouble(depth)));

            if (depth == 0)
                depth = 1;

            int[,] matrix = new int[depth, powDepth];
            matrix = _printTree(matrix, root, 0);
        }
        public void Balance()
        {
            Node<T> _balance(List<Node<T>> nodeList, Node<T> node)
            {
                
                return null;
            }
            List<Node<T>> _treeToList(Node<T> node, List<Node<T>> nodeList)
            {
                if (node.children[0] != null)
                    _treeToList(node.children[0], nodeList);

                nodeList.Add(node);

                if (node.children[1] != null)
                    _treeToList(node.children[1], nodeList);
                return nodeList;
            }
            List<Node<T>> nodeList = _treeToList(root, new List<Node<T>>());
            root = _balance(nodeList, root);
        }

        public int FindDepth()
        {
                int _findDepth(int maxDepth, Node<T> node){

                int leftDepth = 0;
                int rightDepth = 0;

                maxDepth++;

                if (node.children[0] != null)
                    leftDepth =_findDepth(maxDepth, node.children[0]);
                if (node.children[1] != null)
                    rightDepth = _findDepth(maxDepth, node.children[1]);

                if (leftDepth > maxDepth)
                    maxDepth = leftDepth;
                if (rightDepth > maxDepth)
                    maxDepth = rightDepth;

                return maxDepth;
            }

            if (root == null)
            {
                return -1;
            }

            return _findDepth(-1, root);

        }
        
    }
    class Node<T>
    {
        public T value { get; set; }
        public int key { get; set; } //hodnota, dle které se řadí prvky
        public Node<T> parent;
        public Node<T>[] children = new Node<T>[2];
        
        public Node(int _key, T _value){
            value = _value;
            key = _key;
            }

    }
    class Student
    {
        public int Id { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public int Age { get; }

        public string ClassName { get; }

        public Student(int id, string firstName, string lastName, int age, string className)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Age = age; ;
            ClassName = className;
        }

        // aby se nám při Console.WriteLine(student) nevypsala jen nějaká adresa v paměti,
        // upravíme výpis objektu typu student na něco čitelného
        public override string ToString()
        {
            return string.Format("{0} {1} (ID: {2}) ze třídy {3}", FirstName, LastName, Id, ClassName);
        }
    }
}
