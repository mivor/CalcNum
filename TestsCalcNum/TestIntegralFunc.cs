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
            IntegralFunc integral = new IntegralFunc();
            integral.TrapezoidRomberg(22, x => x * x);
        }
    }
}
