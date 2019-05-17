using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace COMP3931_Assignment
{
    /*
     * Gathers sampling rate and quantization level to create new WaveViewer object
     */ 
    public partial class NewWave : Form
    {
        private MainContainer caller; // Reference to the main container

        public NewWave(MainContainer caller)
        {
            this.caller = caller;
            InitializeComponent();
        }

        // Creates a new WaveViewer window with given specifications
        private void submit_Click(object sender, EventArgs e)
        {
            RadioButton buttons = qbox.Controls.OfType<RadioButton>().FirstOrDefault(n => n.Checked);
            short quantization = (short)(buttons.Name == "sixteen" ? 16 : 8);
            WaveViewer newWave = new WaveViewer(caller, (int)sampleRate.Value, quantization);
            newWave.Left = 10;
            newWave.Top = 10 + (newWave.Height + 10) * caller.getWaveCount();
            newWave.Width = caller.Width - 40;
            newWave.MdiParent = caller;
            newWave.Show();
            caller.addViewer(newWave);
            caller.setSelected(caller.getWaveCount() - 1);
            Close();
        }
    }
}
