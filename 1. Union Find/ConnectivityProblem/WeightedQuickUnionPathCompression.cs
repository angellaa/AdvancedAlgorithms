using System;
using System.Linq;
using System.Threading.Tasks;

namespace ConnectivityProblem
{
    public class WeightedQuickUnionPathCompression : IUnionFind
    {
        private readonly int[] parent;
        private readonly byte[] height;

        public WeightedQuickUnionPathCompression(int n)
        {
            parent = Enumerable.Range(0, n).ToArray();
            height = Enumerable.Repeat((byte)1, n).ToArray();
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

            if (height[rootP] < height[rootQ])
            {
                parent[rootP] = rootQ;
            }
            else if (height[rootP] > height[rootQ])
            {
                parent[rootQ] = rootP;
            }
            else
            {
                parent[rootQ] = rootP;
                height[rootP]++;
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

            Parallel.For(0, parent.Length, i =>
            {
                if (Root(i) == pRoot)
                    action(i);
            });
        }
    }
}