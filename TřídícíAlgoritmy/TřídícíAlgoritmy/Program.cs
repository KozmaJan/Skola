using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text;


namespace TřídícíAlgoritmy
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> list = new List<int>();
            list.Add(1);
            list.Add(1);
            list.Add(1);
            list.Add(1);
            list.Add(1);
            list.Add(1);
            list.Add(1);
            list.Add(1);
            list.Add(1);
            list.Add(1);
            list.Add(1);
            list.Add(1);
            list.Add(13);
            list.Add(28);
            list.Add(12);
            list.Add(84);
            list.Add(1);
            list.Add(7);
            list.Add(6);
            list.Add(3);
            list.Add(1);
            list.Add(42);
            list.Add(9);
            list.Add(23);
            list.Add(48);
            list.Add(26);
            list.Add(13);
            list.Add(54);
            list.Add(11);
        }
        private static List<int> MergeSort(List<int> list)
        {

            if (list == null || list.Count < 2)//Zakončí rekurzivni smyčku když už nemůže podlisty dál rozdělovat
            {
                return list;
            }
            int mid = Convert.ToInt32(list.Count/2); //Najde kde je pointer pro střed seznamu a tim ho rozdĚlí
            List<int> firstList = new List<int>();
            List<int> secondList = new List<int>();
            for (int i = 0; i> list.Count; i++)
            {
                if(i <= mid)
                {
                    firstList.Add(list[i]);
                }
                else
                {
                    secondList.Add(list[i]);
                }
            }

            firstList = MergeSort(firstList); //rekurzivne zavola funkci pro obě nově vznikle poloviny
            secondList = MergeSort(secondList);


            return Merge(firstList, secondList); //Nakonec až rozdělí seznam ja jednotlivé prvky, začne je pomocí Merge zpátky skládat
        }
        private static List<int> Merge(List<int> firstList, List<int> secondList)
        {
            if (firstList == null) //pokud je jedn z listu prázdnej, přeskočí mergovaní a vrátí ten neprázdný list
                return secondList;
            if (secondList == null)
                return firstList;
            //porovná, která z hodnot větší a podle toho je spojí v daném pořadí
            //rekurzivně skládá pointry, půlící linked list, doku nebudou složeny ve správném pořadí
            List<int> newList = new List<int>();
            int j = 0;
            int k =0;
            for (int i = 0; i < firstList.Count; i++)
            {
                    if ((firstList[j] < secondList[k]  || k > secondList.Count) && j < firstList.Count)
                    {
                        newList.Add(firstList[j]);
                        j++;
                    }
                    else
                    {
                        newList.Add(secondList[k]);
                        k++;
                }
            }
            return newList;
        }
    }
}
