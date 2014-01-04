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
            int ordMax = 5;
            double[] nod = new double[] {-3, -1, 0, 1, 1.5};
            ContFunc.func f = x => 1 / (1 + x * x);
            ContFunc lib = new ContFunc();

            lib.DifDivizate(ordMax, f, nod);

            Assert.That(lib.DifDiv, Is.TypeOf(typeof(double[,])));
        }

    }
}
