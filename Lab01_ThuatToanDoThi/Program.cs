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
            ConvertGraph g = new ConvertGraph();
            //g.ReadMatrix("C:/Thuat_Toan_Do_Thi/Lab01_ThuatToanDoThi/Lab01_ThuatToanDoThi/Data/Dta.txt");
            //g.PrintMatrix();
            //g.ConvertMatrixToAdjList();
            //g.ConvertMatrixToEdgeList();

            //g.ReadEdgeList("C:/Thuat_Toan_Do_Thi/Lab01_ThuatToanDoThi/Lab01_ThuatToanDoThi/Data/Data4.txt");
            //g.PrintEdgeList();
            //g.ConvertEdgeListToMatrix();
            //g.ConvertEdgeListToAdjList();

            //g.ReadAdjacencyList("C:/Thuat_Toan_Do_Thi/Lab01_ThuatToanDoThi/Lab01_ThuatToanDoThi/Data/Data3.txt");
            //g.PrintAdjacencyList();
            //g.ConvertAdjListToMatrix();
            //g.ConvertAdjListToEdgeList();


            Console.ReadKey();
        }
       
    }
}
