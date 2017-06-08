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
        private CandleStickData[] RawData = new CandleStickData[0];
        private CandleNormalData[] NormalData = new CandleNormalData[0];
        private BigInteger[] BinaryCandleProperty = new BigInteger[0];
        private BigInteger[] BinaryPackage = new BigInteger[0];
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
                Array.Resize<CandleStickData>(ref RawData, csvCandleData.RowLenght-1);
                Array.Resize<CandleNormalData>(ref NormalData, csvCandleData.RowLenght - 1);
                SetRawData();
                SetChart();
                InitComboBox();
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
                currentDisplay = ((currentDisplay + nextDisplay) + amountDisplay > RawData.Length) ? currentDisplay = RawData.Length - amountDisplay : currentDisplay += nextDisplay;
                candleChart.Series[0].Points.Clear();
                SetChart();
            }
        }
        private void Save_Click(object sender, EventArgs e)
        {
            string select = VolumeAvg.Text;
            if (select == string.Empty) return;
            string filter = "CSV file (*.csv)|*.csv";
            saveCsv.Filter = filter;
            DialogResult result = saveCsv.ShowDialog();
            if(result == DialogResult.OK)
            {
                string path = saveCsv.FileName;
                string outData = GetOutputData();
                csvCandleData.WriteData(path, outData);
            }
        }
        private void VolumeAvg_SelectedIndexChanged(object sender, EventArgs e)
        {
            PackingData();
        }
        private string GetOutputData()
        {
            StringBuilder data = new StringBuilder();
            data.Append("RawCandleProperty,BinaryCandleProperty,BinaryPackage");
            data.AppendLine();
            for(int i = 0;i<BinaryCandleProperty.Length;i++)
            {
                string RawCandleProperty = BinaryCandleProperty[i].ToString();
                string BinaryProperty = ToBinaryString(BinaryCandleProperty[i]);
                while (BinaryProperty.Length != 11)
                {
                    if (BinaryProperty.Length > 11)
                    {
                        BinaryProperty.Remove(0);
                    }
                    else
                    {
                        string temp = "0";
                        temp += BinaryProperty;
                        BinaryProperty = temp;
                    }
                }
                if (i < BinaryPackage.Length)
                {
                    string BinaryPack = ToBinaryString(BinaryPackage[i]);
                    while (BinaryPack.Length != 121)
                    {
                        if (BinaryPack.Length > 121)
                        {
                            BinaryPack.Remove(0);
                        }
                        else
                        {
                            string temp = "0";
                            temp += BinaryPack;
                            BinaryPack = temp;
                        }
                    }
                    data.AppendFormat("\"{0}\",\"{1}\",\"{2}\"", RawCandleProperty, BinaryProperty, BinaryPack);
                    data.AppendLine();
                }
                else
                {
                    data.AppendFormat("\"{0}\",\"{1}\",\"{2}\"", RawCandleProperty, BinaryProperty,string.Empty);
                    data.AppendLine();
                }
            }
            return data.ToString();
        }
        private void InitComboBox()
        {
            Save.Visible = true;
            VolumeAvg.Visible = true;
            VolumeAvg.Items.AddRange(items);
            VolumeAvg.SelectedIndex = 0;
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
                candleChart.Series[0].Points.AddXY(RawData[i].DateTime, RawData[i].High, RawData[i].Low, RawData[i].Open, RawData[i].Close);
            }
        }
        private void SetRawData()
        {
            for (int i = 1; i <= RawData.Length; i++)
            {
                RawData[i - 1].DateTime = csvCandleData.GetColumnDataAsString(1, i);
                RawData[i - 1].Open = (float)csvCandleData.GetColumnData(2, i);
                RawData[i - 1].High = (float)csvCandleData.GetColumnData(3, i);
                RawData[i - 1].Low = (float)csvCandleData.GetColumnData(4, i);
                RawData[i - 1].Close = (float)csvCandleData.GetColumnData(5, i);
                RawData[i - 1].Volume = csvCandleData.GetColumnData(6, i);
            }
        }
        private void SetNormalRawData()
        {

        }
        private void PackingData()
        {
            int DayOfAvgVolume = 0;
            switch(VolumeAvg.Text)
            {
                case "4 VolumesAvg":
                    DayOfAvgVolume = 4;
                    break;
                case "5 VolumesAvg":
                    DayOfAvgVolume = 5;
                    break;
                case "6 VolumesAvg":
                    DayOfAvgVolume = 6;
                    break;
                case "7 VolumesAvg":
                    DayOfAvgVolume = 7;
                    break;
                case "8 VolumesAvg":
                    DayOfAvgVolume = 8;
                    break;
                case "9 VolumesAvg":
                    DayOfAvgVolume = 9;
                    break;
                case "10 VolumesAvg":
                    DayOfAvgVolume = 10;
                    break;
                case "11 VolumesAvg":
                    DayOfAvgVolume = 11;
                    break;
                default:
                    DayOfAvgVolume = 4;
                    break;
            }
            Package package = new Package();
            var CandleProperty = package.getMaskData(RawData,DayOfAvgVolume);

            List<BigInteger> temp = new List<BigInteger>();
            List<BigInteger[]> InputData = new List<BigInteger[]>();
            int size = CandleProperty.Length - 1;
            int count = 0;
            while(true)
            {
                temp.Add(CandleProperty[count]);
                if(temp.Count == 11)
                {
                    InputData.Add(temp.ToArray());
                    temp.Clear();
                }
                else if(count == size)
                {
                    InputData.Add(temp.ToArray());
                    temp.Clear();
                    break;
                }
                count++;
            }
            Array.Resize<BigInteger>(ref BinaryPackage, InputData.Count);
            Array.Resize<BigInteger>(ref BinaryCandleProperty, CandleProperty.Length);

            BinaryCandleProperty = CandleProperty;
            
            for(int i =0;i<InputData.Count;i++)
            {
                BinaryPackage[i] = package.Packing(InputData[i]);
            }
;        }
        private string ToBinaryString(BigInteger data)
        {
            var bytes = data.ToByteArray();
            var idx = bytes.Length - 1;
            var base2 = new StringBuilder(bytes.Length * 8);
            var binary = Convert.ToString(bytes[idx], 2);
            base2.Append(binary);
            for (idx--; idx >= 0; idx--)
            {
                base2.Append(Convert.ToString(bytes[idx], 2).PadLeft(8, '0'));
            }
            return base2.ToString();
        }
        private BigInteger ToDecimal(string RawBinary)
        {
            BigInteger output = 0;
            char[] binary = RawBinary.ToArray();
            int size = binary.Length;

            for (int i = 1; i <= size; i++)
            {
                Console.WriteLine("Loop Count : " + i);
                if (binary[size - i] == '0')
                {
                    continue;
                }
                else
                {
                    output += (BigInteger)Math.Pow(2, i - 1);
                    Console.WriteLine("Loop " + i + " " + output);
                }
            }

            return output;
        }
    }
}
