using LibCalcNum;
using NUnit.Framework;
using System;

namespace TestsCalcNum
{
    [TestFixture]
    class TestContFunc
    {
        ContinousFunction polinom;
        ContinousFunction.Polinom f;
        double[] nod;
        double[] valNod;
        int maxGrad;
        double maxErr;
        double result;
        double findNode;

        private void Init3rdRoot()
        {
            nod = new double[] { 1.0, 1.1, 1.3, 1.5, 1.6 };
            valNod = new double[] { 1, 1.032, 1.091, 1.145, 1.17 };
            f = x => Math.Pow(x, 0.3333);
            maxGrad = 5;
            maxErr = 0.001;
            findNode = 1.15;
            result = Math.Pow(1.15, 0.33333);
        }

        [Test]
        public void DiferenteDivizate()
        {
            Init3rdRoot();
            double result = -0.083333333333360363;
            polinom = new ContinousFunction(nod, valNod);

            polinom.GetDivDif(maxGrad);

            Assert.That(polinom.FinalDivDif, Is.EqualTo(result));
        }

        [Test]
        public void LagrangeAitken()
        {
            Init3rdRoot();
            polinom = new ContinousFunction(nod,valNod);

            double value = polinom.InterpolateAitken(findNode, maxGrad, maxErr);

            Assert.That(value, Is.EqualTo(result).Within(maxErr));
        }
        
        [Test]
        public void LagrangeAitken_withFunction()
        {
            Init3rdRoot();
            polinom = new ContinousFunction(nod, f);

            double value = polinom.InterpolateAitken(findNode, maxGrad, maxErr);

            Assert.That(value, Is.EqualTo(result).Within(maxErr));
        }

        [Test]
        public void LagrangeNewton()
        {
            Init3rdRoot();
            polinom = new ContinousFunction(nod, valNod);

            double value = polinom.InterpolateNewton(findNode, maxGrad, maxErr);

            Assert.That(value, Is.EqualTo(result).Within(maxErr));
        }

        [Test]
        public void LagrangeNewton_withFunction()
        {
            Init3rdRoot();
            double maxErr = 0.00001;
            polinom = new ContinousFunction(nod, f);

            double value = polinom.InterpolateNewton(findNode, maxGrad, maxErr);

            Assert.That(value, Is.EqualTo(result).Within(maxErr));
        }

        [Test]
        public void Taylor()
        {
            double result = Math.E;
            double[] nod = new double[] { 0 };
            double[] valNod = new double[] { 1, 1, 1, 1, 1, 1, 1 };
            int maxGrad = 7;
            double maxErr = 0.00025;
            double findNode = 1;
            polinom = new ContinousFunction(nod, valNod);

            double value = polinom.InterpolateTaylor(findNode, maxGrad);

            Assert.That(value, Is.EqualTo(result).Within(maxErr));
        }
    }
}
