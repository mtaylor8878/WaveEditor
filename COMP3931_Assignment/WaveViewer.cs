using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Threading;
using System.Diagnostics;

namespace COMP3931_Assignment
{
    /*
     * Wave object window.
     * Holds all relevant data that relates to that wave and methods for manipulation of the wave.
     */ 
    public partial class WaveViewer : Form
    {
        // Win32 Imports
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        static extern IntPtr GetDC(IntPtr handle);
        [DllImport("Gdi32.dll", CharSet = CharSet.Auto)]
        static extern Int32 SetROP2(IntPtr handle, Int16 flag);
        [DllImport("Gdi32.dll", CharSet = CharSet.Auto)]
        static extern Boolean MoveToEx(IntPtr hdc, Int16 x, Int16 y, Int32 point);
        [DllImport("Gdi32.dll", CharSet = CharSet.Auto)]
        static extern Boolean LineTo(IntPtr hdc, Int32 x, Int32 y);
        [DllImport("Gdi32.dll", CharSet = CharSet.Auto)]
        static extern Boolean SetViewportOrgEx(IntPtr hdc, Int32 x, Int32 y, Int32 lPoint);
        [DllImport("Gdi32.dll", CharSet = CharSet.Auto)]
        static extern Boolean SetViewportExtEx(IntPtr hdc, Int32 x, Int32 y, Int32 lPoint);
        [DllImport("Gdi32.dll", CharSet = CharSet.Auto)]
        static extern Boolean SetWindowExtEx(IntPtr hdc, Int32 x, Int32 y, Int32 lPoint);
        [DllImport("Gdi32.dll", CharSet = CharSet.Auto)]
        static extern Boolean SetMapMode(IntPtr hdc, Int16 fnMapMode);

        // record_play.dll imports
        [DllImport("record_play.dll", CharSet = CharSet.Auto)]
        static extern IntPtr DlgShow(int sampleRate, int bitSample);
        [DllImport("record_play.dll", CharSet = CharSet.Auto)]
        static extern unsafe byte* GetSaveBuffer();
        [DllImport("record_play.dll", CharSet = CharSet.Auto)]
        static extern unsafe UInt32 GetLength();
        [DllImport("record_play.dll", CharSet = CharSet.Auto)]
        static extern bool DlgProc(IntPtr hwnd, UInt32 message, int wParam, int lParam);
        [DllImport("record_play.dll", CharSet = CharSet.Auto)]
        static extern void SendSamples(byte[] samples, UInt32 length);

        // assembly_calculate.dll imports
        [DllImport("assembly_calculate.dll", CharSet = CharSet.Auto)]
        static extern unsafe void convolution_filter(float[] filter, float[] result, int[] samples, int fSize, int sSize);

        // Message constants for recording window
        static int IDC_RECORD_BEG = 1000;
        static int IDC_RECORD_END = 1001;
        static int IDC_PLAY_BEG = 1002;
        static int IDC_PLAY_PAUSE = 1003;
        static int IDC_PLAY_END = 1004;
        static int IDC_PLAY_REV = 1005;
        static int IDC_PLAY_REP = 1006;
        static int IDC_PLAY_SPEED = 1007;
        static uint WM_COMMAND = 0x0111;

        static EventWaitHandle recEvent = new EventWaitHandle(false, EventResetMode.AutoReset, "RecordFinish"); // event that signals recording is completed
        private byte[] samples; // raw byte array of samples
        public int sampleRate; // sample rate of the wave
        private int numSamples; // number of samples
        private unsafe byte* saveBuffer; // pointer to the buffer where recordings are stored in the recording window
        private unsafe UInt32 dataLen; // length of recording window's data
        private IntPtr recDlg; // Handle to the recording window
        private short sampleBit; // quantization of the wave (8-bit, 16-bit)
        private short channels; // number of audio channels (mono stereo)
        private int startSample; // the first sample that can be seen on the viewer window
        private int endSample; // the last sample that can be seen on the viewer window
        private int hScale; // the horizontal scale of samples.  number of samples currently on the window
        private int selStart, selEnd; // start and end samples of the selection box
        private MainContainer main; // reference to the main container
        private bool mouseHeld; // whether the mouse is currently held down
        private bool desel; // if the paint method should be clearing the selection box
        private Rectangle selSpace; // Rectangle representing the selection box
        private Rectangle mouseSample; // Rectangle for the small red dot that shows the sample level
        private Rectangle timeBar; // Rectangle representing the time strip at the bottom of the window
        private Thread recListener; // Thread that listens for the recording to finish
        private int secDenom; // numbers of samples to mark a time stamp at
        private Dictionary<int, Label> timeStamp; // list of all time stamps on the screen 
        private int[] fSamples; // store for combining multiple filtered samples
        private float[] filterResult; // store for combining filtered result

