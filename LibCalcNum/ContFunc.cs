using System;
using System.Collections.Generic;

namespace LibCalcNum
{
    public class ContinousFunction
    {
        public List<Node> Nodes { get; private set; }
        public double[,] DivDif { get; private set; }
        public double FinalDivDif { get; private set; }

        public delegate double Polinom(double x);

        public ContinousFunction(List<Node> pNodes)
        {
            Nodes = new List<Node>(pNodes);
        }

        public ContinousFunction(double[] pNodes, Polinom pF) 
        {
            Nodes = ArrayToNodes(pNodes, pF);
        }

        public ContinousFunction(double[] pNodes, double[] pValNodes)
        {
            Nodes = ArrayToNodes(pNodes, pValNodes);
        }

        public void GetDivDif(int maxOrd)
        {
            DivDif = new double[maxOrd, maxOrd];

            for (int row = 0; row < maxOrd; row++)
            {
                DivDif[row, 0] = Nodes[row].Fx;
            }

            for (int col = 1; col < maxOrd; col++) //j
            {
                for (int row = 0; row < (maxOrd - col); row++) //k
                {
                    DivDif[row,col] = ( DivDif[row+1,col-1] - DivDif[row,col-1] ) 
                        / ( Nodes[row+col].X - Nodes[row].X );
                }
            }

            FinalDivDif = DivDif[0, maxOrd - 1];
        }

        private void sortNodesRelativeToNode(double baseNode)
        {
            // sort nodes according to distance from baseNode
            for (int i = 0; i < Nodes.Count; i++)
            {
                for (int j = i + 1; j < Nodes.Count; j++)
                {
                    if (Math.Abs(Nodes[i].X - baseNode) > Math.Abs(Nodes[j].X - baseNode))
                    {
                        var tmp = Nodes[i];
                        Nodes[i] = Nodes[j];
                        Nodes[j] = tmp;
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
                matrix[row, 0] = Nodes[row].Fx; 
			}

            // search for value
            for (int col = 1; col < maxGrad; col++)
            {
                for (int row = col; row < maxGrad; row++)
                {
                    matrix[row, col] = 1 / ( Nodes[row].X - Nodes[col-1].X )
                        * (matrix[col-1, col-1] * (Nodes[row].X - findNode) 
                        - matrix[row, col-1] * (Nodes[col-1].X - findNode));
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

            GetDivDif(maxGrad);

            double result = Nodes[0].Fx;
            for (int k = 1; k < maxGrad; k++)
            {
                double prevResult = result;
                double multiplier = 1;
                for (int i = 0; i < k; i++)
                {
                    multiplier *= (findNode - Nodes[i].X);
                }

                result = prevResult + (multiplier * DivDif[0, k]);

                if (Math.Abs(result - prevResult) < maxErr)
                {
                    // Found right value!!!
                    return result;
                }
            }

            // if we are here we did NOT find an answer
            return double.NaN;
        }

        public double InterpolateTaylor(double findNode, int maxGrad)
        {
            double[] factorial = new double[maxGrad];
            double result = Nodes[0].Fx;
            double constant = findNode - Nodes[0].X;
            factorial[0] = 1;

            for (int k = 1; k < maxGrad; k++)
            {
                factorial[k] = factorial[k - 1] * k;
                result = result + 1 / factorial[k] * Math.Pow(constant, k) * Nodes[k].Fx;
            }

            return result;
        }

        private List<Node> ArrayToNodes(double[] node, double[] valNode)
        {
            List<Node> result = new List<Node>();
            for (int i = 0; i < valNode.Length; i++)
            {
                Node n = node.Length != 1 ? new Node(node[i], valNode[i]) : new Node(node[0], valNode[i]);
                result.Add(n);
            }
            return result;
        }

        private List<Node> ArrayToNodes(double[] node, Polinom f)
        {
            List<Node> result = new List<Node>();
            for (int i = 0; i < node.Length; i++)
            {
                double valNode = f(node[i]);
                Node n = new Node(node[i], valNode);
                result.Add(n);
            }
            return result;
        }
    }
}
