using System.Linq;

namespace ConnectivityProblem
{
    public class QuickFind : IUnionFind
    {
        private readonly int[] id;

        public QuickFind(int n)
        {
            id = Enumerable.Range(0, n).ToArray();
        }

        public bool AreConnected(int p, int q)
        {
            return id[p] == id[q];
        }

        public void Connect(int p, int q)
        {
            if (AreConnected(p, q)) return;

            int pId = id[p];
            int qId = id[q];

            for (int i = 0; i < id.Length; i++)
                if (id[i] == pId)  
                    id[i] = qId;
        }
    }
}