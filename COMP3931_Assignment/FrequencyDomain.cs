using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;

namespace COMP3931_Assignment
{
    /*
     * Frequency domain representation.  Contains amplitude of all frequency levels.
     * Contains method for DFT and windowing
     */ 
    public partial class FrequencyDomain : Form
    {
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        static extern IntPtr GetDC(IntPtr handle);
        [DllImport("Gdi32.dll", CharSet = CharSet.Auto)]
        static extern Boolean SetViewportOrgEx(IntPtr hdc, Int32 x, Int32 y, Int32 lPoint);
        [DllImport("Gdi32.dll", CharSet = CharSet.Auto)]
        static extern Boolean SetViewportExtEx(IntPtr hdc, Int32 x, Int32 y, Int32 lPoint);
        [DllImport("Gdi32.dll", CharSet = CharSet.Auto)]
        static extern Boolean SetWindowExtEx(IntPtr hdc, Int32 x, Int32 y, Int32 lPoint);
        [DllImport("Gdi32.dll", CharSet = CharSet.Auto)]
        static extern Boolean SetMapMode(IntPtr hdc, Int16 fnMapMode);
        [DllImport("gdi32.dll")]
        static extern bool Rectangle(IntPtr hdc, int nLeftRect, int nTopRect, int nRightRect, int nBottomRect);
        [DllImport("gdi32.dll")]
        static extern uint SetDCBrushColor(IntPtr hdc, uint crColor);

        private int[] original; // The un-modified sample data
        private int[] samples;  // Sample data after being windowed
        private Complex[] bins; // Frequency bins that are to be displayed on the graph
        private double max; // The highest frequency amplitude
        private WaveViewer owner; // Reference to the WaveViewer that created this FrequencyDomain object
        private int fBin;  // The cut-off bin for a low-pass filter
        private Rectangle lowPass; // Rectangle that graphically represents the low-pass filter
        private bool threaded; // Whether or not the main program is specified as threaded

        /*
         * Constructor that initializes the display.
         * 
         * params:
         * wave - The WaveViewer that this FrequencyDomain object represents
         * data - Byte array of sample data
         * bitSample - Quantization level of given samples
         * thread - Whether or not to thread calculations
         */ 
        public FrequencyDomain(WaveViewer wave, byte[] data, short bitSample, bool thread)
        {
            owner = wave;
            max = 0;
            fBin = 0;
            lowPass.X = 0;
            lowPass.Y = 0;
            lowPass.Height = this.ClientSize.Height;
            lowPass.Width = 0;
            original = new int[data.Length * 8 / bitSample];
            bins = new Complex[original.Length];
            if(bitSample == 8)
            {
                for (int i = 0; i < data.Length; i++)
                    original[i] = data[i];
            } else if(bitSample == 16)
            {
                for (int i = 0; i < original.Length; i++)
                    original[i] = BitConverter.ToInt16(data, i * 2);
            }
            threaded = thread;
            applyWindow('n');
            InitializeComponent();
        }

        // Adjusts low-pass filter graphic height to correspond with window height
        private void FrequencyDomain_Resize(object sender, EventArgs e)
        {
            lowPass.Height = this.ClientSize.Height;
            Invalidate();
        }

