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
        double[,] matrix;
        double[] constTerms;
        double[] result;
        LinearSystem system;

        [Test]
        public void Gauss()
        {
            //int ord = 3;
            matrix = new double[3, 3] { {2, 2, 2}, {1, -1, 0}, {3, 1, 2}};
            constTerms = new double[3] {6, 2, 8};
            result = new double[3] {2, 0, 1};

            system = new LinearSystem(matrix, constTerms);

            system.ResolveGauss();

            Assert.That(system.Solution, Is.EqualTo(result));
        }

        [Test]
        public void Iacobi()
        {
            //int ord = 3;
            matrix = new double[3, 3] { { 2, -1, 0 }, { -1, 3, -1 }, { 0, -1, 2 } };
            constTerms = new double[3] { 1, 1, 1 };
            result = new double[3] { 1, 1, 1 };
            double maxErr = 0.0001;

            system = new LinearSystem(matrix, constTerms);

            system.ResolveIacobi(maxErr);

            Assert.That(system.Solution, Is.EqualTo(result));
        }
    }
}
