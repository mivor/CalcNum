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
                double sum = GetRombergSum(n, h);
                n *= 2; 
                sum *= h;
                result = prevResult / 2 + sum;
            } while (Math.Abs(result - prevResult) >= maxErr);

            Evaluations = Math.Log(n, 2);
            Solution = result;

        }

        public void AproxTrapezoidClassic(double evaluations)
        {
            this.Evaluations = evaluations;
            List<Sum> sums = new List<Sum>();
            sums.Add(new Sum((nowX, prevX) => F(nowX) + F(prevX)));
            GetClassicSum(sums);

            Solution = (LimitB - LimitA) / (2 * Evaluations) * sums[0].Value;
        }

        public void AproxEulerMacLaurin(Func<double, double> derivate, double evaluations)
        {
            this.Evaluations = evaluations;
            List<Sum> sums = new List<Sum>();
            sums.Add(new Sum((nowX, prevX) => F(nowX) + F(prevX)));
            GetClassicSum(sums);

            Solution = (LimitB - LimitA) / (2 * Evaluations) * sums[0].Value 
                - Math.Pow((LimitB - LimitA), 2) / (12 * Math.Pow(Evaluations, 2)
                * (derivate(LimitB) - derivate(LimitA)));
        }

        public void AproxSimpsonRomberg(double maxErr)
        {
            // initialize
            double prevResult;
            double h = (LimitB - LimitA) / 2;
            double sumFx = F(LimitA) + F(LimitB);
            double n = 1;
            double sum1 = F(LimitA + h);
            double sum2 = 0;
            double result = h / 3 * (sumFx + 2 * sum2 + 4 * sum1);

            do
            {
                prevResult = result;
                h = h / 2;
                n *= 2;
                sum2 = sum2 + sum1;
                sum1 = GetRombergSum(n, h);
                result = h / 3 * (sumFx + 2 * sum2 + 4 * sum1);
            } while (Math.Abs(result - prevResult) >= maxErr);

            Evaluations = Math.Log(n, 2);
            Solution = result;
        }

        public void AproxSimpsonClassic(double evaluations)
        {
            this.Evaluations = evaluations;
            List<Sum> sums = new List<Sum>();
            sums.Add(new Sum( (nowX, prevX) => F(nowX) + F(prevX) ));
            sums.Add(new Sum((nowX, prevX) => F((nowX + prevX) / 2)));
            GetClassicSum(sums);

            Solution = (LimitB - LimitA) / (6 * Evaluations) * (sums[0].Value + 4 * sums[1].Value);
        }

        public void AproxNewton(double evaluations)
        {
            this.Evaluations = evaluations;
            List<Sum> sums = new List<Sum>();
            sums.Add(new Sum(( nowX, prevX) => F(nowX) + F(prevX) ));
            sums.Add(new Sum(( nowX, prevX) => F(prevX + (nowX - prevX) / 3) ));
            sums.Add(new Sum(( nowX, prevX) => F(prevX + 2 * (nowX - prevX) / 3) ));
            GetClassicSum(sums);

            Solution = (LimitB - LimitA) / (8 * Evaluations) * (sums[0].Value + 3 * (sums[1].Value + sums[2].Value));
        }

        //
        // private methods
        //

        private class Sum
        {
            public double Value { get; set; }
            public Func<double, double, double> F { get; private set; }

            public Sum(Func<double, double, double> function, double val = 0)
            {
                this.Value = val;
                this.F = function;
            }

            public void UpdateValue(double nowX, double prevX)
            {
                Value += F(nowX, prevX);
            }
        }

        private void GetClassicSum(List<Sum> sums )
        {
            double nowX = LimitA;
            double prevX;

            for (int i = 1; i < Evaluations; i++)
            {
                prevX = nowX;
                nowX = LimitA + i * (LimitB - LimitA) / Evaluations;
                foreach (Sum s in sums)
                {
                    s.UpdateValue(nowX, prevX);
                }
            }
        }

        private double GetRombergSum(double n, double h)
        {
            double sum = 0;
            for (int i = 0; i < n; i++)
            {
                sum += F(LimitA + (2 * i - 1) * h);
            }
            return sum;
        }
    }
}
