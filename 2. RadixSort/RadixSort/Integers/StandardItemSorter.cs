using System;

namespace RadixSort
{
    public class StandardItemSorter : IItemSort
    {
        public void Sort(ref Item[] items)
        {
            Array.Sort(items);
        }
    }
}