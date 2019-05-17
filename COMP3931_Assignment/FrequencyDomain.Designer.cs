namespace COMP3931_Assignment
{
    partial class FrequencyDomain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.windowStrip = new System.Windows.Forms.MenuStrip();
            this.windowingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.noneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.triangleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.blackmanHarrisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lowPassToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.filterLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.mouseLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.windowStrip.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // windowStrip
            // 
            this.windowStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.windowStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.windowingToolStripMenuItem,
            this.filterToolStripMenuItem});
            this.windowStrip.Location = new System.Drawing.Point(0, 0);
            this.windowStrip.Name = "windowStrip";
            this.windowStrip.Size = new System.Drawing.Size(568, 28);
            this.windowStrip.TabIndex = 0;
            this.windowStrip.Text = "windowStrip";
            // 
            // windowingToolStripMenuItem
            // 
            this.windowingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.noneToolStripMenuItem,
            this.triangleToolStripMenuItem,
            this.blackmanHarrisToolStripMenuItem});
            this.windowingToolStripMenuItem.Name = "windowingToolStripMenuItem";
            this.windowingToolStripMenuItem.Size = new System.Drawing.Size(97, 24);
            this.windowingToolStripMenuItem.Text = "Windowing";
            // 
            // noneToolStripMenuItem
            // 
            this.noneToolStripMenuItem.Checked = true;
            this.noneToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.noneToolStripMenuItem.Name = "noneToolStripMenuItem";
            this.noneToolStripMenuItem.Size = new System.Drawing.Size(193, 26);
            this.noneToolStripMenuItem.Text = "None";
            this.noneToolStripMenuItem.Click += new System.EventHandler(this.noneToolStripMenuItem_Click);
            // 
            // triangleToolStripMenuItem
            // 
            this.triangleToolStripMenuItem.Name = "triangleToolStripMenuItem";
            this.triangleToolStripMenuItem.Size = new System.Drawing.Size(193, 26);
            this.triangleToolStripMenuItem.Text = "Triangle";
            this.triangleToolStripMenuItem.Click += new System.EventHandler(this.triangleToolStripMenuItem_Click);
            // 
            // blackmanHarrisToolStripMenuItem
            // 
            this.blackmanHarrisToolStripMenuItem.Name = "blackmanHarrisToolStripMenuItem";
            this.blackmanHarrisToolStripMenuItem.Size = new System.Drawing.Size(193, 26);
            this.blackmanHarrisToolStripMenuItem.Text = "Blackman-Harris";
            this.blackmanHarrisToolStripMenuItem.Click += new System.EventHandler(this.blackmanHarrisToolStripMenuItem_Click);
            // 
            // filterToolStripMenuItem
            // 
            this.filterToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lowPassToolStripMenuItem});
            this.filterToolStripMenuItem.Name = "filterToolStripMenuItem";
            this.filterToolStripMenuItem.Size = new System.Drawing.Size(54, 24);
            this.filterToolStripMenuItem.Text = "Filter";
            // 
            // lowPassToolStripMenuItem
            // 
            this.lowPassToolStripMenuItem.Name = "lowPassToolStripMenuItem";
            this.lowPassToolStripMenuItem.Size = new System.Drawing.Size(142, 26);
            this.lowPassToolStripMenuItem.Text = "Low Pass";
            this.lowPassToolStripMenuItem.Click += new System.EventHandler(this.lowPassToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.filterLabel,
            this.mouseLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 403);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(568, 25);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // filterLabel
            // 
            this.filterLabel.Name = "filterLabel";
            this.filterLabel.Size = new System.Drawing.Size(75, 20);
            this.filterLabel.Text = "Filter: 0Hz";
            // 
            // mouseLabel
            // 
            this.mouseLabel.Name = "mouseLabel";
            this.mouseLabel.Size = new System.Drawing.Size(86, 20);
            this.mouseLabel.Text = "Mouse: 0Hz";
            // 
            // FrequencyDomain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(568, 428);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.windowStrip);
            this.MainMenuStrip = this.windowStrip;
            this.Name = "FrequencyDomain";
            this.Text = "FrequencyDomain";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FreqeuncyDomain_Paint);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FrequencyDomain_MouseMove);
            this.Resize += new System.EventHandler(this.FrequencyDomain_Resize);
            this.windowStrip.ResumeLayout(false);
            this.windowStrip.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip windowStrip;
        private System.Windows.Forms.ToolStripMenuItem windowingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem noneToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem triangleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem blackmanHarrisToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem filterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lowPassToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel filterLabel;
        private System.Windows.Forms.ToolStripStatusLabel mouseLabel;
    }
}