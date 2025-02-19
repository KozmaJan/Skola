using System;
using System.Collections.Generic;

namespace BorůvkaImplenetace
{
    class Program
    {
       
    }
    class Graf
    {
        int v; //Počet vrcholů 
        List<Node> vrcholi = new List<Node>();
        public Graf(int vrcholy, Queue<int> pocty_hran, Queue<int> cilove_vrcholy, Queue<int> delky_hran)
        {
            int h = 0;
            for (int i = 0; i < vrcholy; i++)
            {
                Node vrchol = new Node();
                vrchol.index = i;
                h = pocty_hran.Dequeue();
                for (int j = 0; j < h; i++)
                {
                    vrchol.sousedi.Add(cilove_vrcholy.Dequeue());
                    vrchol.hrany.Add(delky_hran.Dequeue());
                }
                vrcholi.Add(vrchol);
            }
        }
        public void Boruvka()
        {
            List<int> Nejkratsi = new List<int>();
            int shortest = int.MaxValue;
            for (int i = 0; i < vrcholi.Count; i++)
            {
                for (int j = 0; j < vrcholi[i].hrany.Count ; j++) {           
                    if(vrcholi[i].hrany[j] < shortest)
                    {
                        shortest = vrcholi[i].hrany[j];
                    }
                }
            Nejkratsi.Add(shortest);
            }
        }
    }
    class Node
    {
        public int index;
        public List<int> sousedi = new List<int>(); //indexi sousedů
        public List<int> hrany = new List<int>(); //vzdálenost k sousedovi
    }
}
