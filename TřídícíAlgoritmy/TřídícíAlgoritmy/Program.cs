using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.Collections;


namespace TřídícíAlgoritmy
{
    class Program
    {
        static void Main(string[] args)
        {
           var results = BenchmarkRunner.Run<MyBenchmark>();
           // List<int> list = new List<int>();
           //
           // Random rand = new Random();
           // for (int i = 0; i < 1000; i++)
           // {
           //     list.Add(rand.Next(0, 100000000 + 1));
           // }
           //
           // list = MergeSort(list);
           //
           // int a = 0;
            //   foreach(int i in list)
            //   {
            //       Console.WriteLine(i);
            //   }

        }
        private static List<int> MergeSort(List<int> list)
        {

            if (list == null || list.Count < 2)//Zakončí rekurzivni smyčku když už nemůže podlisty dál rozdělovat
            {
                return list;
            }
            int mid = Convert.ToInt32(list.Count/2); //Najde kde je pointer pro střed seznamu a tim ho rozdělí
            List<int> firstList = new List<int>();
            List<int> secondList = new List<int>();
            for (int i = 0; i < list.Count; i++)
            {
                if(i < mid)
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
            for (int i = 0; i < firstList.Count + secondList.Count; i++)
            {
                    if (j < firstList.Count && ( secondList.Count <= k || firstList[j] < secondList[k]))
                    {
                        newList.Add(firstList[j]);
                        j++;
                    }
                    else if (k < secondList.Count)
                    {
                        newList.Add(secondList[k]);
                        k++;
                }
            }
            return newList;
     
        }
        public class MyBenchmark
        {
            public int[] numbers1;
            public int[] numbers2;

            public MyBenchmark()
            {
                Random rnd = new Random();
                int size = 1000;
                numbers1 = new int[size];
                numbers2 = new int[size];
                for (int i = 0; i < size; i++)
                {
                    numbers1[i] = rnd.Next(-50, 50);
                    numbers2[i] = numbers1[i];
                }
                mergesort_2();
                mergesort_3();
            }
            [Benchmark]
            public void mergesort_2()
            {
                numbers1 = MergeSort_2(new List<int> (numbers1)).ToArray();
            }
            [Benchmark]
            public void mergesort_3()
            {
                numbers2 = MergeSort_3(new List<int>(numbers2)).ToArray();
            }
            public static List<int> MergeSort_2(List<int> list)
            {

                if (list == null || list.Count < 2)//Zakončí rekurzivni smyčku když už nemůže podlisty dál rozdělovat
                {
                    return list;
                }
                int mid = Convert.ToInt32(list.Count / 2); //Najde kde je pointer pro střed seznamu a tim ho rozdělí
                List<int> firstList = new List<int>();
                List<int> secondList = new List<int>();
                for (int i = 0; i < list.Count; i++)
                {
                    if (i < mid)
                    {
                        firstList.Add(list[i]);
                    }
                    else
                    {
                        secondList.Add(list[i]);
                    }
                }

                firstList = MergeSort_2(firstList); //rekurzivne zavola funkci pro obě nově vznikle poloviny
                secondList = MergeSort_2(secondList);


                return Merge(firstList, secondList); //Nakonec až rozdělí seznam ja jednotlivé prvky, začne je pomocí Merge zpátky skládat
            }
            public static List<int> Merge_2(List<int> firstList, List<int> secondList)
            {
                if (firstList == null) //pokud je jedn z listu prázdnej, přeskočí mergovaní a vrátí ten neprázdný list
                    return secondList;
                if (secondList == null)
                    return firstList;
                //porovná, která z hodnot větší a podle toho je spojí v daném pořadí
                //rekurzivně skládá pointry, půlící linked list, doku nebudou složeny ve správném pořadí
                List<int> newList = new List<int>();
                int j = 0;
                int k = 0;
                for (int i = 0; i < firstList.Count + secondList.Count; i++)
                {
                    if (j < firstList.Count && (secondList.Count <= k || firstList[j] < secondList[k]))
                    {
                        newList.Add(firstList[j]);
                        j++;
                    }
                    else if (k < secondList.Count)
                    {
                        newList.Add(secondList[k]);
                        k++;
                    }
                }
                return newList;

            }
            public static List<int> MergeSort_3(List<int> list)
            {

                if (list == null || list.Count < 3)//Zakončí rekurzivni smyčku když už nemůže podlisty dál rozdělovat
                {
                    return list;
                }
                int mid1 = Convert.ToInt32(list.Count / 3); //Najde kde je pointer pro střed seznamu a tim ho rozdělí
                int mid2 = Convert.ToInt32(2 * list.Count / 3);
                List<int> firstList = new List<int>();
                List<int> secondList = new List<int>();
                List<int> thirdList = new List<int>();
                for (int i = 0; i < list.Count; i++)
                {
                    if (i < mid1)
                    {
                        firstList.Add(list[i]);
                    }
                    else if (i < mid1)
                    {
                        secondList.Add(list[i]);
                    }
                    else
                    {
                        thirdList.Add(list[i]);
                    }
                }

                firstList = MergeSort_3(firstList); //rekurzivne zavola funkci pro obě nově vznikle poloviny
                secondList = MergeSort_3(secondList);
                thirdList = MergeSort_3(thirdList);


                return Merge_3(firstList, secondList, thirdList); //Nakonec až rozdělí seznam ja jednotlivé prvky, začne je pomocí Merge zpátky skládat
            }
            public static List<int> Merge_3(List<int> firstList, List<int> secondList, List<int> thirdList)
            {
                if (firstList == null) //jeden z důvodů proč se nepouživá trojení je nejspíš že člověk potřebuje nějak vyřešit, když nejde rovným dílem rozdělit původní list, proto musí třeba naprogramovat merge pro o jedna menší (nebo aspoń tak je to řešeno v mém kódu)
                    return Merge_2(secondList, thirdList);
                if (secondList == null)
                    return Merge_2(firstList, thirdList);
                if (thirdList == null)
                    return Merge_2(secondList, firstList);
                //porovná, která z hodnot větší a podle toho je spojí v daném pořadí
                //rekurzivně skládá pointry, půlící linked list, doku nebudou složeny ve správném pořadí
                List<int> newList = new List<int>();
                int j = 0;
                int k = 0;
                int i = 0;

                while ( i < firstList.Count || j < secondList.Count || k < thirdList.Count)
                {
                    int minValue = int.MaxValue, minIdx = -1;

                    // Find the smallest among the three current elements
                    if (i < firstList.Count && firstList[i] < minValue)
                    {
                        minValue = firstList[i];
                        minIdx = 0;
                    }
                    if (j < secondList.Count && secondList[j] < minValue)
                    {
                        minValue = secondList[j];
                        minIdx = 1;
                    }
                    if (k < thirdList.Count && thirdList[k] < minValue)
                    {
                        minValue = thirdList[k];
                        minIdx = 2;
                    }

                    // Place the smallest element in the merged array
                    if (minIdx == 0)
                    {
                        newList.Add(minValue);
                        i++;
                    }
                    else if (minIdx == 1)
                    {
                        newList.Add(minValue);
                        j++;
                    }
                    else
                    {
                        newList.Add(minValue);
                        k++;
                    }
                }

                return newList;

            }
        }
        }
    }
