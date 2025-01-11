using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab01_ThuatToanDoThi
{
    internal class Undirected_Graph
    {
        //Fields 
        public const int Max = 10000; //Số đỉnh tối đa của đồ thị 
        private int n;     //Số đỉnh của đồ thị hiện tại 
        private int[,] matrix; //Ma trận lưu trữ những gì nó đọc được từ file data.txt ( trong thư mục Data ) 
        private List<int>[] adjacencyList; //dạng List chứa các danh sách con (List trong List ) 

        //Properties : Có cũng được không có cũng chả sao 
        public int N
        {
            set { n = value; }
            get { return n; }
        }
        public int[,] Matrix
        {
            get { return matrix; }
        }
        public List<int>[] AdjacencyList
        {
            get { return adjacencyList; }
        }


        //---------------------------------------------------------------------------------------------------------------------
        //Contructors 
        public Undirected_Graph(int n)
        {
            N = n;
            matrix = new int[n, n];
            adjacencyList = new List<int>[n];
            for (int i = 0; i < n; i++)
            {
                adjacencyList[i] = new List<int>();
            }
        }

        public Undirected_Graph()
        {
            n = 0;
            matrix = new int[Max, Max];
            adjacencyList = new List<int>[Max]; 
            for (int i = 0; i < Max; i++)
            {
                adjacencyList[i] = new List<int>(); 
            }
        }


        //---------------------------------------------------------------------------------------------------------------------
        //Method

        //METHOD 01.1 : Method đọc dữ liệu từ file Data.txt ,sau đó lưu những gì nó đọc được vào mảng 2 chiều Matrix 
        public void ReadMatrix(string filePath)
        {
            try
            {
                // Tạo 1 mảng lines để lưu trữ từng dòng data mà nó đọc được từ Data.txt 
                string[] lines = File.ReadAllLines(filePath);

                // Khởi tạo lại matrix dựa vào số mà nó đọc được ở dòng đầu tiên 
                n = int.Parse(lines[0]);
                matrix = new int[n, n ];  //Khởi tạo lại matrix 

                //Tách dữ liệu từ mảng lines ra thành từng kí tự và sau đó lưu trữ lại 
                for (int i = 1; i <= n; i++)   //Bắt đầu từ 1 vì dòng 0 đã đọc rồi và nó là số đỉnh của đồ thị 
                {
                    //Tạo 1 mảng mới để lưu trữ từng số liệu ( vì ReadAllLines đọc 1 lèo hết hàng xong nó lưu lại ,chứ nó không thèm tách ra )
                  
                    string[] data = lines[i].Split(' ');

                    //Duyệt và lưu dữ liệu từ mảng data vào ma trận 
                    for (int j = 0; j < n; j++)
                    {
                        matrix[i - 1, j] = int.Parse(data[j]);
                    }

                }
            }
            catch (Exception e)
            {
                //Xử lí lỗi nếu có 
                Console.WriteLine("Error when try to read data from file, please check the file path then try again :) \n \n  "  +  e.Message);
            }


        }
        //METHOD 01.2 : Phương thức xử lý danh sách kề 
         public void ReadAdjacencyList(string filePath)    //Ý tưởng là tạo ra những danh sách List mini bên trong 1 danh sách List lớn
         {
            try
            {
                string[] lines = File.ReadAllLines(filePath);
                n = int.Parse(lines[0]);  //Cập nhật số đỉnh của danh sách 
                adjacencyList = new List<int>[n];  //Tạo ra 1 List cha 
                Console.WriteLine("Số đỉnh : " + n);
                for (int i = 0; i < n; i++)
                {
                    //Qua mỗi vòng lặp ,tạo ra 1 List con ở vị trí thứ i trong List cha 
                    adjacencyList[i] = new List<int>();  //Khởi tạo List con mới ,rỗng và không có kích thước cho trước 
                }

                for (int i = 1; i <= n; i++)
                {
                    if (!string.IsNullOrWhiteSpace(lines[i]))   //Kiểm tra xem chuỗi ở vị trí i trong lines có rỗng hay là khoảng trắng 
                    {
                        string[] datas = lines[i].Split(' '); // thực hiện tách chuỗi sau đó gán vào datas 
                        foreach (string data in datas)   //Tiến hành gán kí tự từ datas vào trong List con của adjacencyList 
                        {
                            adjacencyList[i - 1].Add(int.Parse(data));  //Phải tiến hành ép kiểu vì adjacencyList đang là kiểu số nguyên trong khi data đọc đuọc là string 

                        }
                    }
                }
            }
            catch ( Exception e )
            {
                //Xử lí nếu có lỗi 
                Console.WriteLine("There some troubles while reach to file, make sure your file path is correct :)  \n \n " + e.Message );
            }
         


        }

        //METHOD 01.3 : Phương thức xử lý danh sách cạnh 
        public void EdgeList()
        {

        }


        //-------------------------------------------------------------------------------------------------------------------------
        //METHOD 02.1 : In ma trận ra màn hình console
        public void PrintMatrix()
        {
            Console.WriteLine("The Adjecency Matrix Input");
            Console.WriteLine(n);

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write(matrix[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("\n") ;
        }
        //METHOD 02.2 : In danh sách kề ra màn hình console 
        public void PrintAdjacencyList()
        {
            Console.WriteLine("The Adjacency List Input");
            Console.WriteLine(n); //In đỉnh ra trước 

            for ( int i = 0;i < n; i++)  //Duyệt danh sách adjacencyList  
            {
                foreach(int value in adjacencyList[i])
                {
                    Console.Write(value + " ");
                }
                Console.WriteLine() ;
            }
            
        }

        //METHOD 02.3 : In danh sách cạnh ra màn hình console 
        //---------------------------------------------------------------------------------------------------------------------
        //METHOD 03.1 : In ra số bậc của mỗi đỉnh trong ma trận 
        public void AdjacencyMatrixDegree ()
        {

            //Duyệt ma trận ( không có số đỉnh nha ,nếu muốn thì nhét N vô ) 
            Console.WriteLine("The Adjecency Matrix Output");
            Console.WriteLine (n);
            for (int i = 0; i < n; i++)
            {
                int count = 0;
                for (int j = 0; j < n; j++)
                {
                    if ( matrix[i, j] != 0)   // Vì ma trận đang chứa 0 và 1 
                    {
                        count++;
                    }
                    
                }
                Console.Write(count + " ");
            }
            Console.WriteLine("\n");
        }
        //METHOD 03.2 : In ra bậc của mỗi đỉnh ( danh sách kề - Adjacency List ) 
        public void AdjacencyListDegree()
        {
            Console.WriteLine("The Adjacency List Output ");
            Console.WriteLine(n);

            for (int i = 0; i< n; i ++ )
            {
                int count = adjacencyList[i].Count;
                Console.Write(count + " ");
                
            }
            Console.WriteLine("\n");    
        }
        //---------------------------------------------------------------------------------------------------------------------
        

    }
}
