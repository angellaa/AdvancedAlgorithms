using ConnectivityProblem;
using NUnit.Framework;

namespace ConnectivityProblemTests
{
    [TestFixture(UnionFind.Type.QuickFind)]
    [TestFixture(UnionFind.Type.QuickUnion)]
    [TestFixture(UnionFind.Type.QuickUnionPathCompression)]
    [TestFixture(UnionFind.Type.WeightedQuickUnion)]
    [TestFixture(UnionFind.Type.WeightedQuickUnionPathCompression)]
    public class ConnectivityTests
    {
        private readonly UnionFind.Type type;
        private IUnionFind unionFind;

        public ConnectivityTests(UnionFind.Type type)
        {
            this.type = type;
        }

        [SetUp]
        public void SetUp()
        {
            unionFind = UnionFind.Create(type, 5);

            unionFind.Connect(0, 1);
            unionFind.Connect(1, 3);

            unionFind.Connect(2, 4);                
        }

        [Test, Combinatorial]
        public void AreConnected([Values(0, 1, 3)] int p, [Values(0, 1, 3)] int q)
        {
            Assert.IsTrue(unionFind.AreConnected(p, q));
        }

        [Test, Combinatorial]
        public void AreNotConnected([Values(0, 1, 3)] int p, [Values(2, 4)] int q)
        {
            Assert.IsFalse(unionFind.AreConnected(p, q));
        }
    }
}