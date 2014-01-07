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

        [Test]
        public void DiferenteDivizate()
        {
            double result = 0.3076;
            int ordMax = 5;
            double[] nod = new double[] {-3, -1, 0, 1, 1.5};
            ContFunc.func f = x => 1 / (1 + x * x);
            ContFunc lib = new ContFunc(nod, f);

            lib.GetDifDiv(ordMax);

            Assert.That(lib.DifDivFinal, Is.EqualTo(result).Within(0.001));
        }

    }
}
