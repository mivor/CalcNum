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
        public int OrdMat { get; private set; }
        public double maxErr { get; private set; }
        public double Evaluations { get; private set; }


        public LinearSystem(double[,] matrix, double[] consTerms)
        {
            this.Matrix = matrix;
            this.ConsTerms = consTerms;
            this.OrdMat = matrix.GetLength(0) > matrix.GetLength(1) 
                ? matrix.GetLength(0) : matrix.GetLength(1);
        }
        public void ResolveGauss()
        {
            double[] pVect = new double[OrdMat];
            double[] cTerms = ConsTerms;
            double[,] mat = Matrix;
            double[,] a = new double[OrdMat, OrdMat];
            for (int i = 0; i < OrdMat; i++)
            {
                pVect[i] = i;
            }

            for (int k = 0; k < OrdMat-1; k++)
            {
                int i = 1;
                int j = 1;

                //init a[i,j]

                //check
                //if (a[i,j] == 0) return;

                //switch lines in mat
                for (int q = 0; q < OrdMat; q++)
                {
                    switchTerms(ref mat[i, q],ref mat[k, q]);
                    
                }

                //switch termens in consTerms
                switchTerms(ref cTerms[i],ref cTerms[k]);

                //switch cols in mat
                for (int q = 0; q < OrdMat; q++)
                {
                    switchTerms(ref mat[q, j], ref mat[q, k]);
                }

                // switch terms in p
                switchTerms(ref pVect[i], ref pVect[k]);

                for ( i = k+1; i < OrdMat; i++)
                {
                    double temp = a[i, k] / a[k, k];
                    for ( j = k+1; j < OrdMat; j++)
                    {
                        a[i, j] = a[i, j] - a[k, j] * temp;
                    }
                    cTerms[i] = cTerms[i] - cTerms[k] * temp;
                }
            }

            //check
            if (a[OrdMat, OrdMat] == 0) return;

            // calculate solution
            Solution[OrdMat] = cTerms[OrdMat] / a[OrdMat, OrdMat];
            for (int p = OrdMat-1; p >= 0; p++)
            {
                double sum = 0;
                for (int j = p+1; j < OrdMat; j++)
			    {
                    sum += a[p,j] * Solution[j];
			    }
                Solution[p] = (cTerms[p] - sum) / a[p, p];
            }

            Array.Sort(pVect);
            Array.Sort(Solution);
        }

        public void ResolveIacobi(double maxErr)
        {
            // test convergence
            testConvergence();

            double[] beta = new double[OrdMat];
            double[,] u = new double[OrdMat, OrdMat];
            double[,] x = new double[OrdMat, OrdMat];
            double maxDiff = 0;
            int k;

            for (int i = 0; i < OrdMat; i++)
            {
                beta[i] = ConsTerms[i] / Matrix[i, i];
            }

            for (int i = 0; i < OrdMat; i++)
            {
                x[0, i] = beta[i];
                u[1, i] = 0;
                for (int j = 0; j < OrdMat; j++)
                {
                    u[1, i] += (0 - Matrix[i, j]) / Matrix[i, i] * x[0, i]; 
                }
                x[1, i] = u[1, i] + x[0, i] + beta[i];
            }

            for (k = 0; maxDiff < maxErr; k++)
            {
                maxDiff = maxDiffOfVectors(x, k+1, k);
                if (maxDiff < maxErr) break;

                for (int i = 0; i < OrdMat; i++)
                {
                    u[k + 1, i] = 0;
                    for (int j = 0; j < OrdMat; j++)
                    {
                        u[k+1,i] += (0 - Matrix[i, j]) / Matrix[i, i] * x[k, i]; 
                    }
                    x[k + 1, i] = u[k+1, i] + x[k, i] + beta[i];
                }

                maxDiff = maxDiffOfVectors(x, k, k - 1);
            }

            // results
            Evaluations = k;
            for (int i = 0; i < OrdMat; i++)
			{
			    Solution[i] = x[k, i];
			}
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

        private void testConvergence()
        {
            for (int i = 0; i < OrdMat; i++)
            {
                double check = 0;

                for (int j = 0; j < OrdMat; j++)
                {
                    check += Matrix[i, j];
                }

                check = check - Matrix[i, i];

                if (check >= Matrix[i, i]) return;
            }
        }

        private double maxDiffOfVectors(double[,] m, int v1, int v2)
        {
            double result = 0;

            for (int i = 0; i < OrdMat; i++)
            {
                double max = Math.Abs(m[v1,i] - m[v2,i]);
                if (max > result) result = max;
            }
            return result;
        }
    }
}
