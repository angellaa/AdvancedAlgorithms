using System;
using System.Diagnostics;
using RadixSort;

namespace RadixSortConsole
{
    public class Utils
    {
        static readonly Stopwatch Timer = new Stopwatch();
        private static readonly Random Random = new Random(Environment.TickCount);

        public static int[] RandomArray(int n, int max = Int32.MaxValue)
        {
            var numbers = new int[n];

            for (int i = 0; i < n; i++)
                numbers[i] = Random.Next(max);

            return numbers;
        }

        public static Item[] RandomItemArray(int n, int max = Int32.MaxValue)
        {
            var items = new Item[n];

            for (int i = 0; i < n; i++)
            {
                items[i] = new Item(Random.Next(max), Random.Next(max));
            }

            return items;
        }

        public static void CheckOrdering(int[] numbers)
        {
            for (int i = 1; i < numbers.Length; i++)
                if (numbers[i] < numbers[i - 1])
                    Debug.Fail("The items is not ordered.");
        }

        public static void CheckOrdering(Item[] items)
        {
            for (int i = 1; i < items.Length; i++)
                if (items[i].Id < items[i - 1].Id)
                    Debug.Fail("The items is not ordered.");
        }

        public static void ClearMemory()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        public static void Measure(string description, Action action)
        {
            Timer.Restart();

            action();

            Timer.Stop();
            Console.Write("{0}: {1:N3}s\t", description, Timer.Elapsed.TotalSeconds);
        }

        public static void MeasureIntSorting(int n, int max, string description, IIntSort sorter)
        {
            ClearMemory();

            var numbers = RandomArray(n, max);

            Measure(description, () => sorter.Sort(ref numbers));
            CheckOrdering(numbers);
        }

        public static void MeasureItemSorting(int n, int max, string description, IItemSort sorter)
        {
            ClearMemory();

            var items = RandomItemArray(n, max);

            Measure(description, () => sorter.Sort(ref items));
            CheckOrdering(items);
        }
    }
}