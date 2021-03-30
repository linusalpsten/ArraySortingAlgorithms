using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArraySortingAlgorithms
{
    static class Ext
    {
        private static Random rng = new Random();
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static void Shuffle<T>(this IList<T> list, IEnumerable<int> IgnoreIndex)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                if (IgnoreIndex.Contains(n))
                {
                    continue;
                }
                int k = rng.Next(n + 1);
                while (IgnoreIndex.Contains(k))
                {
                    k = rng.Next(n + 1);
                }
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static string ToString<T>(this IEnumerable<T> list)
        {
            string s = "[";
            s += string.Join(",", list) + "]";
            return s;
        }
    }
    class Comp : IComparer<int>
    {
        public int Compare(int x, int y) //negative means y is greater, 0 means equal, positive means x is greater
        {
            return x - y;
        }
    }
    static class Helper
    {
        private static DateTime benchmarkTime = DateTime.Now;
        public static void Write<T>(IEnumerable<T> sorted, string method, IComparer<T> comparer, TimeSpan time)
        {
            Console.WriteLine($"{method}");
            Console.WriteLine($"Time: {time}");
            Console.WriteLine($"Is Sorted: {IsSorted<T>(sorted, comparer)}");
            //Console.WriteLine(sorted.ToString<T>());
            Console.WriteLine();
        }

        public static bool IsSorted<T>(IEnumerable<T> list, IComparer<T> comparer)
        {
            bool isSorted = true;
            for (int i = 0; i < list.Count() - 1; i++)
            {
                isSorted = isSorted & comparer.Compare(list.ElementAt(i), list.ElementAt(i + 1)) <= 0;
            }
            return isSorted;
        }

        public static IEnumerable<T> Merge<T>(IEnumerable<T> Sorted1, IEnumerable<T> Sorted2, IComparer<T> comparer)
        {
            T[] merged = new T[Sorted1.Count() + Sorted2.Count()];
            int mergedIndex = 0;

            int i1 = 0;
            int i2 = 0;
            while (i1 != Sorted1.Count() || i2 != Sorted2.Count())
            {
                if (i1 < Sorted1.Count())
                {
                    if (i2 < Sorted2.Count())
                    {
                        if (comparer.Compare(Sorted1.ElementAt(i1), Sorted2.ElementAt(i2)) < 0)
                        {
                            merged[mergedIndex] = Sorted1.ElementAt(i1);
                            mergedIndex++;
                            i1++;
                        }
                        else
                        {
                            merged[mergedIndex] = Sorted2.ElementAt(i2);
                            mergedIndex++;
                            i2++;
                        }
                    }
                    else
                    {
                        merged[mergedIndex] = Sorted1.ElementAt(i1);
                        mergedIndex++;
                        i1++;
                    }
                }
                else
                {
                    merged[mergedIndex] = Sorted2.ElementAt(i2);
                    mergedIndex++;
                    i2++;
                }
            }
            return merged;
        }

        public static (IEnumerable<T> part1, IEnumerable<T> part2) Partition<T>(IEnumerable<T> list, int pi, IComparer<T> comparer)
        {
            int p1l = 0;
            int p2l = 0;
            for (int i = 0; i < list.Count(); i++)
            {
                int c = comparer.Compare(list.ElementAt(i), list.ElementAt(pi));
                if (c < 0)
                {
                    p1l++;
                }
                else if (c > 0)
                {
                    p2l++;
                }
            }
            T[] part1 = new T[p1l];
            int p1i = 0;
            T[] part2 = new T[p2l];
            int p2i = 0;

            for (int i = 0; i < list.Count(); i++)
            {
                int c = comparer.Compare(list.ElementAt(i), list.ElementAt(pi));
                if (c < 0)
                {
                    part1[p1i] = list.ElementAt(i);
                    p1i++;
                }
                else if (c > 0)
                {
                    part2[p2i] = list.ElementAt(i);
                    p2i++;
                }
            }

            return (part1, part2);
        }

        public static void StartBenchmark()
        {
            benchmarkTime = DateTime.Now;
        }

        public static TimeSpan StopBenchmark()
        {
            return DateTime.Now - benchmarkTime;
        }
    }
    class Program
    {
        private static Random rng = new Random();
        static void Main(string[] args)
        {
            MainAsync().GetAwaiter().GetResult();

        }
        private static async Task MainAsync()
        {
            int[] list = Enumerable.Range(1, 500000).ToArray();
            list.Shuffle();
            var comparer = new Comp();

            //Helper.StartBenchmark();
            //var selectionSorted = SelectionSort(list, comparer);
            //TimeSpan selectionTime = Helper.StopBenchmark();
            //Helper.Write(selectionSorted, nameof(SelectionSort), comparer, selectionTime);

            //Helper.StartBenchmark();
            //var bubbleSorted = BubbleSort(list, comparer);
            //TimeSpan bubbleTime = Helper.StopBenchmark();
            //Helper.Write(bubbleSorted, nameof(BubbleSort), comparer, bubbleTime);

            //Helper.StartBenchmark();
            //var recursiveBubbleSorted = RecursiveBubbleSort(list, comparer);
            //TimeSpan recursiveBubbleTime = Helper.StopBenchmark();
            //Helper.Write(recursiveBubbleSorted, nameof(RecursiveBubbleSort), comparer, recursiveBubbleTime);

            //Helper.StartBenchmark();
            //var insertionSorted = InsertionSort(list, comparer);
            //TimeSpan insertionTime = Helper.StopBenchmark();
            //Helper.Write(insertionSorted, nameof(InsertionSort), comparer, insertionTime);

            //Helper.StartBenchmark();
            //var bogoSorted = BogoSort(list, comparer);
            //TimeSpan bogoTime = Helper.StopBenchmark();
            //Helper.Write(bogoSorted, nameof(BogoSort), comparer, bogoTime);

            //Helper.StartBenchmark();
            //var joelSpecial = JoelsBogoSortSpecial(list, comparer);
            //TimeSpan joelBogoTime = Helper.StopBenchmark();
            //Helper.Write(joelSpecial, nameof(JoelsBogoSortSpecial), comparer, joelBogoTime);

            Helper.StartBenchmark();
            var mergeSorted = await MergeSort(list, comparer);
            TimeSpan mergeTime = Helper.StopBenchmark();
            Helper.Write(mergeSorted, nameof(MergeSort), comparer, mergeTime);

            Helper.StartBenchmark();
            var quickSorted = await QuickSort(list, comparer);
            TimeSpan quickTime = Helper.StopBenchmark();
            Helper.Write(quickSorted, nameof(QuickSort), comparer, quickTime);
        }

        static IEnumerable<T> SelectionSort<T>(IEnumerable<T> list, IComparer<T> comparer)
        {
            T[] sorted = new T[list.Count()];
            int sortedIndex = 0;

            int[] indexLookup = new int[list.Count()];
            for (int i = 0; i < indexLookup.Length; i++)
            {
                indexLookup[i] = -1;
            }
            int lookupIndex = 0;

            while (lookupIndex != list.Count())
            {
                int firstAvailableIndex = 0;
                for (int j = 0; j < list.Count(); j++)
                {
                    if (!indexLookup.Contains(j))
                    {
                        firstAvailableIndex = j;
                        break;
                    }
                }
                T n = list.ElementAt(firstAvailableIndex);
                int i = firstAvailableIndex;
                for (int j = 0; j < list.Count(); j++)
                {
                    if (indexLookup.Contains(j))
                    {
                        continue;
                    }
                    if (comparer.Compare(n, list.ElementAt(j)) > 0)
                    {
                        n = list.ElementAt(j);
                        i = j;
                    }
                }
                sorted[sortedIndex] = n;
                sortedIndex++;

                indexLookup[lookupIndex] = i;
                lookupIndex++;
            }

            return sorted;
        }
        static IEnumerable<T> BubbleSort<T>(IEnumerable<T> list, IComparer<T> comparer)
        {
            T[] copy = list.Select(i => i).ToArray();

            bool sorted = false;
            while (!sorted)
            {
                sorted = true;
                for (int i = 0; i < copy.Length - 1; i++)
                {
                    bool swap = comparer.Compare(copy[i], copy[i + 1]) > 0;
                    sorted = sorted & !swap;
                    if (swap)
                    {
                        T v = copy[i];
                        copy[i] = copy[i + 1];
                        copy[i + 1] = v;
                    }
                }
            }

            return copy;
        }
        static IEnumerable<T> RecursiveBubbleSort<T>(IEnumerable<T> list, IComparer<T> comparer)
        {
            if (list.Count() <= 1)
            {
                return list;
            }
            T[] copy = list.Select(i => i).ToArray();
            for (int i = 0; i < copy.Length - 1; i++)
            {
                bool swap = comparer.Compare(copy[i], copy[i + 1]) > 0;
                if (swap)
                {
                    T v = copy[i];
                    copy[i] = copy[i + 1];
                    copy[i + 1] = v;
                }
            }
            var sorted = RecursiveBubbleSort(copy.Take(copy.Length - 1).ToArray(), comparer);
            for (int i = 0; i < sorted.Count(); i++)
            {
                copy[i] = sorted.ElementAt(i);
            }
            return copy;
        }
        static IEnumerable<T> InsertionSort<T>(IEnumerable<T> list, IComparer<T> comparer)
        {
            T[] copy = list.Select(i => i).ToArray();

            for (int i = 1; i < copy.Length; i++)
            {
                T n = copy[i];
                int j = i - 1;
                while (comparer.Compare(copy[j], n) >= 0 && j != 0)
                {
                    j--;
                }
                if (comparer.Compare(copy[j], n) <= 0)
                {
                    if (j + 1 != i)
                    {
                        for (int k = i - 1; k >= j + 1; k--)
                        {
                            copy[k + 1] = copy[k];
                        }
                        copy[j + 1] = n;
                    }
                }
                else
                {
                    for (int k = i - 1; k >= j; k--)
                    {
                        copy[k + 1] = copy[k];
                    }
                    copy[j] = n;
                }
            }
            return copy;
        }
        static IEnumerable<T> BogoSort<T>(IList<T> list, IComparer<T> comparer)
        {
            IList<T> sorted = list.Select(i => i).ToArray();

            if (list.Count() > 10)
            {
                return sorted;
            }
            while (!Helper.IsSorted(sorted, comparer))
            {
                sorted.Shuffle();
            }
            return sorted;
        }
        static IEnumerable<int> JoelsBogoSortSpecial(IList<int> list, IComparer<int> comparer)
        {
            IList<int> sorted = list.Select(i => i).ToArray();
            int[] ignoredIndexes = new int[list.Count];
            int indexIgnored = 0;
            for (int i = 0; i < ignoredIndexes.Length; i++)
            {
                ignoredIndexes[i] = -1;
            }
            while (!Helper.IsSorted(sorted, comparer))
            {
                for (int i = 0; i < sorted.Count(); i++)
                {
                    if (sorted[i] == i + 1)
                    {
                        if (!ignoredIndexes.Contains(i))
                        {
                            ignoredIndexes[indexIgnored] = i;
                            indexIgnored++;
                        }
                    }
                }
                sorted.Shuffle(ignoredIndexes);
            }
            return sorted;
        }
        async static Task<IEnumerable<T>> MergeSort<T>(IEnumerable<T> list, IComparer<T> comparer)
        {
            if (list.Count() <= 1)
            {
                return list;
            }
            int m = list.Count() / 2;
            var l1 = MergeSort(list.Take(m), comparer);
            var l2 = MergeSort(list.Skip(m).Take(list.Count() - m), comparer);
            return Helper.Merge(await l1, await l2, comparer);
        }
        async static Task<IEnumerable<T>> QuickSort<T>(IEnumerable<T> list, IComparer<T> comparer)
        {
            if (list.Count() <= 1)
            {
                return list;
            }

            T[] sorted = new T[list.Count()];
            int si = 0;
            int pi = 0;
            var partition = Helper.Partition(list, pi, comparer);
            var part1 = QuickSort(partition.part1, comparer);
            var part2 = QuickSort(partition.part2, comparer);
            foreach (var item in await part1)
            {
                sorted[si] = item;
                si++;
            }
            sorted[si] = list.ElementAt(pi);
            si++;
            foreach (var item in await part2)
            {
                sorted[si] = item;
                si++;
            }
            return sorted;
        }
    }
}
