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
            GraphFunction.RegLine result = new GraphFunction.RegLine(95 / 281, 399 / 281);
            nod = new double[] { 1, 3, 4, 6, 8, 9 };
            valNod = new double[] { 1, 2, 4, 4, 5, 3};
            graph = new GraphFunction(nod, valNod);

            graph.GetRegressionLine();

            Assert.That(graph.RegressionLine, Is.EqualTo(result));
        }

    }
}
