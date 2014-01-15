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
        public int MatOrd { get; private set; }


        public LinearSystem(double[,] matrix, double[] consTerms)
        {
            this.Matrix = matrix;
            this.ConsTerms = consTerms;
            this.MatOrd = matrix.GetLength(0) > matrix.GetLength(1) 
                ? matrix.GetLength(0) : matrix.GetLength(1);
        }
        public void ResolveGauss()
        {
            double[] pVect = new double[MatOrd];
            double[] cTerms = ConsTerms;
            double[,] mat = Matrix;
            double[,] a = new double[MatOrd, MatOrd];
            for (int i = 0; i < MatOrd; i++)
            {
                pVect[i] = i;
            }

            for (int k = 0; k < MatOrd-1; k++)
            {
                int i = 1;
                int j = 1;

                //init a[i,j]

                //check
                //if (a[i,j] == 0) return;

                //switch lines in mat
                for (int q = 0; q < MatOrd; q++)
                {
                    switchTerms(ref mat[i, q],ref mat[k, q]);
                    
                }

                //switch termens in consTerms
                switchTerms(ref cTerms[i],ref cTerms[k]);

                //switch cols in mat
                for (int q = 0; q < MatOrd; q++)
                {
                    switchTerms(ref mat[q, j], ref mat[q, k]);
                }

                // switch terms in p
                switchTerms(ref pVect[i], ref pVect[k]);

                for ( i = k+1; i < MatOrd; i++)
                {
                    double temp = a[i, k] / a[k, k];
                    for ( j = k+1; j < MatOrd; j++)
                    {
                        a[i, j] = a[i, j] - a[k, j] * temp;
                    }
                    cTerms[i] = cTerms[i] - cTerms[k] * temp;
                }
            }

            //check
            if (a[MatOrd, MatOrd] == 0) return;

            // calculate solution
            Solution[MatOrd] = cTerms[MatOrd] / a[MatOrd, MatOrd];
            for (int p = MatOrd-1; p >= 0; p++)
            {
                double sum = 0;
                for (int j = p+1; j < MatOrd; j++)
			    {
                    sum += a[p,j] * Solution[j];
			    }
                Solution[p] = (cTerms[p] - sum) / a[p, p];
            }

            Array.Sort(pVect);
            Array.Sort(Solution);
        }

        //
        // private methods
        //

        private void switchTerms(ref double term1, ref double term2)
        {
            double tmp = term1;
            term1 = term2;
            term2 = tmp;
        }

    }
}
