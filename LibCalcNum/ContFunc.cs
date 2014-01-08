using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibCalcNum
{
    public class ContFunc
    {
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

        public void GetDifDiv(int ordMax)
        {
            DifDiv = new double[ordMax, ordMax];

            for (int i = 0; i < ordMax; i++)
            {
                DifDiv[i, 0] = F(Noduri[i]);
            }
            for (int k = 1; k < ordMax; k++)
            {
                for (int i = 0; i < (ordMax - k); i++)
                {
                    DifDiv[i,k] = (DifDiv[i+1,k-1] - DifDiv[i,k-1]) / (Noduri[i+k] - Noduri[i]);
                }
            }

            DifDivFinal = DifDiv[ordMax - 1, 0];
        }


        public double AproxLagAitken(double findNode, int gradMax, double errMax)
        {
            double temp;
            double[,] matrix = new double[gradMax, gradMax];

            // sort nodes according to distance from findNode
            for (int i = 0; i < gradMax; i++)
            {
                for (int j = i + 1; j < gradMax; j++)
                {
                    if (Noduri[i] - findNode > Noduri[j] - findNode)
                    {
                        temp = Noduri[i];
                        Noduri[i] = Noduri[j];
                        Noduri[j] = temp;

                        temp = ValNoduri[i];
                        ValNoduri[i] = ValNoduri[j];
                        ValNoduri[j] = temp;
                    }
                }
            }

            // initialize first column of matrix
            for (int row = 0; row < gradMax; row++)
			{
                matrix[row, 0] = ValNoduri[row]; 
			}

            // search for value
            for (int col = 1; col < gradMax; col++)
            {
                for (int row = col; row < gradMax; row++)
                {
                    matrix[row, col] = 1 / ( Noduri[row] - Noduri[col-1] )
                        * (matrix[col-1, col-1] * (Noduri[row] - findNode) - matrix[row, col-1] * (Noduri[col-1] - findNode));
                }
                if (Math.Abs(matrix[col,col] - matrix[col-1,col-1]) < errMax)
                {
                    return matrix[col, col];
                }
            }

            return double.NaN;
        }
    }
}
