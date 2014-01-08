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
            ContinousFunction.Polinom f = x => Math.Pow(x, 0.3333);
            //double[] valNod = new double[] { 1, 1.032, 1.091, 1.145, 1.17 };
            double errMax = 0.00001;
            double findNode = 1.15;

            ContinousFunction polinom = new ContinousFunction(nod, f);

            double value = polinom.InterpolateNewton(findNode, gradMax, errMax);
            
            Console.ReadLine();
        }
    }
}
