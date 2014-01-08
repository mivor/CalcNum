using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibCalcNum
{
    public class Node
    {
        public double X { get; set; }
        public double Fx { get; set; }

        public Node(double pX, double pFx)
        {
            X = pX;
            Fx = pFx;
        }
    }
}
