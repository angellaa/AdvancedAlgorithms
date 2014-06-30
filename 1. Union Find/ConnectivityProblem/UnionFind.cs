namespace ConnectivityProblem
{
    public static class UnionFind
    {
        public enum Type
        {
            QuickFind,
            QuickUnion,
            WeightedQuickUnion,
            QuickUnionPathCompression,
            WeightedQuickUnionPathCompression
        };

        public static IUnionFind Create(Type type, int n)
        {
            switch (type)
            {
                case Type.QuickFind: 
                    return new QuickFind(n);
                
                case Type.QuickUnion: 
                    return new QuickUnion(n);
                
                case Type.WeightedQuickUnion: 
                    return new WeightedQuickUnion(n);
                
                case Type.QuickUnionPathCompression: 
                    return new QuickUnionPathCompression(n);
                
                case Type.WeightedQuickUnionPathCompression:
                default:
                    return new WeightedQuickUnionPathCompression(n);
            }
        }        
    }
}