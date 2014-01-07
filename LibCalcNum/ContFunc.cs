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
        public double[] ValNoduri { get; private set; }
        public double[,] DifDiv { get; private set; }
        public double DifDivFinal { get; private set; }
        public func F { get; private set; }

        public delegate double func(double x);

        public ContFunc(double[] pNoduri, func pF) 
        {
            Noduri = pNoduri;
            F = pF;
            ValNoduri = new double[Noduri.Length];
            for (int i = 0; i < Noduri.Length; i++)
            {
                ValNoduri[i] = F(Noduri[i]);
            }
        }
        public ContFunc(double[] pNoduri, double[] pValNodrui)
        {
            Noduri = pNoduri;
            ValNoduri = pValNodrui;
        }

        public void GetDifDiv(int pOrdMax)
        {
            OrdMax = pOrdMax;

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
