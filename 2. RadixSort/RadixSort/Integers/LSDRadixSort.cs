
namespace RadixSort
{
    public class LSDRadixSort : IIntSort
    {
        public void Sort(ref int[] array)
        {
            var aux = new int[array.Length];

            int max = 255;

            for (int bucket = 0; bucket < 4; bucket++)
            {
                var count = new int[max + 2];
                int shift = bucket * 8;

                foreach (var n in array)
                    count[((n >> shift) & 0xFF) + 1]++;

                for (int i = 0; i < max; i++)
                    count[i + 1] += count[i];

                foreach (var n in array)
                    aux[count[(n >> shift) & 0xFF]++] = n;

                var temp = array;
                array = aux;
                aux = temp;
            }
        }
    }
}