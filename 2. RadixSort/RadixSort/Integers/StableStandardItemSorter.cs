using System.Linq;

namespace RadixSort
{
    public class StableStandardItemSorter : IItemSort
    {
        public void Sort(ref Item[] items)
        {
            items = items.OrderBy(x => x.Id).ToArray();
        }
    }
}