using System;
using System.Collections.Generic;
namespace Kruskal_s_algorithm
{
    internal class Program
    {


class KruskalAlgorithm
    {
        
        static int Find(int[] parent, int i)
        {
            if (parent[i] == i)
                return i;
            parent[i] = Find(parent, parent[i]); 
            return parent[i];
        }

        
        static void Union(int[] parent, int[] rank, int x, int y)
        {
            int rootX = Find(parent, x);
            int rootY = Find(parent, y);

            if (rootX != rootY)
            {
                // Union by rank
                if (rank[rootX] > rank[rootY])
                    parent[rootY] = rootX;
                else if (rank[rootX] < rank[rootY])
                    parent[rootX] = rootY;
                else
                {
                    parent[rootY] = rootX;
                    rank[rootX]++;
                }
            }
        }

        // Kruskal's Algorithm to find MST
        static List<Tuple<int, int, int>> Kruskal(int V, List<Tuple<int, int, int>> edges)
        {
            // Sort edges by weight
            edges.Sort((e1, e2) => e1.Item3.CompareTo(e2.Item3));

            int[] parent = new int[V];
            int[] rank = new int[V];

            for (int i = 0; i < V; i++)
            {
                parent[i] = i;
                rank[i] = 0;
            }

            List<Tuple<int, int, int>> mst = new List<Tuple<int, int, int>>();
            int mstWeight = 0;

            foreach (var edge in edges)
            {
                int u = edge.Item1;
                int v = edge.Item2;
                int weight = edge.Item3;

                // If u and v are in different sets, include this edge
                if (Find(parent, u) != Find(parent, v))
                {
                    mst.Add(edge);
                    mstWeight += weight;
                    Union(parent, rank, u, v);
                }

                // Stop if we have enough edges
                if (mst.Count == V - 1)
                    break;
            }

            // Output the MST
            Console.WriteLine("Edges in the MST:");
            foreach (var edge in mst)
            {
                Console.WriteLine($"({edge.Item1}, {edge.Item2}) - {edge.Item3}");
            }
            Console.WriteLine($"Total weight of MST: {mstWeight}");

            return mst;
        }

        static void Main()
        {
                Console.Write("Enter the number of vertices: ");
                int V = int.Parse(Console.ReadLine());

                
                Console.Write("Enter the number of edges: ");
                int E = int.Parse(Console.ReadLine());

                List<Tuple<int, int, int>> edges = new List<Tuple<int, int, int>>();

                Console.WriteLine("Enter the edges (format: vertex1 vertex2 weight):");
                for (int i = 0; i < E; i++)
                {
                    string[] edgeData = Console.ReadLine().Split();
                    int u = int.Parse(edgeData[0]);
                    int v = int.Parse(edgeData[1]);
                    int weight = int.Parse(edgeData[2]);
                    edges.Add(Tuple.Create(u, v, weight));
                }
                Kruskal(V, edges);
            }
    }



}
}

