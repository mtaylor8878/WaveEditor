namespace COMP3931_Assignment
{
    partial class WaveGraph
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.waveChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.addSignal = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.waveChart)).BeginInit();
            this.SuspendLayout();
            // 
            // waveChart
            // 
            this.waveChart.BorderlineWidth = 0;
            chartArea1.AxisX.LabelStyle.Enabled = false;
            chartArea1.AxisX.MajorGrid.Enabled = false;
            chartArea1.AxisX.MajorTickMark.Enabled = false;
            chartArea1.AxisY.MajorGrid.Enabled = false;
            chartArea1.Name = "TimeDomain";
            chartArea2.AxisX.MajorGrid.Enabled = false;
            chartArea2.AxisX.MajorTickMark.Enabled = false;
            chartArea2.AxisX.Minimum = 0D;
            chartArea2.AxisX.MinorTickMark.Enabled = true;
            chartArea2.Name = "FrequencyDomain";
            this.waveChart.ChartAreas.Add(chartArea1);
            this.waveChart.ChartAreas.Add(chartArea2);
            this.waveChart.Location = new System.Drawing.Point(12, 12);
            this.waveChart.Name = "waveChart";
            series1.ChartArea = "TimeDomain";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series1.IsVisibleInLegend = false;
            series1.Name = "Samples";
            series2.ChartArea = "FrequencyDomain";
            series2.IsVisibleInLegend = false;
            series2.Name = "Amps";
            this.waveChart.Series.Add(series1);
            this.waveChart.Series.Add(series2);
            this.waveChart.Size = new System.Drawing.Size(665, 341);
            this.waveChart.TabIndex = 0;
            this.waveChart.Text = "chart1";
            // 
            // addSignal
            // 
            this.addSignal.Location = new System.Drawing.Point(12, 359);
            this.addSignal.Name = "addSignal";
            this.addSignal.Size = new System.Drawing.Size(75, 23);
            this.addSignal.TabIndex = 1;
            this.addSignal.Text = "Add Signal";
            this.addSignal.UseVisualStyleBackColor = true;
            this.addSignal.Click += new System.EventHandler(this.addSignal_Click);
            // 
            // WaveGraph
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(690, 389);
            this.Controls.Add(this.addSignal);
            this.Controls.Add(this.waveChart);
            this.Name = "WaveGraph";
            this.Text = "Wave";
            ((System.ComponentModel.ISupportInitialize)(this.waveChart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart waveChart;
        private System.Windows.Forms.Button addSignal;
    }
}