using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibCalcNum
{
    public class LinearSystem
    {

        public double[] Solution { get; private set; }
        public double[,] Matrix { get; private set; }
        public double[] ConsTerms { get; private set; }
        public int MatrixOrder { get; private set; }


        public LinearSystem(double[,] matrix, double[] consTerms)
        {
            this.Matrix = matrix;
            this.ConsTerms = consTerms;
            this.MatrixOrder = matrix.GetLength(0) > matrix.GetLength(1) 
                ? matrix.GetLength(0) : matrix.GetLength(1);
        }
        public void ResolveGauss()
        {
            throw new NotImplementedException();
        }

    }
}
