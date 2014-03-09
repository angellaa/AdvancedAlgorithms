using System;
using System.Linq;

namespace ConnectivityProblem
{
    public class WeightedQuickUnionPathCompression : IUnionFind
    {
        private readonly int[] parent;
        private readonly int[] size;

        public WeightedQuickUnionPathCompression(int n)
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
            {
                parent[p] = parent[parent[p]];
                p = parent[p];
            }

            return p;
        }

        public void ForEachConnected(int p, Action<int> action)
        {
            int pRoot = Root(p);

            for (int i = 0; i < parent.Length; i++)
                if (Root(i) == pRoot)
                    action(i);
        }
    }
}