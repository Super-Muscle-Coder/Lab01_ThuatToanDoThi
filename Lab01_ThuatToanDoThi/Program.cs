using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab01_ThuatToanDoThi
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            ConvertGraph graph = new ConvertGraph();
            graph.ReadEdgeList("C:/Thuat_Toan_Do_Thi/Lab01_ThuatToanDoThi/Lab01_ThuatToanDoThi/Data/Data4.txt");
            graph.PrintEdgeList();
            graph.EdgeListDegree();
            graph.ConvertEdgeToAdjList();

            Console.ReadKey();
        }
       
    }
}
