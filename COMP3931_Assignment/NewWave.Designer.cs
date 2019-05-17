namespace COMP3931_Assignment
{
    partial class NewWave
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
            this.sampleRate = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.submit = new System.Windows.Forms.Button();
            this.qbox = new System.Windows.Forms.GroupBox();
            this.eight = new System.Windows.Forms.RadioButton();
            this.sixteen = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.sampleRate)).BeginInit();
            this.qbox.SuspendLayout();
            this.SuspendLayout();
            // 
            // sampleRate
            // 
            this.sampleRate.Location = new System.Drawing.Point(203, 15);
            this.sampleRate.Margin = new System.Windows.Forms.Padding(4);
            this.sampleRate.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.sampleRate.Name = "sampleRate";
            this.sampleRate.Size = new System.Drawing.Size(160, 22);
            this.sampleRate.TabIndex = 0;
            this.sampleRate.Value = new decimal(new int[] {
            22050,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 17);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Sample Rate";
            // 
            // submit
            // 
            this.submit.Location = new System.Drawing.Point(140, 108);
            this.submit.Margin = new System.Windows.Forms.Padding(4);
            this.submit.Name = "submit";
            this.submit.Size = new System.Drawing.Size(100, 28);
            this.submit.TabIndex = 2;
            this.submit.Text = "Create";
            this.submit.UseVisualStyleBackColor = true;
            this.submit.Click += new System.EventHandler(this.submit_Click);
            // 
            // qbox
            // 
            this.qbox.Controls.Add(this.sixteen);
            this.qbox.Controls.Add(this.eight);
            this.qbox.Location = new System.Drawing.Point(19, 47);
            this.qbox.Name = "qbox";
            this.qbox.Size = new System.Drawing.Size(344, 55);
            this.qbox.TabIndex = 3;
            this.qbox.TabStop = false;
            this.qbox.Text = "Quantization";
            // 
            // eight
            // 
            this.eight.AutoSize = true;
            this.eight.Checked = true;
            this.eight.Location = new System.Drawing.Point(18, 21);
            this.eight.Name = "eight";
            this.eight.Size = new System.Drawing.Size(98, 21);
            this.eight.TabIndex = 0;
            this.eight.TabStop = true;
            this.eight.Text = "8-Bit Audio";
            this.eight.UseVisualStyleBackColor = true;
            // 
            // sixteen
            // 
            this.sixteen.AutoSize = true;
            this.sixteen.Location = new System.Drawing.Point(184, 21);
            this.sixteen.Name = "sixteen";
            this.sixteen.Size = new System.Drawing.Size(106, 21);
            this.sixteen.TabIndex = 1;
            this.sixteen.TabStop = true;
            this.sixteen.Text = "16-Bit Audio";
            this.sixteen.UseVisualStyleBackColor = true;
            // 
            // NewWave
            // 
            this.AcceptButton = this.submit;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 143);
            this.Controls.Add(this.qbox);
            this.Controls.Add(this.submit);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.sampleRate);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "NewWave";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Create New Wave";
            ((System.ComponentModel.ISupportInitialize)(this.sampleRate)).EndInit();
            this.qbox.ResumeLayout(false);
            this.qbox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown sampleRate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button submit;
        private System.Windows.Forms.GroupBox qbox;
        private System.Windows.Forms.RadioButton sixteen;
        private System.Windows.Forms.RadioButton eight;
    }
}