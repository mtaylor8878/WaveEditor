namespace COMP3931_Assignment
{
    partial class MainContainer
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
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.generateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addWaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.analyzeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fourierToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.threadedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.performanceLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playButton = new System.Windows.Forms.Button();
            this.recBtn = new System.Windows.Forms.Button();
            this.stopButton = new System.Windows.Forms.Button();
            this.assemblyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.importToolStripMenuItem,
            this.exportToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(129, 26);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.Size = new System.Drawing.Size(129, 26);
            this.importToolStripMenuItem.Text = "Import";
            this.importToolStripMenuItem.Click += new System.EventHandler(this.importToolStripMenuItem_Click);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.Enabled = false;
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(129, 26);
            this.exportToolStripMenuItem.Text = "Export";
            this.exportToolStripMenuItem.Click += new System.EventHandler(this.exportToolStripMenuItem_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.generateToolStripMenuItem,
            this.analyzeToolStripMenuItem,
            this.optionsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1011, 28);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // generateToolStripMenuItem
            // 
            this.generateToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addWaveToolStripMenuItem});
            this.generateToolStripMenuItem.Enabled = false;
            this.generateToolStripMenuItem.Name = "generateToolStripMenuItem";
            this.generateToolStripMenuItem.Size = new System.Drawing.Size(81, 24);
            this.generateToolStripMenuItem.Text = "Generate";
            // 
            // addWaveToolStripMenuItem
            // 
            this.addWaveToolStripMenuItem.Name = "addWaveToolStripMenuItem";
            this.addWaveToolStripMenuItem.Size = new System.Drawing.Size(152, 26);
            this.addWaveToolStripMenuItem.Text = "Add Wave";
            this.addWaveToolStripMenuItem.Click += new System.EventHandler(this.addWaveToolStripMenuItem_Click);
            // 
            // analyzeToolStripMenuItem
            // 
            this.analyzeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fourierToolStripMenuItem});
            this.analyzeToolStripMenuItem.Enabled = false;
            this.analyzeToolStripMenuItem.Name = "analyzeToolStripMenuItem";
            this.analyzeToolStripMenuItem.Size = new System.Drawing.Size(73, 24);
            this.analyzeToolStripMenuItem.Text = "Analyze";
            // 
            // fourierToolStripMenuItem
            // 
            this.fourierToolStripMenuItem.Name = "fourierToolStripMenuItem";
            this.fourierToolStripMenuItem.Size = new System.Drawing.Size(130, 26);
            this.fourierToolStripMenuItem.Text = "Fourier";
            this.fourierToolStripMenuItem.Click += new System.EventHandler(this.fourierToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.threadedToolStripMenuItem,
            this.assemblyToolStripMenuItem,
            this.performanceLogToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(73, 24);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // threadedToolStripMenuItem
            // 
            this.threadedToolStripMenuItem.Checked = true;
            this.threadedToolStripMenuItem.CheckOnClick = true;
            this.threadedToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.threadedToolStripMenuItem.Name = "threadedToolStripMenuItem";
            this.threadedToolStripMenuItem.Size = new System.Drawing.Size(196, 26);
            this.threadedToolStripMenuItem.Text = "Threaded";
            this.threadedToolStripMenuItem.Click += new System.EventHandler(this.threadedToolStripMenuItem_Click);
            // 
            // performanceLogToolStripMenuItem
            // 
            this.performanceLogToolStripMenuItem.BackColor = System.Drawing.SystemColors.Control;
            this.performanceLogToolStripMenuItem.CheckOnClick = true;
            this.performanceLogToolStripMenuItem.Name = "performanceLogToolStripMenuItem";
            this.performanceLogToolStripMenuItem.Size = new System.Drawing.Size(196, 26);
            this.performanceLogToolStripMenuItem.Text = "Performance Log";
            this.performanceLogToolStripMenuItem.Click += new System.EventHandler(this.performanceLogToolStripMenuItem_Click);
            // 
            // playButton
            // 
            this.playButton.Enabled = false;
            this.playButton.Location = new System.Drawing.Point(384, 0);
            this.playButton.Margin = new System.Windows.Forms.Padding(4);
            this.playButton.Name = "playButton";
            this.playButton.Size = new System.Drawing.Size(100, 28);
            this.playButton.TabIndex = 3;
            this.playButton.Text = "Play";
            this.playButton.UseVisualStyleBackColor = true;
            this.playButton.Click += new System.EventHandler(this.playButton_Click);
            // 
            // recBtn
            // 
            this.recBtn.Enabled = false;
            this.recBtn.Location = new System.Drawing.Point(607, 0);
            this.recBtn.Margin = new System.Windows.Forms.Padding(4);
            this.recBtn.Name = "recBtn";
            this.recBtn.Size = new System.Drawing.Size(100, 28);
            this.recBtn.TabIndex = 5;
            this.recBtn.Text = "Record";
            this.recBtn.UseVisualStyleBackColor = true;
            this.recBtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // stopButton
            // 
            this.stopButton.Enabled = false;
            this.stopButton.Location = new System.Drawing.Point(497, 0);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(91, 28);
            this.stopButton.TabIndex = 7;
            this.stopButton.Text = "Stop";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // assemblyToolStripMenuItem
            // 
            this.assemblyToolStripMenuItem.CheckOnClick = true;
            this.assemblyToolStripMenuItem.Name = "assemblyToolStripMenuItem";
            this.assemblyToolStripMenuItem.Size = new System.Drawing.Size(196, 26);
            this.assemblyToolStripMenuItem.Text = "Assembly";
            this.assemblyToolStripMenuItem.Click += new System.EventHandler(this.assemblyToolStripMenuItem_Click);
            // 
            // MainContainer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1011, 449);
            this.Controls.Add(this.stopButton);
            this.Controls.Add(this.recBtn);
            this.Controls.Add(this.playButton);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainContainer";
            this.Text = "Wave Control";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem generateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addWaveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem analyzeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fourierToolStripMenuItem;
        private System.Windows.Forms.Button playButton;
        private System.Windows.Forms.Button recBtn;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem threadedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem performanceLogToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem assemblyToolStripMenuItem;
    }
}

