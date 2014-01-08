using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibCalcNum
{
    public class IntegralFunc
    {
        public double LimitA { get; private set; }
        public double LimitB { get; private set; }
        public Func<double, double> F { get; private set; }
        public double Evaluations { get; private set; }
        public double Solution { get; private set; }

        public IntegralFunc(Func<double, double> function, double limitA, double limitB)
        {
            this.LimitA = limitA;
            this.LimitB = limitB;
            this.F = function;
        }

        public void AproxTrapezoidRomberg(double maxErr = 0.001)
        {
            // initialize
            double prevResult;
            double h = LimitB - LimitA;
            double result = h / 2 * ( F(LimitA) + F(LimitB) );
            double n = 1;

            do
            {
                prevResult = result;
                h = h / 2;
                double sum = 0;
                for (int i = 0; i < n; i++)
                {
                    sum += F(LimitA + (2 * i - 1) * h);
                }
                sum *= h;
                result = prevResult / 2 + sum;
                n *= 2; 
            } while (Math.Abs(result - prevResult) >= maxErr);

            Evaluations = Math.Log(n, 2);
            Solution = result;

        }


        public void AproxTrapezoidClassic(double evaluations)
        {
            this.Evaluations = evaluations;
            double h = LimitB - LimitA;
            double sum = 0;
            double curentFx = F(LimitA);
            double prevFx;

            for (int i = 1; i < Evaluations; i++)
            {
                prevFx = curentFx;
                curentFx = F(LimitA + i * h / Evaluations);
                sum += curentFx + prevFx;
            }

            Solution =  h / (2 * Evaluations) * sum;
        }

        public void AproxEulerMacLaurin(Func<double, double> derivate, double evaluations)
        {
            this.Evaluations = evaluations;
            double h = LimitB - LimitA;
            double sum = 0;
            double curentFx = F(LimitA);
            double prevFx;

            for (int i = 1; i < Evaluations; i++)
            {
                prevFx = curentFx;
                curentFx = F(LimitA + i * h / Evaluations);
                sum += curentFx + prevFx;
            }

            Solution = h / (2 * Evaluations) * sum - Math.Pow(h, 2) / (12 * Math.Pow(Evaluations, 2)
                * (derivate(LimitB) - derivate(LimitA)));
        }


        //
        // private methods
        //

        private double GetTrapezoidSum()
        {
            double sum = 0;
            return sum;
        }
    }
}
