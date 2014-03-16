using System;
using System.Diagnostics;

namespace ConnectivityProblem
{
    class Program
    {
        static readonly Stopwatch Timer = new Stopwatch();
        static readonly Random Random = new Random((int)DateTime.UtcNow.Ticks);

        const int MinN = 10;
        const int MaxN = 100000000;
        const UnionFind.Type Type = UnionFind.Type.WeightedQuickUnionPathCompression;

        static void Main()
        {
            for (int n = MinN; n <= MaxN; n *= 10)
            {
                var unionFind = UnionFind.Create(Type, n);

                CleanMemory();

                Console.Write("N = {0, 10}\t", n);

                Measure("    Connect()", () =>
                {
                    for (int i = 0; i < n; i++)
                    {
                        var p = Random.Next(n);
                        var q = Random.Next(n);

                        unionFind.Connect(p, q);
                    }
                });

                Measure("AreConnected()", () =>
                {
                    for (int i = 0; i < n; i++)
                    {
                        var p = Random.Next(n);
                        var q = Random.Next(n);

                        unionFind.AreConnected(p, q);
                    }
                });

                Console.WriteLine();
            }

            Console.WriteLine("\nEnd");
            Console.ReadKey();
        }

        private static void CleanMemory()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        private static void Measure(string description, Action action)
        {
            Timer.Restart();

            action();

            Timer.Stop();
            Console.Write("{0} : {1:N3} seconds\t", description, Timer.Elapsed.TotalSeconds);
        }
    }
}
