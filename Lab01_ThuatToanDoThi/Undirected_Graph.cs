using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data;
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
        private int m;     //Số cạnh của đồ thị hiện tại
        private int[,] matrix; //Ma trận lưu trữ những gì nó đọc được từ file data.txt ( trong thư mục Data ) 
        private List<int>[] adjacencyList; //dạng List chứa các danh sách con (List trong List )
        private List<int>[] edgeList; //Danh sách cạnh của đồ thị, vấn đề ở đây là nó chưa biết trước được số cạnh của đồ thị
                                      //Có 1 điểm khá thú vị đó là nếu số đỉnh lại nhiều hơn số cạnh 2 đơn vị trở lên thì chắc chắn đồ thị có chứa đỉnh cô lập 
                                      //Ví dụ : A-B C : Có 3 đỉnh nhưng chỉ có 1 cạnh nối 2 đỉnh A và B ,đỉnh C là đỉnh cô lập 
                                      //Còn nếu số cạnh chỉ ít hơn số đỉnh 1 đơn vị thì đồ thị không chứa đỉnh cô lập ,nó sẽ là đồ thị liên thông mạnh 
        //Properties : Có cũng được không có cũng chả sao 
        public int N
        {
            set { n = value; }
            get { return n; }
        }
        public int M
        {
            set { m = value; }
            get { return m; }
        }
    
        public int[,] Matrix
        {
            get { return matrix; }
            set { matrix = value; }
        }
        public List<int>[] AdjacencyList
        {
            get { return adjacencyList; }
            set { adjacencyList = value; }
        }
        public List<int>[] EdgeList
        {
            get { return edgeList; }
            set { edgeList = value; }
        }


        //---------------------------------------------------------------------------------------------------------------------
        //Contructors 
        public Undirected_Graph(int n)
        {
            N = n;
            matrix = new int[n, n];
        }

        public Undirected_Graph()
        {
            n = 0;
            matrix = new int[Max, Max];    
        }


        //---------------------------------------------------------------------------------------------------------------------
        //Method xử lý dữ liệu đọc được từ file

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
        public void ReadEdgeList(string filePath)
        {
            try
            {
                string[] lines = File.ReadAllLines(filePath);
                string[] edgeListInfo = lines[0].Split(' ');  //Tách chuỗi ở dòng đầu tiên ra thành từng kí tự ,nhằm mục đích lấy thông tin về số cạnh và số đỉnh 
                n = int.Parse(edgeListInfo[0]);  //Lấy số đỉnh
                m = int.Parse(edgeListInfo[1]);  //Lấy số cạnh

                edgeList = new List<int>[m];  //Tạo ra 1 List cha để chứa các List con
                for (int i = 0; i < m; i++)
                {
                    edgeList[i] = new List<int>();  //Khởi tạo List con
                }
                
                for (int i = 1; i <= m; i++ )
                {
                    if (!string.IsNullOrWhiteSpace(lines[i]))  //nếu chuỗi đang xét là không rỗng 
                    {
                        string[] datas = lines[i].Split(' ');  //Tách chuỗi ra thành từng kí tự
                        foreach (string data in datas)  //Duyệt qua từng kí tự trong datas 
                        {
                            edgeList[i - 1].Add(int.Parse(data));  //Ép kiểu và thêm vào List con
                        }
                    }
                }


            }
            catch(Exception e)
            {
                Console.WriteLine("There some troubles while reach to file, make sure your file path is correct :)  \n \n " + e.Message);
            }

        }


        //-----------------------------------------------------------------------------------------------------------------------
        //Method Print : Dùng để check xem dữ liệu đã đọc được từ file có đúng không
        //METHOD 02.1 : In ma trận ra màn hình console
        public void PrintMatrix()
        {
            Console.WriteLine("The Matrix Input");
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

            for (int i = 0; i < n; i++)  //Duyệt danh sách adjacencyList  
            {
                
                foreach(int value in adjacencyList[i])
                {
                    Console.Write(value + " ");
                }
               
                Console.WriteLine() ;                 
            }
        }

        //METHOD 02.3 : In danh sách cạnh ra màn hình console 
        public void PrintEdgeList()
        {
            Console.WriteLine("The Edge List Input");
            Console.WriteLine(n + " " + m);  // Print the number of vertices and edges

            // test debug 
            if (edgeList.Length < m )
            {
                Console.WriteLine("Error: edgeList does not contain enough elements.");
                return;
            }

            for (int i = 0; i < m; i++)
            {
                foreach (int value in edgeList[i])  
                {
                    Console.Write(value + " ");
                }
                Console.WriteLine();
            }
          
            
        }
        //---------------------------------------------------------------------------------------------------------------------
        //METHOD 03.1 : In ra số bậc của mỗi đỉnh trong ma trận 
        public void AdjacencyMatrixDegree ()
        {

            //Duyệt ma trận ( không có số đỉnh nha ,nếu muốn thì nhét N vô ) 
            Console.WriteLine("The Matrix Output");
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
        //METHOD 03.3 : In ra bậc của mỗi đỉnh ( danh sách cạnh - Edge List ) 
        public void EdgeListDegree()
        {
            Console.WriteLine("The Edge List Output");
            Console.WriteLine(n);

            Dictionary<int, int> DegreeDict = CalculatorDegree(); 
            foreach(var data in DegreeDict)
            {
                Console.WriteLine($"Đỉnh: {data.Key}, Bậc: {data.Value}");
            }

            Console.WriteLine();
        }

        //METHOD 03.3.1 : Phương thức tính bậc của mỗi đỉnh 
        private Dictionary<int, int> CalculatorDegree()
        {
            Dictionary<int, int> degreeDict = new Dictionary<int, int>(); //Khởi tạo 1 Dictionary để lưu trữ bậc của mỗi đỉnh
            //Trong đó value sẽ đại diện cho đỉnh và key sẽ đại diện cho bậc của đỉnh đó
            
            for ( int i = 0;i < m; i++)  //List edgeList vẫn chứa các List con nên vẫn phải duyệt 2 lần thôi 
            {
                foreach (int value in edgeList[i])
                {
                    if (degreeDict.ContainsKey(value))  //Nếu Dictionary đã chứa key đó rồi thì tăng giá trị của key đó lên 1 
                    {
                        degreeDict[value]++;
                    }
                    else if  (!degreeDict.ContainsKey(value))   //Nếu Dictionary chưa chứa key đó thì thêm vào 
                    {
                        degreeDict.Add(value, 1);  //        
                    }
                }
            }
            return degreeDict;
        }

        //---------------------------------------------------------------------------------------------------------------------

    }
}
