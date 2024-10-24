using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace SeznamSpojovaci
{
    internal class Program
    {
        static void Main(string[] args)
        {
            LinkedList linkedList = new LinkedList();

            linkedList.Add(-5);
            linkedList.Add(7);
            linkedList.Add(0);
            linkedList.Add(11);
            linkedList.Add(-3);
            linkedList.Add(10);
            linkedList.Add(6);
            linkedList.PrintLinkedList();

            Console.WriteLine("Nachází se číslo 5 v Listě?: " + linkedList.Find(5));

            Console.WriteLine("Minimum Listu:" + linkedList.Min());

            Console.WriteLine("Srovnání Listu:");

            linkedList.SortLinkedList();

            linkedList.PrintLinkedList();

            Console.WriteLine("Destruktivni prunik:");

            LinkedList L1 = new LinkedList();
            L1.Add(4);
            L1.Add(1);
            L1.Add(0);
            L1.Add(2);
            L1.Add(3);
            L1.Add(2);
            LinkedList L2 = new LinkedList();
            L2.Add(0);
            L2.Add(2);
            L2.Add(1);
            L2.Add(8);

            L1.DestructMerge(L2);
            L1.PrintLinkedList();
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

        public bool Find(int value)//Najde danný prvek, časová složitost O(n)
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
        public int Min() //Najde minimalni prvek, časová složitost O(n)
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
        public void PrintLinkedList() //Vypíše linked list, O(n)
        {
            Node node = Head;
            Console.WriteLine("Vypsaný LinkedList:");
            while (node != null)
            {
                Console.WriteLine(node.Value);
                node = node.Next;
            }
        }
        public void SortLinkedList() //Pro naši funkci budeme používat MergeSort, který pro linked list funguje trochu jinak než u normálniho listu
        {
            Head = MergeSort(Head); //Začne rekurzivni funkci Mergesort, ktera vrací jenom Node kterym list začíná
        }
        private static Node MergeSort(Node head)
        {

            if (head == null || head.Next == null)//Zakončí rekurzivni smyčku když už nemůže podlisty dál rozdělovat
            {
                return head;
            }
            Node mid = Split(head); //Najde kde je pointer pro střed seznamu a tim ho rozdĚlí

            head = MergeSort(head); //rekurzivne zavola funkci pro obě nově vznikle poloviny
            mid = MergeSort(mid);


            return Merge(head, mid); //Nakonec až rozdělí seznam ja jednotlivé prvky, začne je pomocí Merge zpátky skládat
        }
        private static Node Merge(Node head, Node Mid)
        {
            if (head == null) //pokud je jedn z listu prázdnej, přeskočí mergovaní a vrátí ten neprázdný list
                return Mid;
            if (Mid == null)
                return head;
            //porovná, která z hodnot větší a podle toho je spojí v daném pořadí
            //rekurzivně skládá pointry, půlící linked list, doku nebudou složeny ve správném pořadí
            if (head.Value < Mid.Value)
            {
                head.Next = Merge(head.Next, Mid);
                return head;
            }
            else
            {
                Mid.Next = Merge(head, Mid.Next);
                return Mid;
            }
        }
        private static Node Split(Node head) //rekurzivni funkce pro rozdělení LinkedListu pro merge sort
        {
            Node fast = head; //jelikož u LinkedListu nemůžeme jednodušee najít střed, použijeme následovnou metodu
            Node slow = head; //vytvoříme si dva Pointry, rychlý a pomalý, jde vždy o jeden prvek, rychĺý o dva. Necháme je oba projít zároveň listem. Až rychlí dojde na konec, zastavíme a list rozdělíme v místě kde je pomalý. Tak najdeme půlku

            while (fast != null && fast.Next != null)
            {
                fast = fast.Next.Next;//rychlý se pohne o dva
                if (fast != null)
                {
                    slow = slow.Next;//pomalý o jeden, dokud rychlý nedojede
                }
            }
            Node temp = slow.Next;//rozdělí list, temp značí Node uprostřed
            slow.Next = null;
            return temp;
        }
        public void DestructMerge(LinkedList toMerge) //Funkce pro destruktivni průnik dvou linkedListu
        {
            SortLinkedList(); //oba listy nejdřív vzestupně srovná
            toMerge.SortLinkedList();

            if (toMerge.Head.Next == null) //ošetří pro vyjímku kdy druhý list má méně jak dva prvky
            {
                if (Find(toMerge.Head.Value))
                    Head = toMerge.Head;
                else
                    Head = null;
                return;
            }

            //započne rekurzivni algoritmus pro nalezeni průniku listu
            Head = DestructMergeNext(Head, toMerge.Head);
        }
        private Node DestructMergeNext(Node head1, Node head2) //rekurze pro mnozinu, časova kompexita O(n), n je součet počtu prvku obou původních listů
        {
            //Vezme si dva pod-LinkedListy, jejiž hlavy značí head1 a head2
            //Kdykoliv je hodnota z jedne hlav menší než ta druhá, posune hlavu daného listu dokud se nebudou obě hlavy rovnat
            //Poté rekurzivně započne funkci znova, ale až pro druhy prvek každého podlistu
            //rekurze vrací hlavu pouze jednoho ze zesnamů a to tehdy když narazí na konec listu, nebo se obě hlavy rovnají a rekurzivně najde další prvek
            while(head1 != null & head2 != null)
            if (head1.Value == head2.Value)
            {
                  head1.Next = DestructMergeNext(head1.Next, head2.Next);
                  break;
            }
            else if (head1.Value < head2.Value)
            {
                head1 = head1.Next;
            }
            else
            {
                head2 = head2.Next;
            }
            return head1;
        }
    }
}

