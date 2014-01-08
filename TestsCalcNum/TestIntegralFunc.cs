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
    class TestIntegralFunc
    {
        double limitA;
        double limitB;
        double maxErr;
        double result;
        double evaluations;
        IntegralFunc integral;

        [SetUp]
        public void SetUp()
        {
            limitA = 0;
            limitB = 1;
            maxErr = 0.0001;
            result = Math.Log(2);
            integral = new IntegralFunc(x => 1 / (x + 1), limitA, limitB);
        }

        [Test]
        public void TrapezoidRomberg()
        {
            maxErr = 0.0001;

            integral.AproxTrapezoidRomberg(maxErr);

            Assert.That(integral.Solution, Is.EqualTo(result).Within(maxErr));
        }

        [Test]
        public void TrapezoidClassic()
        {
            evaluations = 5025;

            integral.AproxTrapezoidClassic(evaluations);

            Assert.That(integral.Solution, Is.EqualTo(result).Within(maxErr));
        }

        [Test]
        public void EulerMacLaurin()
        {
            evaluations = 5025;

            integral.AproxEulerMacLaurin( x => -1 / Math.Pow((x + 1),2), evaluations);

            Assert.That(integral.Solution, Is.EqualTo(result).Within(maxErr));
        }

        [Test]
        public void SimpsonRomberg()
        {
            maxErr = 0.0001;

            integral.AproxSimpsonRomberg(maxErr);

            Assert.That(integral.Solution, Is.EqualTo(result).Within(maxErr));
        }

        [Test]
        public void SimpsonClassic()
        {
            evaluations = 5025;

            integral.AproxSimpsonClassic(evaluations);

            Assert.That(integral.Solution, Is.EqualTo(result).Within(maxErr));
        }
    }
}