        /*
         * Applies one of 3 windowing functions on the sample data
         * t - Triangle Window
         * b - Blackman-Harris Window
         * n - No Window
         * 
         * params:
         * window - character representation of the window type
         */ 
        private void applyWindow(char window)
        {
            double[] windowF = new double[bins.Length];
            samples = new int[original.Length];
            switch (window)
            {
                case 't': // Triangle Window
                    for (int i = 0; i < windowF.Length; i++)
                        windowF[i] = 1 - Math.Abs((i - ((double)(windowF.Length - 1) / 2)) / (((double)windowF.Length - 1) / 2));
                    for (int i = 0; i < bins.Length; i++)
                        samples[i] = (int)(original[i] * windowF[i]);
                    break;
                case 'b': // Blackman-Harris Window
                    double[] a = {0.35875, 0.48829, 0.14128, 0.01168};
                    for (int i = 0; i < windowF.Length; i++)
                        windowF[i] = a[0] - a[1] * Math.Cos(2 * Math.PI * i / (windowF.Length - 1))
                                     + a[2] * Math.Cos(4 * Math.PI * i / (windowF.Length - 1)) 
                                     - a[3] * Math.Cos(6 * Math.PI * i / (windowF.Length - 1));
                    for (int i = 0; i < bins.Length; i++)
                        samples[i] = (int)(original[i] * windowF[i]);
                    break;
                case 'n': // No Window
                    samples = original;
                    break;
            }
            DFT();
        }

        // Converts sample data to frequency bins through Discrete Fourier Analysis
        private void DFT()
        {
            Stopwatch time = new Stopwatch();
            time.Start();
            if (threaded)
            {
                Thread[] worker = new Thread[Environment.ProcessorCount];
                DFTThread[] dftData = new DFTThread[worker.Length];
                for (int i = 0; i < worker.Length; i++)
                {
                    dftData[i] = new DFTThread(samples, i, samples.Length / worker.Length, new DFTCallback(dftCallback));
                    worker[i] = new Thread(dftData[i].dftProc);
                    worker[i].Start();
                }
                for (int i = 0; i < worker.Length; i++)
                    worker[i].Join();
            }
            else
            {
                for (int f = 0; f < bins.Length; f++)
                {
                    double real = 0;
                    double imaginary = 0;
                    for (int t = 0; t < samples.Length; t++)
                    {
                        real += samples[t] * Math.Cos(2 * Math.PI * f * t / samples.Length);
                        imaginary -= samples[t] * Math.Sin(2 * Math.PI * f * t / samples.Length);
                    }
                    bins[f] = new Complex(real / samples.Length, imaginary / samples.Length);
                    if (bins[f].Magnitude > max)
                        max = bins[f].Magnitude;
                }
            }
            time.Stop();
            owner.pLog(threaded ? "DFT (Threaded)" : "DFT", String.Format("{0}", time.ElapsedMilliseconds));
            Invalidate();
        }

