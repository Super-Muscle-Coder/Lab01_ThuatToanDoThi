using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab01_ThuatToanDoThi
{
    internal class Directed_Graph : Undirected_Graph //Đồ thị có hướng kế thừa từ đồ thị vô hướng 
    {
        //Kế thừa Fields ,Properties ,Constructor trực tiếp từ lớp Undirected_Graph 
        public Directed_Graph() : base() { }
        public Directed_Graph(int n ) : base( n ) { }

        //-----------------------------------------------------------------------------------------------
        //Method 

        //METHOD 01 : Hàm trả về bậc vào của đỉnh 
        private int[] InDegrees ()
        {
            //Duyệt ma trận 
            int[] indegrees = new int[N];   
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (Matrix[i,j] != 0 )
                    {
                        indegrees[j]++;
                    }
                }
            }
            return indegrees;
        }
        //------------------------------------------------------------------------------------------------
        //METHOD 02 : Hàm trả về bậc ra của đỉnh 
        private int[] OutDegrees ()
        {
            int[] outdegrees = new int[N];
            for (int i = 0; i < N; i++)
            {
                int count = 0; 
                for (int j = 0; j < N; j++)
                {
                    if (Matrix[i,j] != 0 )
                    {
                        count++;
                    }
                } 
                outdegrees[i] = count;
            }
            return outdegrees;
        }
        //------------------------------------------------------------------------------------------------
        //METHOD 03 : Hàm in bậc vào và ra của đỉnh ( Dùng METHOD 01 và METHOD 02 )
        public void PrintDegrees()
        {
            int[] indegrees = InDegrees();
            int[] outdegrees = OutDegrees();
            Console.WriteLine("Số đỉnh : "+N);
            for (int i = 0; i < N; i++)
            {
                Console.WriteLine($"Đỉnh {i}: ");
                Console.WriteLine($"Bậc vào : "+indegrees[i] + "  |  Bậc ra : " + outdegrees[i]);
            }
        }
    }
}
