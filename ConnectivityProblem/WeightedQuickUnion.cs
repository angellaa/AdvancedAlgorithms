using System.Linq;

namespace ConnectivityProblem
{
    public class WeightedQuickUnion : IUnionFind
    {
        private readonly int[] parent;
        private readonly int[] size;

        public WeightedQuickUnion(int n)
        {
            parent = Enumerable.Range(0, n).ToArray();
            size = new int[n];
        }

        public bool AreConnected(int p, int q)
        {
            return Root(p) == Root(q);
        }

        public void Connect(int p, int q)
        {
            var rootP = Root(p);
            var rootQ = Root(q);

            if (rootP == rootQ) return;

            if (size[p] < size[q])
            {
                parent[rootP] = rootQ;
                size[q] += size[p];
            }
            else
            {
                parent[rootQ] = rootP;
                size[p] += size[q];
            }
        }

        private int Root(int p)
        {
            while (parent[p] != p)
                p = parent[p];

            return p;
        }
    }
}