        /* 
            Constructor that creates a blank wave.

            params: 
            sr - Desired sampling rate for the wave
            q - Quantization level of audio
            main - reference to main container
         */
        public unsafe WaveViewer(MainContainer main, int sr, short q)
        {
            recDlg = DlgShow(sr, q);
            dataLen = GetLength();
            numSamples = sr;
            sampleBit = q;
            dataLen = (uint)(numSamples * sampleBit / 8);
            saveBuffer = GetSaveBuffer();
            samples = new byte[dataLen];
            for (int i = 0; i < samples.Length; i++)
                samples[i] = (byte)(sampleBit == 8 ? 128 : 0);
            sampleRate = sr;
            startSample = 0;
            hScale = numSamples <= this.Width ? this.Width : (numSamples + this.Width) / 2;
            endSample = hScale;
            this.main = main;
            channels = 1;
            selStart = 0;
            selEnd = 0;
            desel = false;
            InitializeComponent();
        }

        /*
         * Constructor that creates a wave from a file
         * 
         * params:
         * fs - File stream to read from
         * main - reference to the main container
         */
        public unsafe WaveViewer(MainContainer main, FileStream fs)
        {
            readWaveHeader(fs);
            samples = new byte[dataLen];
            numSamples = (int)(dataLen * 8 / sampleBit);
            fs.Position = 44;
            fs.Read(samples, 0, (int)dataLen);
            hScale = numSamples <= this.Width ? this.Width : (numSamples + this.Width) / 2;
            this.main = main;
            startSample = 0;
            endSample = hScale;
            selStart = 0;
            selEnd = 0;
            desel = false;
            InitializeComponent();
        }

        // Load event that intializes all shared system variables
        private void WaveViewer_Load(object sender, System.EventArgs e)
        {
            timeStamp = new Dictionary<int, Label>();
            statusBit.Text = sampleBit + "-bit Audio";
            statusSampleRate.Text = "Sample Rate: " + sampleRate + "Hz";
            sampleStatus.Text = "Sample Level:  " + getMaxSample();
            zoomBar.Left = this.Width - zoomBar.Width;
            zoomBar.Maximum = numSamples * 8 / sampleBit < hScale ? hScale : numSamples * 8 / sampleBit;
            zoomBar.Minimum = numSamples / this.Width;
            zoomBar.Value = hScale;
            hScroll.Width = this.Width - zoomBar.Width - statusBit.Width - statusSampleRate.Width - statusZoom.Width - sampleStatus.Width;
            hScroll.Left = this.Width - zoomBar.Width - hScroll.Width;
            hScroll.Maximum = numSamples - hScale;
            hScroll.Minimum = 0;
            hScroll.SmallChange = hScale / this.Width + 1;
            statusZoom.Text = "Zoom: " + this.Width * 100 / hScale + "%";
            mouseHeld = false;
            mouseSample.X = 0;
            mouseSample.Y = 0;
            mouseSample.Height = mouseSample.Width = 6;
            timeBar.Height = 20;
            timeBar.X = 0;
            timeBar.Y = this.Height - statusStrip.Height - timeBar.Height;
            timeBar.Width = this.Width;
            selSpace.Height = this.Height - statusStrip.Height - timeBar.Height;
            setSecDenom();
        }

