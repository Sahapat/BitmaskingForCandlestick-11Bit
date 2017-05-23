namespace CandleStick
{
    partial class Form1
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea7 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series7 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea8 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series8 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.candleChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.getCandleData = new System.Windows.Forms.OpenFileDialog();
            this.GetData = new System.Windows.Forms.Button();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.Back = new System.Windows.Forms.Button();
            this.Next = new System.Windows.Forms.Button();
            this.VolumeAvg = new System.Windows.Forms.ComboBox();
            this.Save = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.candleChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // candleChart
            // 
            chartArea7.Name = "ChartArea1";
            this.candleChart.ChartAreas.Add(chartArea7);
            this.candleChart.Location = new System.Drawing.Point(42, 97);
            this.candleChart.Name = "candleChart";
            this.candleChart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.None;
            series7.BackSecondaryColor = System.Drawing.SystemColors.Control;
            series7.BorderColor = System.Drawing.SystemColors.Control;
            series7.ChartArea = "ChartArea1";
            series7.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Candlestick;
            series7.Color = System.Drawing.Color.White;
            series7.CustomProperties = "PriceDownColor=Red, PriceUpColor=Lime";
            series7.Legend = "Legend1";
            series7.Name = "Series1";
            series7.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.BrightPastel;
            series7.YValuesPerPoint = 4;
            this.candleChart.Series.Add(series7);
            this.candleChart.Size = new System.Drawing.Size(450, 422);
            this.candleChart.TabIndex = 0;
            this.candleChart.Text = "candleChart";
            // 
            // GetData
            // 
            this.GetData.Location = new System.Drawing.Point(12, 21);
            this.GetData.Name = "GetData";
            this.GetData.Size = new System.Drawing.Size(62, 41);
            this.GetData.TabIndex = 3;
            this.GetData.Text = "GetData";
            this.GetData.UseVisualStyleBackColor = true;
            this.GetData.Click += new System.EventHandler(this.GetData_Click);
            // 
            // chart1
            // 
            chartArea8.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea8);
            this.chart1.Location = new System.Drawing.Point(535, 97);
            this.chart1.Name = "chart1";
            this.chart1.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.None;
            series8.BackSecondaryColor = System.Drawing.SystemColors.Control;
            series8.BorderColor = System.Drawing.SystemColors.Control;
            series8.ChartArea = "ChartArea1";
            series8.Color = System.Drawing.Color.White;
            series8.Legend = "Legend1";
            series8.Name = "Series1";
            series8.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.BrightPastel;
            this.chart1.Series.Add(series8);
            this.chart1.Size = new System.Drawing.Size(450, 422);
            this.chart1.TabIndex = 4;
            this.chart1.Text = "chart1";
            // 
            // Back
            // 
            this.Back.Location = new System.Drawing.Point(434, 525);
            this.Back.Name = "Back";
            this.Back.Size = new System.Drawing.Size(76, 34);
            this.Back.TabIndex = 5;
            this.Back.Text = "Back";
            this.Back.UseVisualStyleBackColor = true;
            this.Back.Click += new System.EventHandler(this.Back_Click);
            // 
            // Next
            // 
            this.Next.Location = new System.Drawing.Point(516, 525);
            this.Next.Name = "Next";
            this.Next.Size = new System.Drawing.Size(76, 34);
            this.Next.TabIndex = 6;
            this.Next.Text = "Next";
            this.Next.UseVisualStyleBackColor = true;
            this.Next.Click += new System.EventHandler(this.Next_Click);
            // 
            // VolumeAvg
            // 
            this.VolumeAvg.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VolumeAvg.FormattingEnabled = true;
            this.VolumeAvg.Location = new System.Drawing.Point(728, 33);
            this.VolumeAvg.Name = "VolumeAvg";
            this.VolumeAvg.Size = new System.Drawing.Size(148, 28);
            this.VolumeAvg.TabIndex = 7;
            // 
            // Save
            // 
            this.Save.Location = new System.Drawing.Point(882, 30);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(76, 34);
            this.Save.TabIndex = 8;
            this.Save.Text = "Save";
            this.Save.UseVisualStyleBackColor = true;
            this.Save.Click += new System.EventHandler(this.Save_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1032, 571);
            this.Controls.Add(this.Save);
            this.Controls.Add(this.VolumeAvg);
            this.Controls.Add(this.Next);
            this.Controls.Add(this.Back);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.GetData);
            this.Controls.Add(this.candleChart);
            this.Name = "Form1";
            this.Text = "CandleStick";
            ((System.ComponentModel.ISupportInitialize)(this.candleChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart candleChart;
        private System.Windows.Forms.OpenFileDialog getCandleData;
        private System.Windows.Forms.Button GetData;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Button Back;
        private System.Windows.Forms.Button Next;
        private System.Windows.Forms.ComboBox VolumeAvg;
        private System.Windows.Forms.Button Save;
    }
}

