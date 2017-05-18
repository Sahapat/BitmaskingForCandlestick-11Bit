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
        UP,
        DOWN
    };

    class Package
    {
        public static decimal EncyptData(CandleStickData[] input,float avgBody,float avgUpperS,float avgLowS)
        {
            return 0;
        }
        private byte checkDirection(float open,float close)
        {
            if (open >= close) return (byte)TypeTrend.UP;
            else return (byte)TypeTrend.DOWN;
        }
        private byte checkHH(float opendata1,float closedata1,float opendata2,float closedata2)
        {
            float maxData1 = Math.Max(opendata1, closedata1);
            float maxData2 = Math.Max(opendata2, closedata2);

            if (maxData1 > maxData2) return (byte)TypeTrend.UP;
            else return (byte)TypeTrend.DOWN;
        }
        private byte checkLL(float opendata1,float closedata1,float opendata2,float closedata2)
        {
            float minData1 = Math.Min(opendata1, closedata1);
            float minData2 = Math.Min(opendata2, closedata2);
            if (minData1 > minData2) return (byte)TypeTrend.UP;
            else return (byte)TypeTrend.DOWN;
        }
        private byte stutusUpperShadow()
        {
            //setting status UpperShadow here
            return 0;
        }
        private byte statusLowerShadow()
        {
            //setting status LowerShadow here
            return 0;
        }
        private byte statusBody()
        {
            //setting status Body here
            return 0;
        }
        private byte checkGAP()
        {
            //setting status GAP here
            return 0;
        }
        private byte checkVolume()
        {
            //setting status Volume here
            return 0;
        }
    }
}
