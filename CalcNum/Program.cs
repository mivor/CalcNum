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
            //int ordMax = 5;
            //double[] nod = new double[] { -3, -1, 0, 1, 1.5 };
            //ContFunc.func f = x => 1 / (1 + x * x);
            //ContFunc lib = new ContFunc(nod, f);

            //lib.GetDifDiv(ordMax);
            //Console.WriteLine("OUT: {0}", lib.DifDivFinal);
            // 0.3076
            // 1.04768

            double result = Math.Pow(1.15, 0.33333);
            int gradMax = 5;
            double[] nod = new double[] { 1.0, 1.1, 1.3, 1.5, 1.6 };
            double[] valNod = new double[] { 1, 1.032, 1.091, 1.145, 1.17 };
            double errMax = 0.001;
            double findNode = 1.15;

            ContFunc polinom = new ContFunc(nod, valNod);

            double value = polinom.AproxLagAitken(findNode, gradMax, errMax);
            
            Console.ReadLine();
        }
    }
}
