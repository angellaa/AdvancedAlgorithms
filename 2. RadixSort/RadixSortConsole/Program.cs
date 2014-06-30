using System;
using System.Collections.Generic;
using RadixSort;

namespace RadixSortConsole
{
    class Program
    {
        static void Main()
        {
            StandardVsCountingSort();
            //StableSorting();
            //StandardVsStableCountingSort();
            //StandardVsLCDRadixSort();

            Console.ReadKey();
        }

        private static void StableSorting()
        {
            var list = new List<Item>();

            for (int i = 4; i > 0; i--)
                for (int j = 1; j <= 5; j++)
                    list.Add(new Item(i, j));                    

            PrintItems("Original", list);

            var items = list.ToArray();
            new StandardItemSorter().Sort(ref items);
            PrintItems("Standard Item Sorter", items);

            items = list.ToArray();
            new StableStandardItemSorter().Sort(ref items);
            PrintItems("Stable Standard Item Sorter", items);
        }

        private static void StandardVsCountingSort()
        {
            const int max = 100;

            Console.WriteLine("\nMAX = {0}\n", max);

            for (int n = 1000000; n <= 100000000; n *= 10)
            {
                Console.Write("N = {0, 10}\t", n);

                Utils.MeasureIntSorting(n, max, "Standard", new StandardIntSorter());
                Utils.MeasureIntSorting(n, max, "Counting", new CountingSort(max));

                Console.WriteLine();
            }
        }

        private static void StandardVsLCDRadixSort()
        {
            for (int n = 1000000; n <= 100000000; n *= 10)
            {
                Console.Write("N = {0, 10}\t", n);

                Utils.MeasureIntSorting(n, int.MaxValue, "Standard", new StandardIntSorter());
                Utils.MeasureIntSorting(n, int.MaxValue, "LCD Radix", new LSDRadixSort());

                Console.WriteLine();
            }
        }

        private static void StandardVsStableCountingSort()
        {
            const int max = 1000000;

            Console.WriteLine("\nMAX = {0}\n", max);

            for (int n = 1000000; n <= 10000000; n *= 10)
            {
                Console.Write("N = {0, 10}\t", n);

                Utils.MeasureItemSorting(n, max, "Stable Standard", new StableStandardItemSorter());
                Utils.MeasureItemSorting(n, max, "Index Counting", new IndexCountingSort(max));

                Console.WriteLine();
            }
        }

        private static void PrintItems(string title, IEnumerable<Item> items)
        {
            Console.WriteLine(title);
            Console.WriteLine("Id     Value");

            foreach (var item in items)
            {
                Console.WriteLine("{0,-5}  {1,-5}", item.Id, item.Value);
            }

            Console.WriteLine();
        }
    }
}
