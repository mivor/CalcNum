using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibCalcNum;

namespace CalcNum
{
    class Program
    {
        static void Main(string[] args)
        {
            int ordMax = 5;
            double[] nod = new double[] { -3, -1, 0, 1, 1.5 };
            ContFunc.func f = x => 1 / (1 + x * x);
            ContFunc lib = new ContFunc(nod, f);

            lib.GetDifDiv(ordMax);
            Console.WriteLine("OUT: {0}", lib.DifDivFinal);
            // 0.3076
            // 1.04768
            Console.ReadLine();
        }
    }
}
