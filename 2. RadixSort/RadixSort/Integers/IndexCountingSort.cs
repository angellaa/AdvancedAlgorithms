namespace RadixSort
{
    public class IndexCountingSort : IItemSort
    {
        private readonly int max;

        public IndexCountingSort(int max)
        {
            this.max = max;
        }

        public void Sort(ref Item[] items)
        {
            var count = new int[max + 2];
            var aux = new Item[items.Length];

            foreach (var item in items)
                count[item.Id + 1]++;

            for (int i = 0; i < max; i++)
                count[i + 1] += count[i];

            foreach (var item in items)
                aux[count[item.Id]++] = new Item(item.Id, item.Value);

            for (int i = 0; i < items.Length; i++)
                items[i] = aux[i];
        }
    }
}
