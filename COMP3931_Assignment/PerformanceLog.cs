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
     * Log window that holds information on submitted benchmark times.
     */  
    public partial class PerformanceLog : Form
    {
        private MainContainer main; // Reference to the main container that this belongs to

        /*
         * Basic constructor.
         * params:
         * m - reference to the main container that created the PerformanceLog
         */ 
        public PerformanceLog(MainContainer m)
        {
            main = m;
            InitializeComponent();
            logLabel.AppendText("Task" + Environment.NewLine);
            logValue.AppendText("Time (ms)" + Environment.NewLine);
        }

        /*
         * Appends an entry to the PerformanceLog
         * 
         * params:
         * label - The label describing this log entry
         * time  - A string respresentation of the time in milliseconds to execute the action
         */ 
        public void log(String label, String time)
        {
            logLabel.AppendText(label + Environment.NewLine);
            logValue.AppendText(time + Environment.NewLine);
        }

        // Override of the default close button functionality to hide window instead of closing
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            e.Cancel = true;
            main.hidePerformance();
        }
    }
}