        // Paint method called when the window is invalidated
        private void FreqeuncyDomain_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(128, 66, 203, 244)), lowPass);
            IntPtr hdc = GetDC(this.Handle);
            SetMapMode(hdc, 8);
            SetViewportOrgEx(hdc, 0, this.ClientSize.Height - statusStrip1.Height, 0);
            SetViewportExtEx(hdc, this.ClientSize.Width, this.ClientSize.Height - statusStrip1.Height, 0);
            SetWindowExtEx(hdc, bins.Length * 10, -(int)max, 0);
            for (int i = 0; i < bins.Length; i++)
                Rectangle(hdc, i * 10, (int)bins[i].Magnitude, (i + 1)* 10, 0);
        }

        // Mouse move trigger that changes the size of the low-pass filter if mouse is held
        private void FrequencyDomain_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.X >= 0 && e.X <= this.ClientSize.Width)
            {
                int bin = (int)(e.X * ((double)bins.Length * 10 / this.ClientSize.Width)) / 10;
                mouseLabel.Text = "Mouse: " + bin * owner.sampleRate / bins.Length + "Hz";
                
                if (e.Button == MouseButtons.Left && bin <= bins.Length / 2)
                {
                    int last = lowPass.Width;
                    fBin = bin;
                    filterLabel.Text = "Filter: " + fBin * owner.sampleRate / bins.Length + "Hz";
                    lowPass.Width = (int)(e.X * 10) / 10;
                    Invalidate(new Rectangle(lowPass.Width, 0, last - lowPass.Width, this.ClientSize.Height));
                }
            }
        }

        // Changes windowing mode to No Windowing
        private void noneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            noneToolStripMenuItem.Checked = true;
            blackmanHarrisToolStripMenuItem.Checked = false;
            triangleToolStripMenuItem.Checked = false;
            applyWindow('n');
        }

        // Changes windowing mode to Triangle Window
        private void triangleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            noneToolStripMenuItem.Checked = false;
            blackmanHarrisToolStripMenuItem.Checked = false;
            triangleToolStripMenuItem.Checked = true;
            applyWindow('t');
        }

        // Changes windowing mode to Blackman-Harris Window
        private void blackmanHarrisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            noneToolStripMenuItem.Checked = false;
            blackmanHarrisToolStripMenuItem.Checked = true;
            triangleToolStripMenuItem.Checked = false;
            applyWindow('b');
        }

        // Callback function used by DFTThread to combine multi-threaded results into final result
        private void dftCallback(Complex[] result, int offset, double max)
        {
            for (int i = 0; i < result.Length; i++)
                bins[i + offset] = result[i];
            if (max > this.max)
                this.max = max;
        }

        // Creates a low-pass filter based on the current postion of the low-pass filter box
        // and invokes the filter on the WaveViewer this object is related to.
        private void lowPassToolStripMenuItem_Click(object sender, EventArgs e)
        {
            float[] filter = new float[bins.Length];
            if (fBin >= 0)
                filter[0] = 1;
            for (int i = 1; i <= fBin; i++)
            {
                filter[i] = 1;
                filter[filter.Length - i] = 1;
            }
            float[] samples = new float[bins.Length];
            Stopwatch time = new Stopwatch();
            time.Start();
            for (int t = 0; t < samples.Length; t++)
            {
                for (int f = 0; f < bins.Length; f++)
                {
                    samples[t] += (float)(filter[f] * Math.Cos(2 * Math.PI * t * f / samples.Length) - filter[f] * Math.Sin(2 * Math.PI * t * f / samples.Length));
                }
                samples[t] /= samples.Length;
            }
            time.Stop();
            owner.pLog("IDFT", String.Format("{0}", time.ElapsedMilliseconds));
            owner.filter(samples, this);
        }

        /*
         * Thread object that runs Discrete Fourier Transform on the given samples
         */ 
        private class DFTThread
        {
            private int offset; // The thread index to offset samples from
            private int chunkSize; // Number of samples to iterate on
            private int[] dftSamples; // The source samples to transform
            private Complex[] bins; // Frequency bin results
            private DFTCallback callback; // Callback function to call after completion of task
            private double max; // Highest amplitude value of the set

            /*
             * Constructor for DFT Thread
             * 
             * params: 
             * samples - source samples to transform
             * threadNum - the thread index to offset samples from
             * chunkSize - number of samples to iterate on
             * cb - Callback method to call after completion
             */ 
            public DFTThread(int[] samples, int threadNum, int chunkSize, DFTCallback cb)
            {
                offset = threadNum * chunkSize;
                this.chunkSize = chunkSize;
                bins = new Complex[chunkSize];
                dftSamples = samples;
                callback = cb;
                max = 0;
            }

            // Proc function for DFTThread
            public void dftProc()
            {
                for (int f = offset; f < offset + chunkSize; f++)
                {
                    double real = 0;
                    double imaginary = 0;
                    for (int t = 0; t < dftSamples.Length; t++)
                    {
                        real += dftSamples[t] * Math.Cos(2 * Math.PI * f * t / dftSamples.Length);
                        imaginary -= dftSamples[t] * Math.Sin(2 * Math.PI * f * t / dftSamples.Length);
                    }
                    bins[f - offset] = new Complex(real / dftSamples.Length, imaginary / dftSamples.Length);
                    if (bins[f - offset].Magnitude > max)
                        max = bins[f - offset].Magnitude;
                }
                callback?.Invoke(bins, offset, max);
            }
        }

        // Callback declaration for DFTThread
        private delegate void DFTCallback(Complex[] result, int offset, double max);
    }

}
