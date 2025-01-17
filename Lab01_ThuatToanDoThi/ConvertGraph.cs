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
        // Các dạng bài đồ thị chuyển đổi qua lại :
        //1. Ma trận kề -> danh sách cạnh
        //2. Ma trận kề -> danh sách kề
        //3. Danh sách cạnh -> ma trận kề
        //4. Danh sách cạnh -> danh sách kề
        //5. Danh sách kề -> ma trận kề
        //6. Danh sách kề -> danh sách cạnh

        /*Logic chung của mấy bài này đó là đầu tiên phải khởi tạo ra cấu trúc lưu trữ dữ liệu cần thiết 
         Ví dụ như muốn Convert ma trận kề -> danh sách kề thì ta cần khởi tạo ra một List<int>[] AdjacencyList
         sau đó tiến hành duyệt cái cấu trúc gốc ,thêm logic vào là xong 
        */

        //METHOD 01.1 : Ma trận kề -> danh sách cạnh
        public void ConvertMatrixToAdjList()
        {
            AdjacencyList = new List<int>[N]; // Khởi tạo danh sách kề
            for (int i = 0; i < N; i++)
            {
                AdjacencyList[i] = new List<int>(); //Tạo ra các danh sách con 
            }
            //Xây dựng cách mà danh sách kề được tạo ra từ ma trận kề
            //Nói gì thì nói ,vẫn phải duyệt qua từng phần tử của ma trận kề
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (Matrix[i, j] == 1)
                    {
                        AdjacencyList[i].Add(j);
                    }
                }
            }

            //Xây dựng luôn cách in cái danh sách kề đó ra 
            Console.WriteLine(N);
            Console.WriteLine("The Adjacency Matrix -> Adjacency List  : ");
            for (int i = 0; i < N; i++)
            {
                Console.Write($"Vertex {i+1} : ");
                foreach (int value in AdjacencyList[i])
                {
                    Console.Write(value + 1  + " ");
                }
                Console.WriteLine();
            }
        }
        //------------------------------------------------------------------------------------------------------------------
        //METHOD 01.2 : Ma trận kề -> danh sách kề
        public void ConvertMatrixToEdgeList()
        {
            EdgeList = new List<Tuple<int, int>>(); // Khởi tạo danh sách cạnh
            //Duyệt ma trận theo đường chéo chính ( vì ma trận có tính đối xứng ) 
            for ( int i = 0; i < N; i++)
            {
                for (int j = i + 1 ; j < N; j++) //Duyệt theo đường chéo chính nhưng tịnh tiến j lên 1 đơn vị ( Đỡ mất công lặp ) 
                {
                    if (Matrix[i, j] == 1)
                    {
                        EdgeList.Add(new Tuple<int, int>(i + 1 , j + 1 ));
                    }
                }
            }

            //Xây dựng luôn cách in cái danh sách cạnh đó ra
            Console.WriteLine(N);
            Console.WriteLine("The Adjacency Matrix -> Edge List  : ");
            foreach (Tuple<int, int> tuple in EdgeList)
            {
                Console.WriteLine($"{tuple.Item1}, {tuple.Item2}");
            }
            Console.WriteLine();
        }
        //------------------------------------------------------------------------------------------------------------------
        //METHOD 02.1 : Danh sách cạnh -> ma trận kề
        public void ConvertEdgeListToMatrix()
        {
            //Khởi tạo Matrix với kích cỡ N x N 
           Matrix = new int[N, N];
          
           foreach( Tuple<int, int> tuple in EdgeList)
            {
                Matrix[tuple.Item1 - 1, tuple.Item2 - 1] = 1;
                Matrix[tuple.Item2 - 1, tuple.Item1 - 1] = 1;
            }
            //Xây dựng luôn cách in cái ma trận kề đó ra
            Console.WriteLine(N);
            Console.WriteLine("The Edge List -> Adjacency Matrix  : ");
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    Console.Write(Matrix[i, j] + " ");
                }
                Console.WriteLine();
            }
        }
        //------------------------------------------------------------------------------------------------------------------
        //METHOD 02.2 : Danh sách cạnh -> danh sách kề
        public void ConvertEdgeListToAdjList()
        {
            AdjacencyList = new List<int>[N]; // Khởi tạo danh sách kề
            for(int i = 0; i < N; i++)
            {
                AdjacencyList[i] = new List<int>(); //Tạo ra các danh sách con 
            }
            foreach ( Tuple<int, int> tuple in EdgeList) //Duyệt các cạnh bên trong EdgeList 
            {
                AdjacencyList[tuple.Item1 - 1].Add(tuple.Item2 );
                AdjacencyList[tuple.Item2 - 1].Add(tuple.Item1 );

            }
            //Xây dựng luôn cách in cái danh sách kề đó ra
            Console.WriteLine(N);
            Console.WriteLine("The Edge List -> Adjacency List  : ");
            for (int i = 0; i < N; i++)
            {
                Console.Write($"Vertex {i + 1} : ");
                foreach (int value in AdjacencyList[i])
                {
                    Console.Write(value + " ");
                }
                Console.WriteLine();
            }
        }
        //------------------------------------------------------------------------------------------------------------------
        //METHOD 03.1 : Danh sách kề -> ma trận kề
        public void ConvertAdjListToMatrix()
        {
            Matrix = new int[N, N]; //Khởi tạo ma trận kề với kích cỡ N x N
            for ( int i = 0; i < N;i++)
            {
                foreach ( int value in AdjacencyList[i])
                {
                    Matrix[i, value-1] = 1;
                    Matrix[value-1, i] = 1; //Vì ma trận kề là ma trận đối xứng
                }
            }
            //Xây dựng luôn cách in cái ma trận kề đó ra
            Console.WriteLine(N);
            Console.WriteLine("The Adjacency List -> Adjacency Matrix  : ");
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    Console.Write(Matrix[i, j] + " ");
                }
                Console.WriteLine();
            }

        }
        //------------------------------------------------------------------------------------------------------------------
        //METHOD 03.2 : Danh sách kề -> danh sách cạnh
        public void ConvertAdjListToEdgeList()
        {
            EdgeList = new List<Tuple<int, int>>(); // Khởi tạo danh sách cạnh
            for ( int i = 0 ;i < N ;i++ )
            {
                foreach(int value in AdjacencyList[i])
                {
                    if ( i + 1 > value )
                    {
                        continue;
                    }
                    else
                    {
                        EdgeList.Add(new Tuple<int, int>(i + 1, value));
                    }
                    
                }
            }
            //Xây dựng luôn cách in cái danh sách cạnh đó ra
            Console.WriteLine(N);
            Console.WriteLine("The Adjacency List -> Edge List  : ");
            foreach (Tuple<int, int> tuple in EdgeList)
            {
                Console.WriteLine($"{tuple.Item1}, {tuple.Item2}");
            }
        }

    }

}
