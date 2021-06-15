using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab15
{
    class Model
    {
        double Time;
        double temp;
        int State;
        double[] P = new double[3] { 0.33333, 0.33333, 0.33333 };// теор относительные частоты
        double[] Freq = new double[3] { 0, 0, 0 };
        double[,] MatrixQ = new double[3, 3] {{-0.4,0.3,0.1},{0.4,-0.8,0.4},{0.1,0.4,-0.5} };
        Dictionary<double, int> StateWeather = new Dictionary<double, int>();
        Random rnd = new Random();
        public void InitialData()
        {
            Time = 0;
            StateWeather.Clear();
            double A = rnd.NextDouble();
            int k;
            for (k = -1; A > 0; k++)
            {
                A -= P[k + 1];
            }
            State = k + 1;
            StateWeather.Add(Time, State);
        }
        public int GetValeu(double t)
        {
            return StateWeather[t];
        }

        public double GetTime()
        {
            return Time;
        }
        public double GenerateState()
        {
            double a = rnd.NextDouble();
            temp = Math.Log(a)/MatrixQ[State-1,State-1];
            Time += temp;
            double[] mass = new double[3];
            for (int i = 0; i < mass.Length; i++)
            {
                mass[i] = MatrixQ[State-1, i]; 
            }
            double[] prob = new double[3];
            
            for (int i = 0; i < prob.Length; i++)
            {
                if (i == (State - 1))
                {
                    prob[i] = 0;
                }
                else
                {
                    prob[i]= Math.Abs(mass[i] / mass[State - 1]);
                }
            }
            int k;
            for (k = -1; a > 0; k++)
            {
                a -= prob[k + 1];
            }
            State = k+1;

            StateWeather.Add(Time, State);
            return Time;
        }

        public double[] Frequency(int i)
        {
            Freq[i - 1]++;
            return Freq;
        }

        public double[] RelativeFrequency(int n)
        {
            for(int i =0;i<Freq.Length;i++)
            {
                Freq[i] /= n;
            }
            return Freq;
        }

        //Источник алгоритма сравнения https://studfile.net/preview/2966946/page:55/
        public bool CompareFreq(int n)
        {
            int[] massP = new int[3];
            int[] massFreq = new int[3];
            for(int i =0;i<Freq.Length;i++)
            {
                massP[i] = (int)(P[i] * n);
                massFreq[i] = (int)(Freq[i] * n);
            }

            double ChiSquareEmperic = 0;

            for(int i = 0; i < Freq.Length; i++)
            {
                ChiSquareEmperic += Math.Pow((massFreq[i] - massP[i]), 2) / massP[i];
            }

            if (ChiSquareEmperic < 5.991) { return true; }
            else { return false; }
        }
    }
}
