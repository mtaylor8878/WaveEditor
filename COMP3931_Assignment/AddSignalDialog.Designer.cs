namespace COMP3931_Assignment
{
    partial class AddSignalDialog
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
            this.waveType = new System.Windows.Forms.GroupBox();
            this.typeCosine = new System.Windows.Forms.RadioButton();
            this.typeSine = new System.Windows.Forms.RadioButton();
            this.frequency = new System.Windows.Forms.TextBox();
            this.freqLabel = new System.Windows.Forms.Label();
            this.freqAfter = new System.Windows.Forms.Label();
            this.ampLabel = new System.Windows.Forms.Label();
            this.submit = new System.Windows.Forms.Button();
            this.phaseAfter = new System.Windows.Forms.Label();
            this.phaseLabel = new System.Windows.Forms.Label();
            this.phaseShift = new System.Windows.Forms.TextBox();
            this.ampBar = new System.Windows.Forms.TrackBar();
            this.waveType.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ampBar)).BeginInit();
            this.SuspendLayout();
            // 
            // waveType
            // 
            this.waveType.Controls.Add(this.typeCosine);
            this.waveType.Controls.Add(this.typeSine);
            this.waveType.Location = new System.Drawing.Point(16, 16);
            this.waveType.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.waveType.Name = "waveType";
            this.waveType.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.waveType.Size = new System.Drawing.Size(266, 60);
            this.waveType.TabIndex = 0;
            this.waveType.TabStop = false;
            this.waveType.Text = "Wave Type";
            // 
            // typeCosine
            // 
            this.typeCosine.AutoSize = true;
            this.typeCosine.Location = new System.Drawing.Point(169, 23);
            this.typeCosine.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.typeCosine.Name = "typeCosine";
            this.typeCosine.Size = new System.Drawing.Size(72, 21);
            this.typeCosine.TabIndex = 1;
            this.typeCosine.TabStop = true;
            this.typeCosine.Text = "Cosine";
            this.typeCosine.UseVisualStyleBackColor = true;
            // 
            // typeSine
            // 
            this.typeSine.AutoSize = true;
            this.typeSine.Location = new System.Drawing.Point(8, 23);
            this.typeSine.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.typeSine.Name = "typeSine";
            this.typeSine.Size = new System.Drawing.Size(57, 21);
            this.typeSine.TabIndex = 0;
            this.typeSine.TabStop = true;
            this.typeSine.Text = "Sine";
            this.typeSine.UseVisualStyleBackColor = true;
            // 
            // frequency
            // 
            this.frequency.Location = new System.Drawing.Point(117, 87);
            this.frequency.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.frequency.Name = "frequency";
            this.frequency.Size = new System.Drawing.Size(140, 22);
            this.frequency.TabIndex = 1;
            this.frequency.Text = "100";
            this.frequency.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // freqLabel
            // 
            this.freqLabel.AutoSize = true;
            this.freqLabel.Location = new System.Drawing.Point(16, 87);
            this.freqLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.freqLabel.Name = "freqLabel";
            this.freqLabel.Size = new System.Drawing.Size(75, 17);
            this.freqLabel.TabIndex = 2;
            this.freqLabel.Text = "Frequency";
            // 
            // freqAfter
            // 
            this.freqAfter.AutoSize = true;
            this.freqAfter.Location = new System.Drawing.Point(257, 89);
            this.freqAfter.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.freqAfter.Name = "freqAfter";
            this.freqAfter.Size = new System.Drawing.Size(25, 17);
            this.freqAfter.TabIndex = 3;
            this.freqAfter.Text = "Hz";
            // 
            // ampLabel
            // 
            this.ampLabel.AutoSize = true;
            this.ampLabel.Location = new System.Drawing.Point(291, 20);
            this.ampLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ampLabel.Name = "ampLabel";
            this.ampLabel.Size = new System.Drawing.Size(70, 17);
            this.ampLabel.TabIndex = 5;
            this.ampLabel.Text = "Amplitude";
            // 
            // submit
            // 
            this.submit.Location = new System.Drawing.Point(143, 143);
            this.submit.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.submit.Name = "submit";
            this.submit.Size = new System.Drawing.Size(100, 28);
            this.submit.TabIndex = 7;
            this.submit.Text = "Add";
            this.submit.UseVisualStyleBackColor = true;
            this.submit.Click += new System.EventHandler(this.submit_Click);
            // 
            // phaseAfter
            // 
            this.phaseAfter.AutoSize = true;
            this.phaseAfter.Location = new System.Drawing.Point(257, 113);
            this.phaseAfter.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.phaseAfter.Name = "phaseAfter";
            this.phaseAfter.Size = new System.Drawing.Size(14, 17);
            this.phaseAfter.TabIndex = 10;
            this.phaseAfter.Text = "°";
            // 
            // phaseLabel
            // 
            this.phaseLabel.AutoSize = true;
            this.phaseLabel.Location = new System.Drawing.Point(16, 116);
            this.phaseLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.phaseLabel.Name = "phaseLabel";
            this.phaseLabel.Size = new System.Drawing.Size(80, 17);
            this.phaseLabel.TabIndex = 9;
            this.phaseLabel.Text = "Phase Shift";
            // 
            // phaseShift
            // 
            this.phaseShift.Location = new System.Drawing.Point(117, 113);
            this.phaseShift.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.phaseShift.Name = "phaseShift";
            this.phaseShift.Size = new System.Drawing.Size(140, 22);
            this.phaseShift.TabIndex = 8;
            this.phaseShift.Text = "0";
            this.phaseShift.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // ampBar
            // 
            this.ampBar.Location = new System.Drawing.Point(311, 40);
            this.ampBar.Maximum = 100;
            this.ampBar.Name = "ampBar";
            this.ampBar.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.ampBar.Size = new System.Drawing.Size(56, 104);
            this.ampBar.TabIndex = 11;
            this.ampBar.Value = 50;
            // 
            // AddSignalDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 185);
            this.Controls.Add(this.ampBar);
            this.Controls.Add(this.phaseAfter);
            this.Controls.Add(this.phaseLabel);
            this.Controls.Add(this.phaseShift);
            this.Controls.Add(this.submit);
            this.Controls.Add(this.ampLabel);
            this.Controls.Add(this.freqAfter);
            this.Controls.Add(this.freqLabel);
            this.Controls.Add(this.frequency);
            this.Controls.Add(this.waveType);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "AddSignalDialog";
            this.Text = "Adding a Signal";
            this.waveType.ResumeLayout(false);
            this.waveType.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ampBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox waveType;
        private System.Windows.Forms.RadioButton typeCosine;
        private System.Windows.Forms.RadioButton typeSine;
        private System.Windows.Forms.TextBox frequency;
        private System.Windows.Forms.Label freqLabel;
        private System.Windows.Forms.Label freqAfter;
        private System.Windows.Forms.Label ampLabel;
        private System.Windows.Forms.Button submit;
        private System.Windows.Forms.Label phaseAfter;
        private System.Windows.Forms.Label phaseLabel;
        private System.Windows.Forms.TextBox phaseShift;
        private System.Windows.Forms.TrackBar ampBar;
    }
}