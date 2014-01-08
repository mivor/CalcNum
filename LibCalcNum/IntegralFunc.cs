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
            double h = LimitB - LimitA;

            Solution =  h / (2 * Evaluations) * GetTrapezoidSum();
        }

        public void AproxEulerMacLaurin(Func<double, double> derivate, double evaluations)
        {
            this.Evaluations = evaluations;
            double h = LimitB - LimitA;

            Solution = h / (2 * Evaluations) * GetTrapezoidSum() - Math.Pow(h, 2) / (12 * Math.Pow(Evaluations, 2)
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
            double h = LimitB - LimitA;
            Node curentNode = new Node(LimitA, F);
            Node prevNode;
            double sumS, sumM;
            sumS = sumM = 0;

            for (int i = 1; i < Evaluations; i++)
            {
                prevNode = curentNode;
                curentNode = new Node(LimitA + i * h / Evaluations, F);
                sumS += curentNode.Fx + prevNode.Fx;
                sumM += F((curentNode.X + prevNode.X) / 2);
            }

            Solution = h / (6 * Evaluations) * (sumS + 4 * sumM);
        }

        public void AproxNewton(double evaluations)
        {
            throw new NotImplementedException();
        }

        //
        // private methods
        //

        private double GetTrapezoidSum()
        {
            double sum = 0;
            double h = LimitB - LimitA;
            double curentFx = F(LimitA);
            double prevFx;

            for (int i = 1; i < Evaluations; i++)
            {
                prevFx = curentFx;
                curentFx = F(LimitA + i * h / Evaluations);
                sum += curentFx + prevFx;
            }

            return sum;
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
