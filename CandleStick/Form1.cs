using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.IO;
using System.Diagnostics;
namespace CandleStick
{
    public partial class Form1 : Form
    {
        private CsvManager csvCandleData = new CsvManager();
        private CandleStickData[] ChartData = new CandleStickData[0];
        private CandleNormalData[] NormalData = new CandleNormalData[0];
        private int currentDisplay = 0;
        private const int nextDisplay = 60;

        public Form1()
        {
            InitializeComponent();
            initChart();
        }
        private void GetData_Click(object sender, EventArgs e)
        {
            DialogResult result = getCandleData.ShowDialog();
            if(result == DialogResult.OK)
            {
                csvCandleData.ReadData(getCandleData.FileName);
                Array.Resize<CandleStickData>(ref ChartData, csvCandleData.RowLenght-1);
                Array.Resize<CandleNormalData>(ref NormalData, csvCandleData.RowLenght - 1);
                setChartData();
                setChart();
                PackingData();
            }
        }
        private void initChart()
        {
            candleChart.Series.Clear();
            candleChart.Series.Add("Series").ChartType = SeriesChartType.Candlestick;
            candleChart.Series[0]["PointWidth"] = "0.7";
            candleChart.Series[0]["PriceUpColor"] = "Lime";
            candleChart.Series[0]["PriceDownColor"] = "Red";
            candleChart.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.Gray;
            candleChart.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.Gray;
            candleChart.ChartAreas[0].AxisX.LabelStyle.ForeColor = Color.Gold;
            candleChart.ChartAreas[0].AxisY.LabelStyle.ForeColor = Color.Gold;
            candleChart.ChartAreas[0].BackColor = Color.Black;
            candleChart.BackColor = Color.Gray;
        }
        private void setChart()
        {
            for(int i = currentDisplay;i<currentDisplay+nextDisplay;i++)
            {
                candleChart.Series[0].Points.AddXY(ChartData[i].DateTime, ChartData[i].High, ChartData[i].Low, ChartData[i].Open, ChartData[i].Close);
            }
        }
        private void setChartData()
        {
            for(int i =1;i<ChartData.Length;i++)
            {
                ChartData[i-1].DateTime = csvCandleData.GetColumnDataAsString(1, i);
                ChartData[i-1].Open = (float)csvCandleData.GetColumnData(2, i);
                ChartData[i-1].High = (float)csvCandleData.GetColumnData(3, i);
                ChartData[i-1].Low = (float)csvCandleData.GetColumnData(4, i);
                ChartData[i-1].Close = (float)csvCandleData.GetColumnData(5, i);
                ChartData[i-1].Volume = csvCandleData.GetColumnData(6, i);
            }
        }
        private void setNormalChartData()
        {

        }

        private void Back_Click(object sender, EventArgs e)
        {
            currentDisplay = (currentDisplay <= 0) ? currentDisplay = 0 : currentDisplay -= nextDisplay;
            candleChart.Series[0].Points.Clear();
            setChart();
        }

        private void Next_Click(object sender, EventArgs e)
        {
            currentDisplay += nextDisplay;
            candleChart.Series[0].Points.Clear();
            setChart();
        }
        private void PackingData()
        {
        }
    }
}
