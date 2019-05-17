namespace COMP3931_Assignment
{
    partial class WaveViewer
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
            this.components = new System.ComponentModel.Container();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusBit = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusSampleRate = new System.Windows.Forms.ToolStripStatusLabel();
            this.sampleStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusZoom = new System.Windows.Forms.ToolStripStatusLabel();
            this.zoomBar = new System.Windows.Forms.TrackBar();
            this.hScroll = new System.Windows.Forms.HScrollBar();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scrollTime = new System.Windows.Forms.Timer(this.components);
            this.closeViewer = new System.Windows.Forms.Button();
            this.statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.zoomBar)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusBit,
            this.statusSampleRate,
            this.sampleStatus,
            this.statusZoom});
            this.statusStrip.Location = new System.Drawing.Point(0, 344);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip.Size = new System.Drawing.Size(667, 25);
            this.statusStrip.TabIndex = 1;
            this.statusStrip.Text = "statusStrip1";
            // 
            // statusBit
            // 
            this.statusBit.Name = "statusBit";
            this.statusBit.Size = new System.Drawing.Size(73, 20);
            this.statusBit.Text = "AudioBits";
            // 
            // statusSampleRate
            // 
            this.statusSampleRate.Name = "statusSampleRate";
            this.statusSampleRate.Size = new System.Drawing.Size(89, 20);
            this.statusSampleRate.Text = "SampleRate";
            // 
            // sampleStatus
            // 
            this.sampleStatus.AutoSize = false;
            this.sampleStatus.Name = "sampleStatus";
            this.sampleStatus.Size = new System.Drawing.Size(150, 20);
            this.sampleStatus.Text = "Sample Level: -32000";
            this.sampleStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // statusZoom
            // 
            this.statusZoom.AutoSize = false;
            this.statusZoom.Name = "statusZoom";
            this.statusZoom.Size = new System.Drawing.Size(92, 20);
            this.statusZoom.Text = "Zoom: 100%";
            this.statusZoom.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // zoomBar
            // 
            this.zoomBar.Location = new System.Drawing.Point(505, 342);
            this.zoomBar.Margin = new System.Windows.Forms.Padding(4);
            this.zoomBar.Name = "zoomBar";
            this.zoomBar.Size = new System.Drawing.Size(139, 56);
            this.zoomBar.TabIndex = 2;
            this.zoomBar.Scroll += new System.EventHandler(this.zoomBar_Scroll);
            this.zoomBar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.zoomBar_MouseUp);
            // 
            // hScroll
            // 
            this.hScroll.Location = new System.Drawing.Point(396, 345);
            this.hScroll.Name = "hScroll";
            this.hScroll.Size = new System.Drawing.Size(107, 17);
            this.hScroll.TabIndex = 3;
            this.hScroll.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScroll_Scroll);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.cutToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(119, 82);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(118, 26);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(118, 26);
            this.pasteToolStripMenuItem.Text = "Paste";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
            // 
            // cutToolStripMenuItem
            // 
            this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            this.cutToolStripMenuItem.Size = new System.Drawing.Size(118, 26);
            this.cutToolStripMenuItem.Text = "Cut";
            this.cutToolStripMenuItem.Click += new System.EventHandler(this.cutToolStripMenuItem_Click);
            // 
            // scrollTime
            // 
            this.scrollTime.Tick += new System.EventHandler(this.scrollTime_Tick);
            // 
            // closeViewer
            // 
            this.closeViewer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.closeViewer.Location = new System.Drawing.Point(643, -1);
            this.closeViewer.Name = "closeViewer";
            this.closeViewer.Size = new System.Drawing.Size(25, 25);
            this.closeViewer.TabIndex = 4;
            this.closeViewer.Text = "X";
            this.closeViewer.UseVisualStyleBackColor = true;
            this.closeViewer.Click += new System.EventHandler(this.closeViewer_Click);
            // 
            // WaveViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.ClientSize = new System.Drawing.Size(667, 369);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.closeViewer);
            this.Controls.Add(this.hScroll);
            this.Controls.Add(this.zoomBar);
            this.Controls.Add(this.statusStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "WaveViewer";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "WaveViewer";
            this.Load += new System.EventHandler(this.WaveViewer_Load);
            this.Click += new System.EventHandler(this.WaveViewer_Click);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.WaveViewer_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.WaveViewer_KeyDown);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.WaveViewer_MouseDown);
            this.MouseEnter += new System.EventHandler(this.WaveViewer_Enter);
            this.MouseLeave += new System.EventHandler(this.WaveViewer_Leave);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.WaveViewer_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.WaveViewer_MouseUp);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.zoomBar)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel statusBit;
        private System.Windows.Forms.ToolStripStatusLabel statusSampleRate;
        private System.Windows.Forms.TrackBar zoomBar;
        private System.Windows.Forms.HScrollBar hScroll;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
        private System.Windows.Forms.Timer scrollTime;
        private System.Windows.Forms.ToolStripStatusLabel sampleStatus;
        private System.Windows.Forms.Button closeViewer;
        private System.Windows.Forms.ToolStripStatusLabel statusZoom;
    }
}