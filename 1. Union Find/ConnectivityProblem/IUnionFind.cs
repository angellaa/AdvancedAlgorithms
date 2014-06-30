namespace ConnectivityProblem
{
    public interface IUnionFind
    {
        bool AreConnected(int p, int q);  // Find
        void Connect(int p, int q);       // Union
    }
}