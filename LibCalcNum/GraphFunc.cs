using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibCalcNum
{
    public class GraphFunction
    {
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
            throw new NotImplementedException();
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
