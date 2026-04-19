  using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
namespace NejdelsiPosloupnost
{

    class Program
    {
        static void Main()
        {
            var sequences = LoadSequences("vstupy.txt");

            foreach (var seq in sequences)
            {
                if (seq == null)
                {
                Console.WriteLine("Prázdná posloupnost.");
                    continue;
                }
                var lis = LongestIncreasingSubsequence(seq);
                Console.WriteLine(string.Join(" ", lis));
            }
        }

        static List<int> LongestIncreasingSubsequence(List<int> arr)
        {
            int n = arr.Count;

            if (n == 0) return new List<int>();
            if (n == 1) return new List<int> { arr[0] };

            int[] dp = new int[n];
            int[] prev = new int[n];

            for (int i = 0; i < n; i++)
            {
                dp[i] = 1;
                prev[i] = -1;

                for (int j = 0; j < i; j++)
                {
                    if (arr[j] < arr[i] && dp[j] + 1 > dp[i])
                    {
                        dp[i] = dp[j] + 1;
                        prev[i] = j;
                    }
                }
            }

            // najdi maximum
            int maxLen = dp.Max();
            int index = Array.IndexOf(dp, maxLen);

            // rekonstrukce
            List<int> result = new List<int>();
            while (index != -1)
            {
                result.Add(arr[index]);
                index = prev[index];
            }

            result.Reverse();
            return result;
        }

        static List<List<int>> LoadSequences(string path)
        {
            var lines = File.ReadAllLines(path);
            var result = new List<List<int>>(); //zpracovenej dokument na list listů čisel
            var current = new List<int>(); //řádek z původního dokumentu, kterej zrovna konvertuej

            var prevblank = false;
            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))//Nějakej expression z stackoverflow, snad to nebude dělat guláš
                {
                    if (current.Count > 0)
                    {
                        result.Add(new List<int>(current));
                        current.Clear();
                        prevblank = true;
                    }
                    else if(prevblank)//prázdný posloupnosti
                    {
                        result.Add(null);
                        prevblank = false;
                    }
                }
                else
                {
                    current.AddRange(line.Split(' ').Select(int.Parse));
                }
            }

            if (current.Count > 0)
                result.Add(current);

            return result;
        }
    }
}
