using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab01_ThuatToanDoThi
{
    internal class ConvertGraph : Undirected_Graph
    {
        // Kế thừa Fields ,Properties ,Constructor từ Undirected Graph
        public ConvertGraph() : base() { }
        public ConvertGraph(int n) : base(n) { }

        //--------------------------------------------------------------------------------
        // Method 
        // METHOD 01 : Chuyển đổi danh sách cạnh thành danh sách kề 
        public void ConvertEdgeToAdjList()
        {
            // Khởi tạo danh sách kề
            AdjacencyList = new List<int>[N];
            for (int i = 0; i < N; i++)
            {
                AdjacencyList[i] = new List<int>();
            }

            // Duyệt qua từng cạnh trong danh sách cạnh và thêm các đỉnh kề vào danh sách kề
            foreach (var edge in EdgeList)
            {
                int u = edge[0] - 1; // Giảm đi 1 để tương thích với chỉ số từ 0 của mảng
                int v = edge[1] - 1; // Giảm đi 1 để tương thích với chỉ số từ 0 của mảng

                AdjacencyList[u].Add(v + 1); // Thêm đỉnh v vào danh sách kề của đỉnh u
                AdjacencyList[v].Add(u + 1); // Thêm đỉnh u vào danh sách kề của đỉnh v         
            }

            // In danh sách kề ra màn hình console
            Console.WriteLine("The Edge List to Adjacency List:");
            Console.WriteLine(N); // In ra số đỉnh của đồ thị

            for (int i = 0; i < N; i++)
            {
                Console.Write($"Đỉnh {i + 1}: ");
                foreach (int value in AdjacencyList[i])
                {
                    Console.Write(value + " ");
                }
                Console.WriteLine();
            }
        }


        //METHOD 02 : Chuyển đổi danh sách cạnh thành danh sách kề 
        public void ConvertAdjListToEdgeList()
        {
            

            // In danh sách cạnh ra màn hình console
            Console.WriteLine("The Adjacency List to Edge List:");
            foreach (var edge in EdgeList)
            {
                Console.WriteLine($"{edge[0]} {edge[1]}");
            }
        }

    }

}
