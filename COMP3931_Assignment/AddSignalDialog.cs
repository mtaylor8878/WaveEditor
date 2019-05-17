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
     * Dialog used to gather information on signal to be added to WaveViewer
     */ 
    public partial class AddSignalDialog : Form
    {
        private WaveViewer caller;
        public AddSignalDialog(WaveViewer caller)
        {
            this.caller = caller;
            InitializeComponent();
        }

        // Calls addWave function on WaveViewer that called this AddSignalDialog
        private void submit_Click(object sender, EventArgs e)
        {
            RadioButton buttons = waveType.Controls.OfType<RadioButton>().FirstOrDefault(n => n.Checked);
            double phase = Convert.ToDouble(phaseShift.Text);
            if (buttons.Name == "typeCosine")
                phase += 90;
            caller.addWave(Convert.ToDouble(frequency.Text), ampBar.Value / 100d, phase);
            Close();
        }
    }
}
