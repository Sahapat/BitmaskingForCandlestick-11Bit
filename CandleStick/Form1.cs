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
using System.Diagnostics;
using System.Numerics;

namespace CandleStick
{
    public partial class Form1 : Form
    {
        private CsvManager csvCandleData = new CsvManager();
        private CandleStickData[] ChartData = new CandleStickData[0];
        private CandleNormalData[] NormalData = new CandleNormalData[0];
        private int currentDisplay = 0;
        private const int nextDisplay = 5;
        private const int amountDisplay = 60;
        private string[] items = {"4 VolumesAvg", "5 VolumesAvg" , "6 VolumesAvg" , "7 VolumesAvg" 
                            , "8 VolumesAvg" ,"9 VolumesAvg" , "10 VolumesAvg" , "11 VolumesAvg" };

        public Form1()
        {
            InitializeComponent();
            InitChart();
            VolumeAvg.Visible = false;
            Save.Visible = false;
        }
        private void GetData_Click(object sender, EventArgs e)
        {
            string filter = "CSV file (*.csv)|*.csv";
            getCandleData.Filter = filter;
            DialogResult result = getCandleData.ShowDialog();
            if(result == DialogResult.OK)
            {
                csvCandleData.ReadData(getCandleData.FileName);
                Array.Resize<CandleStickData>(ref ChartData, csvCandleData.RowLenght-1);
                Array.Resize<CandleNormalData>(ref NormalData, csvCandleData.RowLenght - 1);
                SetChartData();
                SetChart();
                InitComboBox();
                PackingData();
            }
        }
        private void Back_Click(object sender, EventArgs e)
        {
            if (csvCandleData.CsvData != null)
            {
                currentDisplay = (currentDisplay <= 0) ? currentDisplay = 0 : currentDisplay -= nextDisplay;
                candleChart.Series[0].Points.Clear();
                SetChart();
            }
        }
        private void Next_Click(object sender, EventArgs e)
        {
            if (csvCandleData.CsvData != null)
            {
                currentDisplay = ((currentDisplay + nextDisplay) + amountDisplay > ChartData.Length) ? currentDisplay = ChartData.Length - amountDisplay : currentDisplay += nextDisplay;
                candleChart.Series[0].Points.Clear();
                SetChart();
            }
        }
        private void Save_Click(object sender, EventArgs e)
        {
            string select = VolumeAvg.Text;
            string filter = "CSV file (*.csv)|*.csv";
            saveCsv.Filter = filter;
            DialogResult result = saveCsv.ShowDialog();
            if(result == DialogResult.OK)
            {

            }
        }

        private void InitComboBox()
        {
            Save.Visible = true;
            VolumeAvg.Visible = true;
            VolumeAvg.Items.AddRange(items);
            VolumeAvg.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        private void InitChart()
        {
            candleChart.Series.Clear();
            candleChart.Series.Add("Series").ChartType = SeriesChartType.Candlestick;
            candleChart.Series[0]["PointWidth"] = "0.65";
            candleChart.Series[0]["PriceUpColor"] = "Lime";
            candleChart.Series[0]["PriceDownColor"] = "Red";
            candleChart.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.Gray;
            candleChart.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.Gray;
            candleChart.ChartAreas[0].AxisX.LabelStyle.ForeColor = Color.Gold;
            candleChart.ChartAreas[0].AxisY.LabelStyle.ForeColor = Color.Gold;
            candleChart.ChartAreas[0].BackColor = Color.Black;
            candleChart.BackColor = Color.Gray;

            normalChart.Series.Clear();
            normalChart.Series.Add("Series").ChartType = SeriesChartType.Candlestick;
            normalChart.Series[0]["PointWidth"] = "0.65";
            normalChart.Series[0]["PriceUpColor"] = "Lime";
            normalChart.Series[0]["PriceDownColor"] = "Red";
            normalChart.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.Gray;
            normalChart.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.Gray;
            normalChart.ChartAreas[0].AxisX.LabelStyle.ForeColor = Color.Gold;
            normalChart.ChartAreas[0].AxisY.LabelStyle.ForeColor = Color.Gold;
            normalChart.ChartAreas[0].BackColor = Color.Black;
            normalChart.BackColor = Color.Gray;
        }
        private void SetChart()
        {
            for (int i = currentDisplay;i< currentDisplay+amountDisplay; i++)
            {
                candleChart.Series[0].Points.AddXY(ChartData[i].DateTime, ChartData[i].High, ChartData[i].Low, ChartData[i].Open, ChartData[i].Close);
            }
        }
        private void SetChartData()
        {
            for (int i = 1; i <= ChartData.Length; i++)
            {
                ChartData[i - 1].DateTime = csvCandleData.GetColumnDataAsString(1, i);
                ChartData[i - 1].Open = (float)csvCandleData.GetColumnData(2, i);
                ChartData[i - 1].High = (float)csvCandleData.GetColumnData(3, i);
                ChartData[i - 1].Low = (float)csvCandleData.GetColumnData(4, i);
                ChartData[i - 1].Close = (float)csvCandleData.GetColumnData(5, i);
                ChartData[i - 1].Volume = csvCandleData.GetColumnData(6, i);
            }
        }
        private void SetNormalChartData()
        {

        }
        private void PackingData()
        {
            Package package = new Package();
        }
        private static string ToBinaryString(BigInteger data)
        {
            var bytes = data.ToByteArray();
            var idx = bytes.Length - 1;

            var base2 = new StringBuilder(bytes.Length * 8);
            var binary = Convert.ToString(bytes[idx], 2);
            if (binary[0] != '0' && data.Sign == 1)
            {
                base2.Append('0');
            }
            base2.Append(binary);
            for (idx--; idx >= 0; idx--)
            {
                base2.Append(Convert.ToString(bytes[idx], 2).PadLeft(8, '0'));
            }

            return base2.ToString();
        }

 
    }
}