        // function called when WaveViewer is invalidated
        private unsafe void WaveViewer_Paint(object sender, PaintEventArgs e)
        {
            // draw selection box if not deselecting
            if (!desel)
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(128, 66, 203, 244)), selSpace);
            }

            IntPtr dc = GetDC(this.Handle);
            int scaleR = hScale / this.Width + 1;
            SetMapMode(dc, 8);

            // Draw timebar line
            MoveToEx(dc, 0, (short)timeBar.Y, 0);
            LineTo(dc, this.Width, timeBar.Y);

            // Draw time ticks on time bar
            for (int i = secDenom * (startSample/secDenom + 1); i < endSample; i += secDenom){
                int x = xFromSample(i);
                MoveToEx(dc, (short)x, (short)timeBar.Y, 0);
                LineTo(dc, x, timeBar.Y + timeBar.Height);

                // Create time stamp labels and update their locations based on scroll value
                if (!timeStamp.ContainsKey(i))
                {
                    double fSec = (double)i / sampleRate;
                    int min = (int)fSec / 60;
                    int sec = (int)fSec % 60;
                    int mSec = (int)((fSec % 60 - sec) * 100);
                    Label tmp = new Label();
                    tmp.Location = new Point(x + 1, timeBar.Y + 5);
                    tmp.Text = String.Format("{0:d2}:{1:d2}:{2:d2}", min, sec, mSec);
                    tmp.AutoSize = true;
                    this.Controls.Add(tmp);
                    timeStamp.Add(i, tmp);
                } else
                {
                    timeStamp[i].Location = new Point(x + 1, timeBar.Y + 5);
                }
            }

            // set scaling for the samples and draw 0 line
            SetViewportOrgEx(dc, 0, (this.Height - statusStrip.Height - timeBar.Height) / 2, 0);
            SetWindowExtEx(dc, hScale, -getMaxSample(), 0);
            SetViewportExtEx(dc, this.Width, (this.Height - statusStrip.Height - timeBar.Height) / 2, 0);
            MoveToEx(dc, (short)hScale, 0, 0);
            LineTo(dc, 0, 0);

            // draw the waveform based on the quantization level
            switch (sampleBit)
            {
                case 8:
                    for (int i = startSample; i < endSample && i < numSamples; i += scaleR)
                        LineTo(dc, i - startSample, (short)(samples[i] - 128));
                    break;
                case 16:
                    for (int i = startSample; i < endSample && i < numSamples; i += scaleR)
                        LineTo(dc, i - startSample, BitConverter.ToInt16(samples, i * 2));
                    break;
            }

            // draw a red dot around the sample the mouse is currently hovering at
            e.Graphics.DrawEllipse(Pens.Red, mouseSample);

            // draws a line if the selection box has 0 width
            if (!desel && selStart == selEnd)
            {
                SetROP2(dc, 14);
                MoveToEx(dc, (short)(selStart - startSample), (short)(-getMaxSample()), 0);
                LineTo(dc, (short)(selStart - startSample), (short)getMaxSample());
                SetROP2(dc, 0);
            }

            // clears any time stamps that aren't on the screen anymore
            List<int> delete = new List<int>();
            foreach(KeyValuePair<int,Label> kvp in timeStamp)
                if (kvp.Key < startSample || kvp.Key > endSample)
                    delete.Add(kvp.Key);
            foreach (int i in delete)
            {
                this.Controls.Remove(timeStamp[i]);
                timeStamp.Remove(i);
            }
        }

        /*
         * Generates a sine or cosine wave and adds it to the current wave
         * 
         * params:
         * freq - Desired frequency of the wave
         * amplitude - Desired amplitude of the wave
         * pS - Desired phase shift of the wave
         */
        public void addWave(double freq, double amplitude, double pS)
        {
            int max = getMaxSample();
            switch (sampleBit)
            {
                case 8:
                    for (int t = 0; t < numSamples; t++)
                        samples[t] += (byte)((int)(max * amplitude * Math.Sin(2 * Math.PI * freq * t / numSamples - pS * (Math.PI / 180))));
                    break;
                case 16:
                default:
                    for (int t = 0; t < numSamples; t++)
                    {
                        byte[] sample = BitConverter.GetBytes((Int16)(max * amplitude * Math.Sin(2 * Math.PI * freq * t / numSamples - pS * (Math.PI / 180))));
                        samples[t * 2] += sample[0];
                        samples[t * 2 + 1] += sample[1];
                    }
                    break;
            }
            zoomBar.Maximum = numSamples;
            Invalidate();
        }

        /*
         * Exports the current wave to a .wav file
         * 
         * params:
         * fn - Name for the file
         */
        public void exportToWave(String fn)
        {
            FileStream fs = new FileStream(fn, FileMode.Create, FileAccess.Write);
            BinaryWriter bw = new BinaryWriter(fs);

            // Writing Header File
            bw.Write(Encoding.ASCII.GetBytes("RIFF"));
            bw.Write(36 + numSamples * channels * sampleBit / 8);
            bw.Write(Encoding.ASCII.GetBytes("WAVE"));
            bw.Write(Encoding.ASCII.GetBytes("fmt "));
            bw.Write(16); // Chunk size
            bw.Write((ushort)1); // PCM mode
            bw.Write((ushort)channels);
            bw.Write(sampleRate); // sample rate
            bw.Write(sampleRate * channels * sampleBit / 8); //bit rate
            bw.Write((ushort)(channels * sampleBit / 8)); //Block Align
            bw.Write((ushort)sampleBit);
            bw.Write(Encoding.ASCII.GetBytes("data"));
            bw.Write(numSamples * channels * sampleBit / 8);

            switch (sampleBit)
            {
                default:
                    for (int i = 0; i < samples.Length; i++)
                        bw.Write(samples[i]);
                    break;
            }
            bw.Close();
            fs.Close();
        }

        // Sets the selected wave to this one if clicked
        private void WaveViewer_Click(object sender, EventArgs e)
        {
            main.setSelected(main.getWaveViewer(this));
        }

        // Returns the maximum sample size
        private int getMaxSample()
        {
            switch (sampleBit)
            {
                case 32:
                    return Int32.MaxValue;
                case 8:
                    return Byte.MaxValue / 2;
                default:
                    return Int16.MaxValue;
            }
        }

        /*
         * Sets wave variables based on the header of a file
         * 
         * params:
         * fs - Filestream to read from
         */
        private unsafe void readWaveHeader(FileStream fs)
        {
            BinaryReader br = new BinaryReader(fs);
            fs.Position = 22;
            channels = br.ReadInt16();
            fs.Position = 24;
            sampleRate = br.ReadInt32();
            fs.Position = 34;
            sampleBit = br.ReadInt16();
            recDlg = DlgShow(sampleRate, sampleBit);
            saveBuffer = GetSaveBuffer();
            fs.Position = 40;
            dataLen = br.ReadUInt32();
        }

        // Updates the zoom level when releasing the mouse
        private void zoomBar_MouseUp(object sender, EventArgs e)
        {
            zoomBar_Scroll(sender, e);
        }

        // Updates the zoom level while scrolling the zoom bar
        private void zoomBar_Scroll(object sender, EventArgs e)
        {
            hScale = zoomBar.Value;
            statusZoom.Text = "Zoom: " + this.Width * 100 / hScale + "%";
            endSample = startSample + hScale;
            hScroll.Maximum = numSamples - hScale;
            hScroll.SmallChange = hScale / this.Width + 1;
            selSpace.X = xFromSample(selStart);
            selSpace.Width = (int)((selEnd - selStart) * (double)this.Width / hScale);
            setSecDenom();
            Invalidate();
        }

        // Changes the scroll position of the window based on the position of the scroll bar
        private void hScroll_Scroll(object sender, ScrollEventArgs e)
        {
            startSample = hScroll.Value;
            endSample = hScale + startSample;
            selSpace.X = xFromSample(selStart);
            selSpace.Width = (int)((selEnd - selStart) * (double)this.Width / hScale);
            Invalidate();
        }

        // Starts selecting when mouse is pressed
        private void WaveViewer_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                deselect();
                selStart = (int)(e.X * (double)hScale / this.Width) + startSample;
                selEnd = selStart;
                selSpace.X = e.X;
                selSpace.Width = 0;
                mouseHeld = true;
            }
        }

        // Handles events when the mouse is moved
        private void WaveViewer_MouseMove(object sender, MouseEventArgs e)
        {
            // Gets the sample level based on the mouse's position
            Rectangle last = mouseSample;
            last.X -= 1;
            last.Y -= 1;
            last.Width += 2;
            last.Height += 2;
            mouseSample.X = e.X - mouseSample.Width / 2;
            int curr = sampleFromX(e.X);
            int sample;
            if (sampleBit == 16)
                sample = BitConverter.ToInt16(samples, curr * 2);
            else
                sample = samples[curr] - getMaxSample();
            sampleStatus.Text = "Sample Level: " + sample;
            sample += getMaxSample();
            mouseSample.Y = this.Height - statusStrip.Height - timeBar.Height - (int)(sample * (double)(this.Height - statusStrip.Height - timeBar.Height) / 2 / getMaxSample()) - mouseSample.Height / 2;
            Invalidate(last);
            Invalidate(mouseSample);

            // Starts the scrolling timer if outside the bounds of the wave
            if (mouseHeld)
            {
                if (e.X >= this.Width)
                    scrollTime.Start();
                else
                    scrollTime.Stop();
                setEndSampleFromX(e.X);
            }
            //statusBit.Text = "start: " + selStart + " end: " + selEnd + " rect: " + selSpace.Left + "," + selSpace.Top; //Debug information
        }

        // Ends selection when mouse is released
        private void WaveViewer_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                setEndSampleFromX(e.X);
                mouseHeld = false;
            }
        }

        // Keyboard shortcuts for copying, pasting and deleting
        private void WaveViewer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
            {
                delete();
            }
            else if (e.Control && e.KeyCode == Keys.C)
            {
                copy();
            }
            else if (e.Control && e.KeyCode == Keys.V)
            {
                paste();
            }
            else if (e.Control && e.KeyCode == Keys.X)
            {
                cut();
            }
        }

        // Stops the scroll timer when the mouse enter the window
        private void WaveViewer_Enter(object sender, EventArgs e)
        {
            scrollTime.Stop();
        }

        // Starts the scroll timer when the mouse leaves the window
        private void WaveViewer_Leave(object sender, EventArgs e)
        {
            if (mouseHeld)
            {
                scrollTime.Start();
            }
        }

        // Returns the sample index from an X coordinate
        private int sampleFromX(int x)
        {
            return (int)(x * (double)hScale / this.Width) + startSample;
        }

        // Returns an X coordinate from a sample index
        private int xFromSample(int s)
        {
            return (int)((s - startSample) * (double)this.Width / hScale);
        }

        // Chooses an appropriate number of samples to create a time stamp tick at
        private int setSecDenom()
        {
            int newDenom = (int)(Math.Pow(2, (int)Math.Log((double)(endSample - startSample) / sampleRate, 2) - 3) * sampleRate);
            if (newDenom != secDenom){
                secDenom = newDenom;
                foreach (KeyValuePair<int, Label> kvp in timeStamp)
                    this.BeginInvoke(new MethodInvoker(() =>
                    {
                        this.Controls.Remove(kvp.Value);
                    }));

                timeStamp.Clear();
            }
            return secDenom;
        }

        // Sets the end of the selection area based on an X coordinate
        private void setEndSampleFromX(int x)
        {
            Invalidate(selSpace);
            selEnd = (int)(x * ((double)hScale / this.Width)) + startSample;
            selSpace.Width = (int)((selEnd - selStart) * (double)this.Width / hScale);
        }

        // Deselects the current selected area
        private void deselect()
        {
            desel = true;
            Invalidate(new Rectangle(selSpace.X - 1, selSpace.Y, selSpace.Width + 2, selSpace.Height));
            desel = false;
        }

        // Copies selected samples to the clipboard
        private void copy()
        {
            byte[] c = new byte[(selEnd - selStart) * sampleBit / 8];
            int offset = (selStart * sampleBit / 8);
            for (int i = 0; i < c.Length; i++)
                c[i] = samples[i + offset];
            main.clipboard.setClipboard(c, sampleRate, sampleBit);
        }

        // Copies on context menu click
        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            copy();
        }

        // Pastes on context menu click
        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            paste();
        }

        // Cuts on context menu click
        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cut();
        }

        // Inserts the samples on the clipboard to the wave
        private void paste()
        {
            if (main.clipboard.getQuantization() == sampleBit)
            {
                byte[] p;
                List<byte> sList = samples.ToList();
                if ((p = main.clipboard.getData()).Length != 0)
                {
                    if (sampleRate < main.clipboard.getSampleRate())
                    {
                        p = downSampled(p, main.clipboard.getSampleRate(), sampleRate);
                    }
                    else if (sampleRate > main.clipboard.getSampleRate())
                    {
                        p = upSampled(p, main.clipboard.getSampleRate(), sampleRate);
                    }
                    sList.RemoveRange(selStart * sampleBit / 8, (selEnd - selStart) * sampleBit / 8);
                    sList.InsertRange(selStart * sampleBit / 8, p);
                    samples = sList.ToArray();
                    selEnd = selStart * sampleBit / 8 + p.Length;
                    selSpace.Width = (int)(p.Length * 8 / sampleBit * this.Width / ((double)hScale));
                    dataLen = (uint)samples.Length;
                    numSamples = (int)(dataLen * sampleBit / 8);
                }
                Invalidate();
            }
            else
            {
                MessageBox.Show("Quantization level of clipboard is not compatible with this audio data.",
                    "Data mismatch", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void cut()
        {
            copy();
            delete();
        }

        // Tick method for the scroll timer
        private void scrollTime_Tick(object sender, EventArgs e)
        {
            if (hScroll.Value == hScroll.Maximum)
                scrollTime.Stop();
            if (hScroll.Value + hScale / 10 < hScroll.Maximum)
                hScroll.Value += hScale / 10;
            else
                hScroll.Value = hScroll.Maximum;
            hScroll_Scroll(this, null);
        }

        // removes samples from the area selected
        private void delete()
        {
            List<byte> sList = samples.ToList();
            sList.RemoveRange(selStart * sampleBit / 8, (selEnd - selStart) * sampleBit / 8);
            samples = sList.ToArray();
            selEnd = selStart;
            selSpace.Width = 0;
            dataLen = (uint)samples.Length;
            numSamples = (int)(dataLen * 8 / sampleBit);
            Invalidate();
        }

        // Plays the wave through recording window
        public unsafe void playWave()
        {
            SendSamples(samples, (UInt32)samples.Length);
            DlgProc(recDlg, WM_COMMAND, IDC_PLAY_BEG, 0);
        }

        // Stops playback through recording window
        public void stopPlay()
        {
            DlgProc(recDlg, WM_COMMAND, IDC_PLAY_END, 0);
        }

        // Pauses and resumes playback through recording window
        public void pause()
        {
            DlgProc(recDlg, WM_COMMAND, IDC_PLAY_PAUSE, 0);
        }

        // Begins recording through recording window
        public void record()
        {
            DlgProc(recDlg, WM_COMMAND, IDC_RECORD_BEG, 0);
        }

        // Stops recording through recording window
        public void stopRecord()
        {
            DlgProc(recDlg, WM_COMMAND, IDC_RECORD_END, 0);
            recListener = new Thread(new ThreadStart(recListenerProc));
            recListener.Start();
        }

        /* 
         * Proc for recListner thread.
         * Pulls data over from recording window then updates environment variables
         */ 
        private unsafe void recListenerProc()
        {
            recEvent.WaitOne();
            saveBuffer = GetSaveBuffer();
            dataLen = GetLength();
            samples = new byte[dataLen];
            for (uint i = 0; i < samples.Length; i++)
                samples[i] = saveBuffer[i];
            numSamples = (int)(dataLen * 8 / sampleBit);
            zoomBar.BeginInvoke(new MethodInvoker(() =>
            {
                zoomBar.Maximum = numSamples;
                zoomBar.Value = numSamples;
            }));
            hScale = numSamples;
            statusZoom.Text = "Zoom: " + this.Width * 100 / hScale + "%";
            startSample = 0;
            endSample = numSamples;
            setSecDenom();
            Invalidate();
        }

        // Sends selected samples to FrequencyDomain window for Discrete Fourier Analysis
        public void fourier()
        {
            byte[] tmp;
            if (sampleBit == 16) {
                tmp = new byte[(selEnd - selStart) * 2];
                Array.Copy(samples, selStart * 2, tmp, 0, tmp.Length);
            }
            else
            {
                tmp = new byte[selEnd - selStart];
                Array.Copy(samples, selStart, tmp, 0, tmp.Length);
            }

            FrequencyDomain fd = new FrequencyDomain(this, tmp, sampleBit, main.threaded);
            fd.Show();
        }

        /*
         * Filters the wave by convolution of the given filter
         * 
         * params:
         * f - Filter to use on the wave
         * caller - the FrequencyDomain form that requested the filter
         */
        public void filter(float[] f, Form caller)
        {
            // Create new arrays with padded 0s and pull in sample values
            filterResult = new float[samples.Length * 8 / sampleBit + f.Length - 1];
            fSamples = new int[samples.Length * 8 / sampleBit + f.Length - 1];
            if (sampleBit == 16)
                for (int i = 0; i < fSamples.Length - f.Length + 1; i++)
                    fSamples[i] = BitConverter.ToInt16(samples, i * 2);
            else
                for (int i = 0; i < samples.Length; i++)
                    fSamples[i] = samples[i];

            Stopwatch time = new Stopwatch();
            time.Start();
           
            // Start filter based on selected options
            if (main.threaded)
            {
                Thread[] worker = new Thread[Environment.ProcessorCount];
                FilterThread[] fData = new FilterThread[worker.Length];
                for (int i = 0; i < worker.Length; i++) {
                    fData[i] = new FilterThread(f, fSamples, i, numSamples / worker.Length, new FilterCallback(filterCallback));
                    worker[i] = new Thread(fData[i].filterProc);
                    worker[i].Start();
                }
                for (int i = 0; i < worker.Length; i++)
                    worker[i].Join();
            } else if(main.assembly)
            {
                convolution_filter(f, filterResult, fSamples, f.Length, numSamples);
            } else
                for(int i = 0; i < fSamples.Length - f.Length + 1; i++)
                    for (int j = 0; j < f.Length; j++)
                        filterResult[i] += fSamples[i + j] * f[j];

            time.Stop();

            // Converts sample values back into bytes and stores them in sample array
            if (sampleBit == 16)
                for (int i = 0; i < fSamples.Length - f.Length + 1; i++)
                {
                    byte[] sample = BitConverter.GetBytes((Int16)filterResult[i]);
                    samples[i * 2] = sample[0];
                    samples[i * 2 + 1] = sample[1];
                }
            else
                for (int i = 0; i < samples.Length; i++)
                    samples[i] = (byte)filterResult[i];
            caller.Close();
            pLog(main.threaded ? "Convolved Filter (Threaded): " : "Convolved Filter: ", 
                String.Format("{0}", time.ElapsedMilliseconds));
            Invalidate();
        }

        // Close button functionality
        private void closeViewer_Click(object sender, EventArgs e)
        {
            main.setSelected(-1);
            main.removeWave(this);
            this.Close();
        }

        // Callback function for FilterThreads to combine multi-threaded results
        private void filterCallback(float[] result)
        {
            for (int i = 0; i < result.Length; i++)
                filterResult[i] += result[i];
        }

        /* 
         * Log performance benchmark in the PerformanceLog
         * 
         * params:
         * message - Label for this log entry
         * time    - String representation of the time in milliseconds
         */
        public void pLog(string message, string time)
        {
            main.perfLog.log(message, time);
        }


        /*
         * Down-samples given samples to desired sample rate
         * 
         * params:
         * samples - samples to down-sample
         * sSR     - sample rate of the source samples
         * dSR     - desired sample rate to down-sample to
         * 
         * returns: array of sample bytes after being down-sampled
         */ 
        private byte[] downSampled(byte[] samples, int sSR, int dSR)
        {
            Stopwatch time = new Stopwatch();

            // Builds filter to remove frequencies more than half the destination sampling rate
            float[] filter = new float[samples.Length / 2];
            filter[0] = 1;
            int fBin = filter.Length * dSR / 2 / sSR;
            for (int i = 1; i <= fBin; i++)
            {
                filter[i] = 1;
                filter[filter.Length - i] = 1;
            }

            // Converts filter to time-domain by Inverse DFT
            float[] f = new float[filter.Length];
            time.Start();
            for (int t = 0; t < f.Length; t++)
            {
                for (int freq = 0; freq < f.Length; freq++)
                {
                    f[t] += (float)(filter[freq] * Math.Cos(2 * Math.PI * t * freq / samples.Length) - filter[freq] * Math.Sin(2 * Math.PI * t * freq / f.Length));
                }
                f[t] /= f.Length;
            }
            time.Stop();
            pLog("IDFT", String.Format("{0}", time.ElapsedMilliseconds));
            time.Reset();

            // Builds arrays to hold samples with padded 0s
            filterResult = new float[samples.Length * 8 / sampleBit + f.Length - 1];
            fSamples = new int[samples.Length * 8 / sampleBit + f.Length - 1];
            if (sampleBit == 16)
                for (int i = 0; i < fSamples.Length - f.Length + 1; i++)
                    fSamples[i] = BitConverter.ToInt16(samples, i * 2);
            else
                for (int i = 0; i < samples.Length; i++)
                    fSamples[i] = samples[i];

            time.Start();

            // Filters the samples by convolution based on options
            if (main.threaded)
            {
                Thread[] worker = new Thread[Environment.ProcessorCount];
                FilterThread[] fData = new FilterThread[worker.Length];
                for (int i = 0; i < worker.Length; i++)
                {
                    fData[i] = new FilterThread(f, fSamples, i, (fSamples.Length - f.Length) / worker.Length, new FilterCallback(filterCallback));
                    worker[i] = new Thread(fData[i].filterProc);
                    worker[i].Start();
                }
                for (int i = 0; i < worker.Length; i++)
                    worker[i].Join();
            }
            else
                for (int i = 0; i < fSamples.Length - f.Length + 1; i++)
                    for (int j = 0; j < f.Length; j++)
                        filterResult[i] += fSamples[i + j] * f[j];

            time.Stop();
            pLog(main.threaded ? "Convolved Filter (Threaded): " : "Convolved Filter: ",
                String.Format("{0}", time.ElapsedMilliseconds));

            // Creates a ratio in lowest terms of destination samples to source samples
            int gcd = GCD(dSR, sSR);
            sSR /= gcd;
            dSR /= gcd;
            float[] result = new float[(int)(((double)(fSamples.Length - f.Length) / sSR) * dSR)];
            bool skip = false;
            int sCount = 0;

            // Builds sample array by skipping samples to match new sample rate
            for (int i = 1, rCount = 0; i < filterResult.Length - f.Length; i++)
            {
                if (!skip)
                {
                    result[rCount++] = filterResult[i - 1];
                    if (i % dSR == 0)
                    {
                        skip = true;
                        sCount = 0;
                    }
                }
                else if (++sCount == sSR - dSR)
                    skip = false;
            }

            // Converts samples to byte array and returns the result
            byte[] dsResult = new byte[result.Length * sampleBit / 8];
            if (sampleBit == 16)
                for (int i = 0; i < fSamples.Length - f.Length + 1; i++)
                {
                    byte[] sample = BitConverter.GetBytes((Int16)result[i]);
                    dsResult[i * 2] = sample[0];
                    dsResult[i * 2 + 1] = sample[1];
                }
            else
                for (int i = 0; i < result.Length; i++)
                    dsResult[i] = (byte)result[i];
            return dsResult;
        }

        /*
         * NOTE: DOESN'T WORK
         * 
         * Up-samples given samples to desired sample rate
         * 
         * params:
         * samples - samples to down-sample
         * sSR     - sample rate of the source samples
         * dSR     - desired sample rate to up-sample to
         * 
         * returns: array of sample bytes after being up-sampled
         */
        private byte[] upSampled(byte[] samples, int sSR, int dSR)
        {
            Stopwatch time = new Stopwatch();
            float[] filter = new float[samples.Length / 2];
            filter[0] = 1;
            int fBin = filter.Length * sSR / 2 / dSR;
            for (int i = 1; i <= fBin; i++)
            {
                filter[i] = 1;
                filter[filter.Length - i] = 1;
            }
            float[] f = new float[filter.Length];
            time.Start();
            for (int t = 0; t < f.Length; t++)
            {
                for (int freq = 0; freq < f.Length; freq++)
                {
                    f[t] += (float)(filter[freq] * Math.Cos(2 * Math.PI * t * freq / samples.Length) - filter[freq] * Math.Sin(2 * Math.PI * t * freq / f.Length));
                }
                f[t] /= f.Length;
            }
            time.Stop();
            pLog("IDFT", String.Format("{0}", time.ElapsedMilliseconds));
            time.Reset();

            filterResult = new float[samples.Length * 8 / sampleBit + f.Length - 1];
            fSamples = new int[samples.Length * 8 / sampleBit + f.Length - 1];
            if (sampleBit == 16)
                for (int i = 0; i < fSamples.Length - f.Length + 1; i++)
                    fSamples[i] = BitConverter.ToInt16(samples, i * 2);
            else
                for (int i = 0; i < samples.Length; i++)
                    fSamples[i] = samples[i];

            int gcd = GCD(dSR, sSR);
            sSR /= gcd;
            dSR /= gcd;
            float[] result = new float[(int)(((double)(fSamples.Length - f.Length) / sSR) * dSR) + f.Length];
            bool skip = false;
            int sCount = 0;
            float last = 0;
            for (int i = 1, rCount = 0; i < result.Length - f.Length; i++)
            {
                if (!skip)
                {
                    result[i - 1] = filterResult[rCount++];
                    if (i % sSR == 0)
                    {
                        last = result[i];
                        sCount = 0;
                        skip = true;
                    }
                }
                else
                {
                    result[i - 1] = last;
                    if (++sCount == dSR - sSR)
                        skip = false;
                }
            }

            time.Start();
            if (main.threaded)
            {
                Thread[] worker = new Thread[Environment.ProcessorCount];
                FilterThread[] fData = new FilterThread[worker.Length];
                for (int i = 0; i < worker.Length; i++)
                {
                    fData[i] = new FilterThread(f, fSamples, i, (fSamples.Length - f.Length) / worker.Length, new FilterCallback(filterCallback));
                    worker[i] = new Thread(fData[i].filterProc);
                    worker[i].Start();
                }
                for (int i = 0; i < worker.Length; i++)
                    worker[i].Join();
            }
            else
                for (int i = 0; i < fSamples.Length - f.Length + 1; i++)
                    for (int j = 0; j < f.Length; j++)
                        filterResult[i] += fSamples[i + j] * f[j];
            time.Stop();
            pLog(main.threaded ? "Convolved Filter (Threaded): " : "Convolved Filter: ",
                String.Format("{0}", time.ElapsedMilliseconds));


            byte[] usResult = new byte[(fSamples.Length - f.Length) * sampleBit / 8];
            if (sampleBit == 16)
                for (int i = 0; i < fSamples.Length - f.Length + 1; i++)
                {
                    byte[] sample = BitConverter.GetBytes((Int16)fSamples[i]);
                    usResult[i * 2] = sample[0];
                    usResult[i * 2 + 1] = sample[1];
                }
            else
                for (int i = 0; i < usResult.Length; i++)
                    usResult[i] = (byte)fSamples[i];
            return usResult;
        }

        /*
         * Calculates greates common denominator of two numbers
         * 
         * params:
         * a, b - integers to reduce
         * 
         * return:
         * lowest common integer denominator
         */ 
        private int GCD(int a, int b)
        {
            return b == 0 ? a : GCD(b, a % b);
        }

        /*
         * Thread class for convolution filters.
         */ 
        private class FilterThread
        {
            private int threadNum;
            private int chunkSize;
            private float[] filter;
            private float[] filterResult;
            private int[] fSamples;
            private FilterCallback callback;

            /*
             * Constructor.
             * 
             * params:
             * f - filter to use in convolution
             * samples - source samples to filter
             * threadNum - thread index for this thread
             * chunkSize - number of samples to filter
             * cb - callback function to run after completion
             */ 
            public FilterThread(float[] f, int[] samples, int threadNum, int chunkSize, FilterCallback cb)
            {
                filter = f;
                this.threadNum = threadNum;
                this.chunkSize = chunkSize;
                filterResult = new float[(threadNum + 1) * chunkSize];
                fSamples = samples;
                callback = cb;
            }

            // Proc for the filter thread
            public void filterProc()
            {
                for (int i = threadNum * chunkSize; i < filterResult.Length; i++)
                    for (int j = 0; j < filter.Length; j++)
                        filterResult[i] += fSamples[i + j] * filter[j];
                callback?.Invoke(filterResult);
            }
        }

        // declaration for FilterCallback method
        private delegate void FilterCallback(float[] result);
    }
}