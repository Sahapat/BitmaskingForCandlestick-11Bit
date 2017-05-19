using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public static decimal EncyptData(CandleStickData[] rawData)
        {
            return 0;
        }
        private byte checkDirection(double open,double close)
        {
            if (open > close) return (byte)TypeTrend.UP;
            else return (byte)TypeTrend.DOWN;
        }
        private byte checkHH(double opendata1,double closedata1,double opendata2,double closedata2)
        {
            double maxData1 = Math.Max(opendata1, closedata1);
            double maxData2 = Math.Max(opendata2, closedata2);

            if (maxData1 > maxData2) return (byte)TypeTrend.UP;
            else return (byte)TypeTrend.DOWN;
        }
        private byte checkLL(double opendata1,double closedata1,double opendata2,double closedata2)
        {
            double minData1 = Math.Min(opendata1, closedata1);
            double minData2 = Math.Min(opendata2, closedata2);
            if (minData1 > minData2) return (byte)TypeTrend.UP;
            else return (byte)TypeTrend.DOWN;
        }
        private byte stutusUpperShadow(double US,double US_SD,double avgUpShadow,double[] UScentroid)
        {
            if (US == 0) return (byte)TypeCandle.NONE;
            else
            {
                double STD = (US - avgUpShadow) / US_SD;
                if (STD < (UScentroid[0] + UScentroid[1]) / 2)
                {
                    return (byte)TypeCandle.LOW;
                }
                else if (STD > (UScentroid[1] + UScentroid[2]) / 2)
                {
                    return (byte)TypeCandle.HIGH;
                }
                else return (byte)TypeCandle.MID;
            }
        }
        private byte statusLowerShadow(double LS,double LS_SD,double avgLowShadow,double[] LScentroid)
        {
            if (LS == 0) return (byte)TypeCandle.NONE;
            else
            {
                double STD = (LS - avgLowShadow) / LS_SD;

                if (STD < (LScentroid[0] + LScentroid[1]) / 2)
                {
                    return (byte)TypeCandle.LOW;
                }
                else if (STD > (LScentroid[1] + LScentroid[2]) / 2)
                {
                    return (byte)TypeCandle.HIGH;
                }
                else return (byte)TypeCandle.MID;
            }
        }
        private byte statusBody(double Body,double Body_SD,double avgBody,double[] Bodycentroid)
        {
            if (Body == 0) return (byte)TypeCandle.NONE;
            else
            {
                double STD = (Body - avgBody) / Body_SD;
                if (STD < (Bodycentroid[0] + Bodycentroid[1]) / 2)
                {
                    return (byte)TypeCandle.LOW;
                }
                else if (STD > (Bodycentroid[1] + Bodycentroid[2]) / 2)
                {
                    return (byte)TypeCandle.HIGH;
                }
                else return (byte)TypeCandle.MID;
            }
        }
        private byte checkGAP(CandleStickData currentData,CandleStickData nextData)
        {
            if (Math.Max(currentData.Open, currentData.Close) < nextData.Low || Math.Min(currentData.Open, currentData.Close) > nextData.High)
            {
                return (byte)CandleGAP.GAP;
            }
            else return (byte)CandleGAP.NotGAP;
        }
        private byte checkVolume(double current,params double[] last)
        {
            double avgLast = 0;

            for(int i =0;i<last.Length;i++)
            {
                avgLast += last[i];
            }
            avgLast /= last.Length;

            if (current > avgLast)
            {
                return (byte)CheckVolume.Peak;
            }
            else return (byte)CheckVolume.NotPeak;
        }
    }
}
