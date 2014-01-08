using System;
using System.Collections.Generic;

namespace LibCalcNum
{
    public class GraphFunction
    {
        public RegParabola RegressionParabola { get; private set; }
        public RegLine RegressionLine { get; private set; }
        public List<Node> Nodes { get; private set; }

        public GraphFunction(List<Node> pNodes)
        {
            Nodes = new List<Node>(pNodes);
        }

        public GraphFunction(double[] pNodes, double[] pValNodes)
        {
            Nodes = Node.ArrayToNodes(pNodes, pValNodes);
        }

        public void GetRegressionLine()
        {
            double d, d1, d2, sumX, sumY, sumXY, sumX2;
            sumX = sumY = sumXY = sumX2 = 0;

            foreach (Node n in Nodes)
            {
                sumX += n.X;
                sumY += n.Fx;
                sumXY += n.X * n.Fx;
                sumX2 += Math.Pow(n.X, 2);
            }

            d = Nodes.Count * sumX2 - Math.Pow(sumX, 2);
            d1 = Nodes.Count * sumXY - (sumX * sumY);
            d2 = sumX2 * sumY - sumXY * sumX;

            RegressionLine = new RegLine(d1 / d, d2 / d);
        }

        public void GetRegressionParabola()
        {
            double d, d1, d2, d3, sumX, sumY, sumXY, sumX2Y, sumX2, sumX3, sumX4;
            sumX = sumY = sumXY = sumX2 = sumX3 = sumX4 = sumX2Y = 0;

            foreach (Node n in Nodes)
            {
                sumX += n.X;
                sumY += n.Fx;
                sumXY += n.X * n.Fx;
                sumX2Y += Math.Pow(n.X, 2) * n.Fx;
                sumX2 += Math.Pow(n.X, 2);
                sumX3 += Math.Pow(n.X, 3);
                sumX4 += Math.Pow(n.X, 4);
            }

            d = Nodes.Count * sumX2 * sumX4 + 2 * sumX * sumX2 * sumX3 
                - Math.Pow(sumX2, 3) - Math.Pow(sumX, 2) * sumX4 - Nodes.Count * Math.Pow(sumX3, 2);

            d1 = Nodes.Count * sumX2 * sumX2Y + sumX * sumX2 * sumXY 
                + sumX3 * sumX * sumY - Math.Pow(sumX2, 2) * sumY 
                - Math.Pow(sumX, 2) * sumX2Y - Nodes.Count * sumX3 * sumXY;

            d2 = Nodes.Count * sumXY * sumX4 + sumX2 * sumX3 * sumY 
                + sumX * sumX2 * sumX2Y - Math.Pow(sumX2, 2) * sumXY 
                - sumX4 * sumX * sumY - Nodes.Count * sumX2Y * sumX3;

            d3 = sumX4 * sumX2 * sumY + sumX * sumX2Y * sumX3 
                + sumX2 * sumX3 * sumXY - Math.Pow(sumX2, 2) * sumX2Y 
                - sumX * sumXY * sumX4 - Math.Pow(sumX3, 2) * sumY;

            RegressionParabola = new RegParabola(d1 / d, d2 / d, d3 / d);
        }

        public class RegLine
        {
            public double A { get; private set; }
            public double B { get; private set; }

            public RegLine(double a, double b)
            {
                this.A = a;
                this.B = b;
            }
        }

        public class RegParabola : RegLine
        {
            public double C { get; private set; }

            public RegParabola(double a, double b, double c)
                : base(a, b)
            {
                this.C = c;
            }
        }
    }
}
