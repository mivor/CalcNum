using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibCalcNum
{
    public class ContFunc
    {
        public int OrdMax { get; private set; }
        public double[] Noduri { get; private set; }
        public double[,] DifDiv { get; private set; }
        public double DifDivFinal { get; private set; }
        public func F { get; private set; }

        public delegate double func(double x);

        public ContFunc() 
        {
            
        }
        public ContFunc(int pOrdMax, func pf, double[] pNoduri)
        {
            OrdMax = pOrdMax;
            Noduri = pNoduri;
            F = pf;
        }

        public void DifDivizate(int pOrdMax, func pf, double[] pNoduri)
        {
            OrdMax = pOrdMax;
            F = pf;
            Noduri = pNoduri;

            DifDiv = new double[OrdMax, OrdMax];

            for (int i = 0; i < OrdMax; i++)
            {
                DifDiv[i, 0] = F(Noduri[i]);
            }
            for (int k = 1; k < OrdMax; k++)
            {
                for (int i = 0; i < (OrdMax - k); i++)
                {
                    DifDiv[i,k] = (DifDiv[i+1,k-1] - DifDiv[i,k-1]) / (Noduri[i+k] - Noduri[i]);
                }
            }

            DifDivFinal = DifDiv[OrdMax-1, 0];

        }

    }
}
