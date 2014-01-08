﻿using System;
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

        [Test]
        public void TrapezoidRomberg()
        {
            double limitA = 0;
            double limitB = 1;
            double maxErr = 0.001;
            double result = Math.Log(2);
            IntegralFunc integral = new IntegralFunc(x => 1 / (x + 1), limitA, limitB);
            
            integral.AproxTrapezoidRomberg(maxErr);

            Assert.That(integral.Solution, Is.EqualTo(result).Within(maxErr));
        }
    }
}
