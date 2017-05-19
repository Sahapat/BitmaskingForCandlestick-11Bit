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
                double Kmean = (US - avgUpShadow) / US_SD;
                double distance1 = Math.Abs(UScentroid[0] - Kmean);
                double distance2 = Math.Abs(UScentroid[1] - Kmean);
                double distance3 = Math.Abs(UScentroid[2] - Kmean);

                if (distance1 < distance2)
                {
                    return (byte)TypeCandle.LOW;
                }
                else if (distance3 < distance2)
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
                double Kmean = (LS - avgLowShadow) / LS_SD;
                double distance1 = Math.Abs(LScentroid[0] - Kmean);
                double distance2 = Math.Abs(LScentroid[1] - Kmean);
                double distance3 = Math.Abs(LScentroid[2] - Kmean);

                if (distance1 < distance2)
                {
                    return (byte)TypeCandle.LOW;
                }
                else if (distance3 < distance2)
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
                double Kmean = (Body - avgBody) / Body_SD;
                double distance1 = Math.Abs(Bodycentroid[0] - Kmean);
                double distance2 = Math.Abs(Bodycentroid[1] - Kmean);
                double distance3 = Math.Abs(Bodycentroid[2] - Kmean);

                if (distance1 < distance2)
                {
                    return (byte)TypeCandle.LOW;
                }
                else if (distance3 < distance2)
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
        private byte checkVolume()
        {
            //setting status Volume here
            return 0;
        }
    }
}
