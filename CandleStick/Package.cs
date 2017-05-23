using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace CandleStick
{
    enum TypeCandle
    {
        NONE,
        LOW,
        MID,
        HIGH
    };
    enum TypeTrend
    {
        DOWN,
        UP
    };
    enum CandleGAP
    {
        NotGAP,
        GAP
    };
    enum CheckVolume
    {
        NotPeak,
        Peak
    };

    class Package
    {
        public BigInteger Packing(BigInteger[] data)
        {
            BigInteger output = 0;
            byte shift = 0;
            for(int i =0;i<data.Length;i++)
            {
                output = output | data[i] << shift;
                shift += 11;
            }
            return output;
        }
        public BigInteger[] getMaskData(CandleStickData[] rawData, int DayOfAvgVolume)
        {
            double avgBody = 0;
            for(int i = 0;i<rawData.Length;i++)
            {
                double body = Math.Abs(rawData[i].Open - rawData[i].Close);
                avgBody += body;
            }
            avgBody /= rawData.Length;

            double avgUpShadow = 0;
            for(int i =0;i<rawData.Length;i++)
            {
                double UpShadow = rawData[i].High - Math.Max(rawData[i].Open, rawData[i].Close);
                avgUpShadow += UpShadow;
            }
            avgUpShadow /= rawData.Length;

            double avgLowShadow = 0;
            for(int i =0;i<rawData.Length;i++)
            {
                double LowShadow = rawData[i].Low - Math.Min(rawData[i].Open, rawData[i].Close);
                avgLowShadow += LowShadow;
            }
            avgLowShadow /= rawData.Length;

            double SD_Body = 0;
            for(int i =0;i<rawData.Length;i++)
            {
                SD_Body += (Math.Abs(rawData[i].Open - rawData[i].Close) - avgBody);
            }
            SD_Body /= rawData.Length;

            double SD_Upshadow = 0;
            for(int i = 0;i<rawData.Length;i++)
            {
                SD_Upshadow += (rawData[i].High - Math.Max(rawData[i].Open, rawData[i].Close)-avgUpShadow);
            }
            SD_Upshadow /= rawData.Length;

            double SD_LowShadow = 0;
            for(int i = 0;i<rawData.Length;i++)
            {
                SD_LowShadow += (rawData[i].Low - Math.Min(rawData[i].Open, rawData[i].Close) - avgLowShadow);
            }

            CandleStatus[] maskData = new CandleStatus[rawData.Length];
            BigInteger[] output = new BigInteger[rawData.Length];
            double[] Uscentroid = { -0.552689665062178, 0.615773990557240, 2.955830732137820 };
            double[] LScentroid = { -0.479483091630587, 0.571950622887706, 3.379221758271600 };
            double[] Bodycentroid = { -0.568489143858862, 0.492655804140342, 2.732240511936160 };
            

            for(int i =0;i<maskData.Length;i++)
            {
                maskData[i].Direction = checkDirection(rawData[i].Open, rawData[i].Close);
                maskData[i].Body = statusBody(Math.Abs(rawData[i].Open - rawData[i].Close), SD_Body, avgBody, Bodycentroid);
                maskData[i].UpperShadow = statusUpperShadow(Math.Max(rawData[i].Open, rawData[i].Close), SD_Upshadow, avgUpShadow, Uscentroid);
                maskData[i].LowerShadow = statusLowerShadow(Math.Min(rawData[i].Open, rawData[i].Close), SD_LowShadow, avgLowShadow, LScentroid);
                
                if(i == maskData.Length -2)
                {
                    maskData[i].GAP = checkGAP(rawData[i], rawData[i + 1]);
                    maskData[i].HigherHigh = checkHH(rawData[i].Open, rawData[i].Close, rawData[i + 1].Open, rawData[i + 1].Close);
                    maskData[i].LowerLow = checkLL(rawData[i].Open, rawData[i].Close, rawData[i + 1].Open, rawData[i + 1].Close);
                }
                else
                {
                    maskData[i].GAP = 0;
                    maskData[i].HigherHigh = 0;
                    maskData[i].LowerLow = 0;
                }
                if (i % DayOfAvgVolume == 0 && i != 0)
                {
                    maskData[i].Volume = checkVolume(rawData[i].Volume, rawData[i - 1].Volume, rawData[i - 2].Volume, rawData[i - 3].Volume, rawData[i - 4].Volume);
                }
                else maskData[i].Volume = 0;
            }

            for(int i = 0;i<maskData.Length;i++)
            {
                output[i] = Mask(maskData[i]);
            }
            System.Windows.Forms.MessageBox.Show("maskdata" + maskData[11].Volume);
            return output;
        }
        private BigInteger Mask(CandleStatus data)
        {
            BigInteger temp = 0;
            temp = temp | data.Volume;
            temp = temp | (data.LowerShadow<<1);
            temp = temp | (data.Body << 3);
            temp = temp | (data.UpperShadow << 5);
            temp = temp | (data.GAP << 7);
            temp = temp | (data.Direction << 8);
            temp = temp | (data.LowerLow << 9);
            temp = temp | (data.HigherHigh << 10);
            return temp;
        }
        private short checkDirection(double open,double close)
        {
            if (open < close) return (short)TypeTrend.UP;
            else return (short)TypeTrend.DOWN;
        }//check
        private short checkHH(double opendata1,double closedata1,double opendata2,double closedata2)
        {
            double maxData1 = Math.Max(opendata1, closedata1);
            double maxData2 = Math.Max(opendata2, closedata2);

            if (maxData1 > maxData2) return (short)TypeTrend.UP;
            else return (short)TypeTrend.DOWN;
        }//Logic error
        private short checkLL(double opendata1,double closedata1,double opendata2,double closedata2)
        {
            double minData1 = Math.Min(opendata1, closedata1);
            double minData2 = Math.Min(opendata2, closedata2);
            if (minData1 > minData2) return (short)TypeTrend.UP;
            else return (short)TypeTrend.DOWN;
        }//Logic error
        private short statusUpperShadow(double US,double US_SD,double avgUpShadow,double[] UScentroid)
        {
            if (US == 0) return (short)TypeCandle.NONE;
            else
            {
                double STD = (US - avgUpShadow) / US_SD;
                if (STD < (UScentroid[0] + UScentroid[1]) / 2)
                {
                    return (short)TypeCandle.LOW;
                }
                else if (STD > (UScentroid[1] + UScentroid[2]) / 2)
                {
                    return (short)TypeCandle.HIGH;
                }
                else return (short)TypeCandle.MID;
            }
        }//Logic error
        private short statusLowerShadow(double LS,double LS_SD,double avgLowShadow,double[] LScentroid)
        {
            if (LS == 0) return (short)TypeCandle.NONE;
            else
            {
                double STD = (LS - avgLowShadow) / LS_SD;

                if (STD < (LScentroid[0] + LScentroid[1]) / 2)
                {
                    return (short)TypeCandle.LOW;
                }
                else if (STD > (LScentroid[1] + LScentroid[2]) / 2)
                {
                    return (short)TypeCandle.HIGH;
                }
                else return (short)TypeCandle.MID;
            }
        }//Logic error
        private short statusBody(double Body,double Body_SD,double avgBody,double[] Bodycentroid)
        {
            if (Body == 0) return (short)TypeCandle.NONE;
            else
            {
                double STD = (Body - avgBody) / Body_SD;
                if (STD < (Bodycentroid[0] + Bodycentroid[1]) / 2)
                {
                    return (short)TypeCandle.LOW;
                }
                else if (STD > (Bodycentroid[1] + Bodycentroid[2]) / 2)
                {
                    return (short)TypeCandle.HIGH;
                }
                else return (short)TypeCandle.MID;
            }
        }//check
        private short checkGAP(CandleStickData currentData,CandleStickData nextData)
        {
            if (Math.Max(currentData.Open, currentData.Close) < nextData.Low || Math.Min(currentData.Open, currentData.Close) > nextData.High)
            {
                return (short)CandleGAP.GAP;
            }
            else return (short)CandleGAP.NotGAP;
        }//uncheck
        private short checkVolume(double current,params double[] last)
        {
            double avgLast = 0;

            for(int i =0;i<last.Length;i++)
            {
                avgLast += last[i];
            }
            avgLast /= last.Length;

            if (current > avgLast)
            {
                return (short)CheckVolume.Peak;
            }
            else return (short)CheckVolume.NotPeak;
        }//check
    }
}
