using System;

namespace RadixSort
{
    public class StandardIntSorter : IIntSort
    {
        public void Sort(ref int[] numbers)
        {
            Array.Sort(numbers);
        }
    }
}