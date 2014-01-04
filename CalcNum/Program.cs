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
            ContFunc lib = new ContFunc();

            lib.DifDivizate(ordMax, f, nod);
        }
    }
}
