using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibCalcNum
{
    public class ContFunc
    {
        public double[] Nodes { get; private set; }
        public double[] ValNodes { get; private set; }
        public double[,] DivDif { get; private set; }
        public double FinalDivDif { get; private set; }
        public Polinom F { get; private set; }

        public delegate double Polinom(double x);

        public ContFunc(double[] pNodes, Polinom pF) 
        {
            Nodes = pNodes;
            F = pF;
            ValNodes = new double[Nodes.Length];
            for (int i = 0; i < Nodes.Length; i++)
            {
                ValNodes[i] = F(Nodes[i]);
            }
        }
        public ContFunc(double[] pNoduri, double[] pValNodes)
        {
            Nodes = pNoduri;
            ValNodes = pValNodes;
        }

        public void GetDivDif(int maxOrd)
        {
            DivDif = new double[maxOrd, maxOrd];

            for (int i = 0; i < maxOrd; i++)
            {
                DivDif[i, 0] = F(Nodes[i]);
            }
            for (int k = 1; k < maxOrd; k++)
            {
                for (int i = 0; i < (maxOrd - k); i++)
                {
                    DivDif[i,k] = (DivDif[i+1,k-1] - DivDif[i,k-1]) / (Nodes[i+k] - Nodes[i]);
                }
            }

            FinalDivDif = DivDif[maxOrd - 1, 0];
        }

        private void sortNodesRelativeToNode(double baseNode)
        {
            // sort nodes according to distance from baseNode
            double tmp;
            for (int i = 0; i < Nodes.Length; i++)
            {
                for (int j = i + 1; j < Nodes.Length; j++)
                {
                    if (Nodes[i] - baseNode > Nodes[j] - baseNode)
                    {
                        tmp = Nodes[i];
                        Nodes[i] = Nodes[j];
                        Nodes[j] = tmp;

                        tmp = ValNodes[i];
                        ValNodes[i] = ValNodes[j];
                        ValNodes[j] = tmp;
                    }
                }
            }
        }

        public double InterpolateAitken(double findNode, int maxGrad, double maxErr)
        {
            double[,] matrix = new double[maxGrad, maxGrad];

            sortNodesRelativeToNode(findNode);

            // initialize first column of matrix
            for (int row = 0; row < maxGrad; row++)
			{
                matrix[row, 0] = ValNodes[row]; 
			}

            // search for value
            for (int col = 1; col < maxGrad; col++)
            {
                for (int row = col; row < maxGrad; row++)
                {
                    matrix[row, col] = 1 / ( Nodes[row] - Nodes[col-1] )
                        * (matrix[col-1, col-1] * (Nodes[row] - findNode) - matrix[row, col-1] * (Nodes[col-1] - findNode));
                }
                if (Math.Abs(matrix[col,col] - matrix[col-1,col-1]) < maxErr)
                {
                    // Found right value!!!
                    return matrix[col, col];
                }
            }

            // if we are here we did NOT find an answer
            return double.NaN;
        }

        public double InterpolateNewton(double findNode, int maxGrad, double maxErr)
        {
            sortNodesRelativeToNode(findNode);
        }
    }
}
