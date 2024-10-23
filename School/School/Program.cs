using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace School
{
    internal class Program
    {
        static void Main(string[] args)
        {
            LinkedList linkedList = new LinkedList();

            linkedList.Add(4); 
            linkedList.Add(5);
            linkedList.Add(6);
            linkedList.Find(5);
        }
    }

    class Node 
               
    {
        public Node(int value)
        {
            Value = value;
        }
        public int Value { get; }
        public Node Next { get; set; }
    }
    class LinkedList
    {
        public Node Head { get; set; }

        public void Add(int value) 
        {
            if (Head == null)
                Head = new Node(value); 
                                      
            else
            {
                Node newNode = new Node(value);
                newNode.Next = Head;
                Head = newNode;
            }
        }

        public bool Find(int value)
        {
            Node node = Head; 
            while (node != null) 
            {
                if (node.Value == value) 
                    return true;
                node = node.Next;
            }
            return false;
        }
        public int Min() //časová složitost O(n)
        {
            Node node = Head;
            int min = Head.Value;
            while (node != null)
            {
                if (node.Value < min)
                    min = node.Value;
                node = node.Next;
            }
            return min; 
        }
    }
}

