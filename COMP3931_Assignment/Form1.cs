using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace COMP3931_Assignment
{
    /*
     * The main container form that contains the program
     */ 
    public partial class MainContainer : Form
    {
        private List<WaveViewer> waves; // A list of all wave objects
        private int selected; // The index of the currently selected wave
        private int recording, playing; // Index of the wave that is currently recording or playing respectively
        private EventWaitHandle playEvent = 
                new EventWaitHandle(false, EventResetMode.AutoReset, "PlaybackFinish"); // Event that is triggered by playback completing
        private Thread playListener; // Thread that listens for the playEvent Event to trigger
        public Clipboard clipboard; // Clipboard object that stores current copied and cut samples
        public bool threaded; // Enables or disables threading
        public bool assembly; // Enables or disables assembly code (currently not implemented)
        public PerformanceLog perfLog; // Reference to the PerformanceLog for this program

        // Default constructor that initializes environment variables
        public MainContainer()
        {
            waves = new List<WaveViewer>();
            clipboard = new Clipboard();
            selected = -1;
            recording = -1;
            playing = -1;
            threaded = true;
            assembly = false;
            InitializeComponent();
            perfLog = new PerformanceLog(this);
            perfLog.MdiParent = this;
        }

        // Toolstrip menu trigger that brings up a NewWave window to create a new WaveViewer object
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewWave newPrompt = new NewWave(this);
            newPrompt.Show();
        }

        // Toolstrip menu trigger that brings up the menu to add a signal to the current wave
        private void addWaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddSignalDialog newSignal = new AddSignalDialog(waves[selected]);
            newSignal.Show();
        }

        // Adds provided WaveViewer to the main waves List
        public void addViewer(WaveViewer wv)
        {
            waves.Add(wv);
        }

        // Returns the number of WaveViewers that are active
        public int getWaveCount()
        {
            return waves.Count;
        }

        // Gets the index of the requested WaveViewer
        public int getWaveViewer(WaveViewer wv)
        {
            return waves.IndexOf(wv);
        }

        /*
         * Sets the selected WaveViewer, and enables / disables controls accordingly
         * 
         * params:
         * i - index of WaveViewer to select
         */ 
        public void setSelected(int i)
        {
            selected = i;
            if (selected == -1)
            {
                generateToolStripMenuItem.Enabled = false;
                exportToolStripMenuItem.Enabled = false;
                analyzeToolStripMenuItem.Enabled = false;
                playButton.Enabled = false;
                recBtn.Enabled = false;
            }
            else
            {
                generateToolStripMenuItem.Enabled = true;
                exportToolStripMenuItem.Enabled = true;
                analyzeToolStripMenuItem.Enabled = true;
                playButton.Enabled = true;
                recBtn.Enabled = true;
            }
        }

        // Toolstrip menu trigger that opens the OpenFileDialog to import a .wav file into a new WaveViewer
        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileStream fStream = null;
            OpenFileDialog fileDialog = new OpenFileDialog();

            fileDialog.Filter = "Wave Files|*.wav|All Files|*.*";
            fileDialog.FilterIndex = 1;


            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((fStream = (FileStream)fileDialog.OpenFile()) != null)
                    {
                        WaveViewer newWave = new WaveViewer(this, fStream);
                        newWave.Left = 10;
                        newWave.Top = 10 + (newWave.Height + 10) * getWaveCount();
                        newWave.Width = this.Width - 40;
                        newWave.MdiParent = this;
                        newWave.Show();
                        addViewer(newWave);
                        setSelected(getWaveCount() - 1);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read from disk. Message: " + ex.Message);
                }
            }
        }

        // Toolstrip menu trigger to open a SaveFileDialog to save selected WaveViewer to a .wav file
        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Wave File|*.wav";
            saveDialog.Title = "Export Wave File";
            saveDialog.ShowDialog();

            // If the file name is not an empty string open it for saving.
            if (saveDialog.FileName != "")
            {
                waves[selected].exportToWave(saveDialog.FileName);
            }
        }

        // Plays the currently selected wave and updates control states
        private void playButton_Click(object sender, EventArgs e)
        {
            if (playing == -1)
            {
                waves[selected].playWave();
                stopButton.Enabled = true;
                playButton.Text = "Pause";
                playing = selected;
                recBtn.Enabled = false;
                playListener = new Thread(new ThreadStart(playListenerProc));
                playListener.Start();
            } else
            {
                playButton.Text = playButton.Text == "Pause" ? "Resume" : "Pause";
                waves[playing].pause();
            }
        }

        // Starts recording on the selected WaveViewer object and updates control states
        private void button1_Click(object sender, EventArgs e)
        {
            if (recording == -1)
            {
                waves[selected].record();
                recBtn.Text = "Stop";
                playButton.Enabled = false;
                recording = selected;
            }
            else
            {
                waves[recording].stopRecord();
                recBtn.Text = "Record";
                playButton.Enabled = true;
                recording = -1;
            }
        }

        // Stops the current recording
        private void stopButton_Click(object sender, EventArgs e)
        {
            waves[playing].stopPlay();
            stopButton.Enabled = false;
            playButton.Text = "Play";
            playing = -1;
        }

        // Toolstrip menu trigger that executes DFT on the selected WaveViewer
        private void fourierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            waves[selected].fourier();
        }

        // Toolstrip menu trigger that toggles Threading capabilities
        private void threadedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            threaded = !threaded;
            if (threaded)
            {
                assembly = false;
                assemblyToolStripMenuItem.Checked = false;
            }
        }

        // Toolstrip menu trigger that shows/hides the PerformanceLog
        private void performanceLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (performanceLogToolStripMenuItem.Checked)
                perfLog.Show();
            else
                perfLog.Hide();
        }

        // Proc for the playListener thread to update control states once playback is completed
        private void playListenerProc()
        {
            playEvent.WaitOne();
            stopButton.BeginInvoke(new MethodInvoker(() =>
            {
                stopButton.Enabled = false;
            }));
            playButton.BeginInvoke(new MethodInvoker(() =>
            {
                playButton.Text = "Play";
            }));
            recBtn.BeginInvoke(new MethodInvoker(() =>
            {
                recBtn.Enabled = true;
            }));
            playing = -1;
        }

        // Removes the requested WaveViewer from the waves List
        public void removeWave(WaveViewer wave)
        {
            for (int i = waves.IndexOf(wave); i < waves.Count; i++)
                waves[i].Top -= wave.Height + 10;
            waves.Remove(wave);
        }

        // Toolstrip menu trigger that toggles Assembly capabilites on calculations
        // NOTE: Assembly is NOT properly implemented yet
        private void assemblyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            assembly = !assembly;
            if (assembly)
            {
                threaded = false;
                threadedToolStripMenuItem.Checked = false;
            }

        }

        // Hides the PerformanceLog
        public void hidePerformance()
        {
            perfLog.Hide();
            performanceLogToolStripMenuItem.Checked = false;
        }
    }

    /*
     * Clipboard object that stores samples to be pasted in another part of a wave or another WaveViewer
     */ 
    public class Clipboard
    {
        private byte[] data; // The samples stored
        private int sampleRate; // The sampling rate of the stored samples
        private int quantization; // Quantization level of the stored samples

        // Constructor that initializes variables
        public Clipboard()
        {
            data = new byte[0];
            sampleRate = 0;
            quantization = 0;
        }

        /*
         * Sets the Clipboard's data to the given data
         * 
         * params:
         * d - byte array of samples
         * sr - sampling rate of the given samples
         * q - quantization level of the given samples
         */ 
        public void setClipboard(byte[] d, int sr, int q)
        {
            data = d;
            sampleRate = sr;
            quantization = q;
        }

        // Returns the byte array of data
        public byte[] getData()
        {
            return data;
        }

        // Returns the sampling rate of Clipboard data
        public int getSampleRate()
        {
            return sampleRate;
        }

        // Returns the quantization level of the Clipboard data
        public int getQuantization()
        {
            return quantization;
        }
    }
}
