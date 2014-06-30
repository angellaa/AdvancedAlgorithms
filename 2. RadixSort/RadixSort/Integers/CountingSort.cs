
namespace RadixSort
{
    public class CountingSort : IIntSort
    {
        private readonly int max;

        public CountingSort(int max)
        {
            this.max = max;
        }

        public void Sort(ref int[] numbers)
        {
            var count = new int[max + 1];

            foreach (var n in numbers)
                count[n]++;

            int k = 0;
            for (int n = 0; n <= max; n++)
                for (int i = 0; i < count[n]; i++)
                    numbers[k++] = n;
        }
    }
}