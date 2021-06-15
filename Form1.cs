using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Lab15
{
    public partial class Form1 : Form
    {
        Model md = new Model();
        int k = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (k < (int)numericUpDown1.Value)
            {
                if (md.GetTime() > (double)numericUpDown2.Value)
                {
                    md.Frequency(md.GetValeu(md.GetTime()));
                    chart1.Series[0].Points.Clear();
                    md.InitialData();
                    chart1.Series[0].Points.AddXY(0, md.GetValeu(0));
                    k++;
                }
                else
                {
                    double temp = md.GenerateState();
                    chart1.Series[0].Points.AddXY(temp, md.GetValeu(temp));
                }
            }
            else
            {
                double[] Freq = md.RelativeFrequency((int)numericUpDown1.Value);
                for (int i = 0; i < Freq.Length; i++)
                {
                    chart2.Series[0].Points.AddXY(i + 1, Freq[i]);
                }
                if (md.CompareFreq((int)numericUpDown1.Value))
                {
                    label7.Text = "Эмпирическое и теоретическое распреде­ления не различаются между собой";
                }
                else
                {
                    label7.Text = "Эмпирическое и теоретическое распреде­ления различаются между собой0";
                }
                timer1.Stop();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            chart1.Series[0].Points.Clear();
            md.InitialData();
            chart1.Series[0].Points.AddXY(0, md.GetValeu(0));
            timer1.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!timer1.Enabled) timer1.Start();
            timer1.Stop();
        }
    }
}
