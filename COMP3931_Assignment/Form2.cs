using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace COMP3931_Assignment
{
    public partial class WaveGraph : Form
    {
        private int sampleRate;
        private double[] samples;
        private Complex[] amplitudes;
        public WaveGraph(int sr)
        {
            samples = new double[sr];
            amplitudes = new Complex[sr];
            sampleRate = sr;
            InitializeComponent();
        }

        public void setSampleRate(int sr)
        {
            sampleRate = sr;
        }

        public int getSampleRate()
        {
            return sampleRate;
        }

        public void addWave(double freq, string type, double amplitude, double pS)
        {
            for(int t = 0; t < samples.Length; t++)
            {
                if (type == "typeSine")
                    samples[t] += amplitude * Math.Sin(2 * Math.PI * freq * t / sampleRate + pS * (Math.PI / 180));
                else if (type == "typeCosine")
                    samples[t] += amplitude * Math.Cos(2 * Math.PI * freq * t / sampleRate + pS * (Math.PI / 180));
            }
            drawGraph();
        }

        private void drawGraph()
        {
            waveChart.Series["Samples"].Points.Clear();
            for (int i = 0; i < samples.Length; i++)
                waveChart.Series["Samples"].Points.AddY(samples[i]);

            fourier();
            waveChart.Series["Amps"].Points.Clear();
            for (int i = 0; i < amplitudes.Length; i++)
                waveChart.Series["Amps"].Points.AddY(amplitudes[i].Magnitude);
        }

        private void fourier()
        {
            for (int f = 0; f < amplitudes.Length; f++)
            {
                double real = 0;
                double imaginary = 0;
                for (int t = 0; t < samples.Length; t++)
                {
                    real += samples[t] * Math.Cos(2 * Math.PI * f * t / samples.Length);
                    imaginary -= samples[t] * Math.Sin(2 * Math.PI * f * t / samples.Length);
                }
                amplitudes[f] = new Complex(real / samples.Length, imaginary / samples.Length);
            }
        }

        private void addSignal_Click(object sender, EventArgs e)
        {
            //AddSignalDialog newSignal = new AddSignalDialog(this);
            //newSignal.Show();
        }
    }
}
