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
    class TestLinearSystem
    {
        [Test]
        public void Gauss()
        {
            //int ord = 3;
            double[,] matrix = new double[3, 3] { {2, 2, 2}, {1, -1, 0}, {3, 1, 2}};
            double[] constTerms = new double[3] {6, 2, 8};
            double[] result = new double[3] {2, 0, 1};

            LinearSystem system = new LinearSystem(matrix, constTerms);

            system.ResolveGauss();

            Assert.That(system.Solution, Is.EqualTo(result));
        }
    }
}
