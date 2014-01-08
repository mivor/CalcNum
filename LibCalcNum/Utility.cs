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

        public Node(double x, double fx)
        {
            this.X = x;
            this.Fx = fx;
        }

        public Node(double x, Func<double, double> f)
        {
            this.X = x;
            this.Fx = f(x);
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

        public static List<Node> ArrayToNodes(double[] node, Func<double, double> f)
        {
            List<Node> result = new List<Node>();
            for (int i = 0; i < node.Length; i++)
            {
                Node n = new Node(node[i], f);
                result.Add(n);
            }
            return result;
        }
    }
}
