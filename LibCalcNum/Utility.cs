using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibCalcNum
{
    public class Node
    {
        public double X { get; set; }
        public double Fx { get; set; }

        public Node(double pX, double pFx)
        {
            X = pX;
            Fx = pFx;
        }

        public static  List<Node> ArrayToNodes(double[] node, double[] valNode)
        {
            List<Node> result = new List<Node>();
            for (int i = 0; i < valNode.Length; i++)
            {
                Node n = node.Length != 1 ? new Node(node[i], valNode[i]) : new Node(node[0], valNode[i]);
                result.Add(n);
            }
            return result;
        }

        public static List<Node> ArrayToNodes(double[] node, ContinousFunction.Polinom f)
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
