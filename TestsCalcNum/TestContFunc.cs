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
    class TestContFunc
    {

        ContFunc polinom;

        [Test]
        public void DiferenteDivizate()
        {
            double result = 0.3076;
            int maxOrd = 5;
            double[] nod = new double[] {-3, -1, 0, 1, 1.5};
            ContFunc.Polinom f = x => 1 / (1 + x * x);
            polinom = new ContFunc(nod, f);

            polinom.GetDivDif(maxOrd);

            Assert.That(polinom.FinalDivDif, Is.EqualTo(result).Within(0.001));
        }

        [Test]
        public void LagrangeAitken()
        {
            double result = Math.Pow(1.15, 0.33333);
            int maxGrad = 5;
            double[] nod = new double[] { 1.0, 1.1, 1.3, 1.5, 1.6 };
            double[] valNod = new double[] { 1, 1.032, 1.091, 1.145, 1.17 };
            double maxErr = 0.001;
            double findNode = 1.15;

            polinom = new ContFunc(nod,valNod);
            double value = polinom.InterpolateAitken(findNode, maxGrad, maxErr);

            Assert.That(value, Is.EqualTo(result).Within(maxErr));
        }
        
        [Test]
        public void LagrangeAitken_withFunction()
        {
            double result = Math.Pow(1.15, 0.33333);
            int maxGrad = 5;
            double[] nod = new double[] { 1.0, 1.1, 1.3, 1.5, 1.6 };
            ContFunc.Polinom f = x => Math.Pow(x, 0.33333);
            double maxErr = 0.001;
            double findNode = 1.15;

            polinom = new ContFunc(nod, f);
            double value = polinom.InterpolateAitken(findNode, maxGrad, maxErr);

            Assert.That(value, Is.EqualTo(result).Within(maxErr));
        }

        [Test]
        public void LagrangeNewton()
        {
            double result = Math.Pow(1.15, 0.33333);
            int maxGrad = 5;
            double[] nod = new double[] { 1.0, 1.1, 1.3, 1.5, 1.6 };
            double[] valNod = new double[] { 1, 1.032, 1.091, 1.145, 1.17 };
            double maxErr = 0.00001;
            double findNode = 1.15;

            polinom = new ContFunc(nod, valNod);
            double value = polinom.InterpolateNewton(findNode, maxGrad, maxErr);

            Assert.That(value, Is.EqualTo(result).Within(maxErr));
        }

        [Test]
        public void LagrangeNewton_withFunction()
        {
            double result = Math.Pow(1.15, 0.33333);
            int maxGrad = 5;
            double[] nod = new double[] { 1.0, 1.1, 1.3, 1.5, 1.6 };
            ContFunc.Polinom f = x => Math.Pow(x, 0.33333);
            double maxErr = 0.00001;
            double findNode = 1.15;

            polinom = new ContFunc(nod, f);
            double value = polinom.InterpolateNewton(findNode, maxGrad, maxErr);

            Assert.That(value, Is.EqualTo(result).Within(maxErr));
        }
    }
}
