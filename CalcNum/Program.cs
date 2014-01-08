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

        static ContinousFunction polinom;
        static ContinousFunction.Polinom f;
        static double[] nod;
        static double[] valNod;
        static int maxGrad;
        static double maxErr;
        static double result;
        static double findNode;

        private static void Init3rdRoot()
        {
            nod = new double[] { 1.0, 1.1, 1.3, 1.5, 1.6 };
            valNod = new double[] { 1, 1.032, 1.091, 1.145, 1.17 };
            f = x => Math.Pow(x, 0.3333);
            maxGrad = 5;
            maxErr = 0.001;
            findNode = 1.15;
            result = Math.Pow(1.15, 0.33333);
        }

        static void Main(string[] args)
        {
            Init3rdRoot();
            polinom = new ContinousFunction(nod, valNod);

            polinom.GetDivDif(maxGrad);
            double value = polinom.InterpolateNewton(findNode, maxGrad, maxErr);

            Console.ReadLine();
        }
    }
}
