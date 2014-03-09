using System.Linq;

namespace ConnectivityProblem
{
    public class QuickUnionPathCompression : IUnionFind
    {
        private readonly int[] parent;

        public QuickUnionPathCompression(int n)
        {
            parent = Enumerable.Range(0, n).ToArray();
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

            parent[rootP] = Root(rootQ);
        }

        private int Root(int p)
        {
            while (parent[p] != p)
            {
                parent[p] = parent[parent[p]];
                p = parent[p];
            }

            return p;
        }
    }
}