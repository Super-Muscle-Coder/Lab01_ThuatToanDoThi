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
            Undirected_Graph g = new Undirected_Graph();


            g.ReadEdgeList("C:/Thuat_Toan_Do_Thi/Lab01_ThuatToanDoThi/Lab01_ThuatToanDoThi/Data/Data4.txt");
            g.PrintEdgeList();
            g.EdgeListDegree();

            Console.ReadKey();
        }
       
    }
}
