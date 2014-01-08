using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using LibCalcNum;

namespace TestsCalcNum
{
    [TestFixture]
    class TestGraphFunc
    {
        double[] nod;
        double[] valNod;
        GraphFunction graph;

        [Test]
        public void RegressionLine()
        {
            GraphFunction.RegLine result = new GraphFunction.RegLine((double)95 / 281, (double)399 / 281);
            nod = new double[] { 1, 3, 4, 6, 8, 9 };
            valNod = new double[] { 1, 2, 4, 4, 5, 3};
            graph = new GraphFunction(nod, valNod);

            graph.GetRegressionLine();

            Assert.That(graph.RegressionLine.A, Is.EqualTo(result.A));
            Assert.That(graph.RegressionLine.B, Is.EqualTo(result.B));
        }

        [Test]
        public void RegressionParabola()
        {
            GraphFunction.RegParabola result = new GraphFunction.RegParabola(-1, 2, 3);
            List<Node> n = new List<Node>();
            n.Add(new Node(-4, -21));
            n.Add(new Node(-3, -12));
            n.Add(new Node(-2, -5));
            n.Add(new Node(-1, 0));
            n.Add(new Node(0, 3));
            n.Add(new Node(1, 4));
            n.Add(new Node(2, 3));
            n.Add(new Node(3, 0));
            n.Add(new Node(4, -5));
            n.Add(new Node(5, -12));
            n.Add(new Node(6, -21));
            graph = new GraphFunction(n);

            graph.GetRegressionParabola();

            Assert.That(graph.RegressionParabola.A, Is.EqualTo(result.A));
            Assert.That(graph.RegressionParabola.B, Is.EqualTo(result.B));
            Assert.That(graph.RegressionParabola.C, Is.EqualTo(result.C));
        }

    }
}
