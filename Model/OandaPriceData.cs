using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechAnalysis.Model;

namespace OandaClient
{
    public class OandaPriceDatac
    {
        public string Instrument { get; set; }
        public string PeriodName { get; set; }
        public int PeriodValue { get; set; }
        public List<OHLC> OHLC { get; set; } = new List<OHLC>();

        public OandaPriceDatac(string instrument, string periodName, int periodValue)
        {
            Instrument = instrument;
            PeriodName = periodName;
            PeriodValue = periodValue;
        }

        public void AddOHLC(IList<OHLC> ohlc)
        {
            OHLC.AddRange(ohlc);
        }

        public OHLC AddOHLC(string symbol, DateTime startDateTime, DateTime endDateTime, double open, double high, double low, double close)
        {
            var ohlc = new OHLC(symbol, startDateTime, endDateTime, open, high, low, close);
            OHLC.Add(ohlc);
            return ohlc;
        }

        public void ConvertBidAsk(IList<BidAsk> bidAsks)
        {
            int firstHour = bidAsks[0].DateTime.Hour;

            bidAsks.ToList().ForEach(bidAsk => {

            });
        }

        public static OandaPriceDatac Create(string instrument, string periodName, int periodValue,
            OandaBidAskResult[]? oandaBidAskResults)
        {
            var oandaPriceData = new OandaPriceDatac(instrument, periodName, periodValue);
            if (oandaBidAskResults != null)
            {
                foreach (var oandaBidAskResult in oandaBidAskResults)
                {
                    if (oandaBidAskResult.bid != null)
                    {
                        var startDateTime = DateTime.Parse(oandaBidAskResult.Time);
                        var endDateTime = startDateTime.AddMinutes(14);
                        oandaPriceData.AddOHLC(
                            instrument,
                            startDateTime, endDateTime,
                            oandaBidAskResult.bid.GetOpen(),
                            oandaBidAskResult.bid.GetHigh(),
                            oandaBidAskResult.bid.GetLow(),
                            oandaBidAskResult.bid.GetClose());
                    }

                    if (oandaBidAskResult.ask != null)
                    {
                        var startDateTime = DateTime.Parse(oandaBidAskResult.Time);
                        var endDateTime = startDateTime.AddMinutes(14);
                        oandaPriceData.AddOHLC(
                            instrument,
                            startDateTime, endDateTime,
                            oandaBidAskResult.ask.GetOpen(),
                            oandaBidAskResult.ask.GetHigh(),
                            oandaBidAskResult.ask.GetLow(),
                            oandaBidAskResult.ask.GetClose());
                    }
                }
            }

            return oandaPriceData;
        }
       
        public static OandaPriceDatac Create(string instrument, string periodName, int periodValue,             
            OandaCandleResult[]? oandaCandleResults)
        {
            var oandaPriceData = new OandaPriceDatac(instrument, periodName, periodValue);
            if(oandaCandleResults != null)
            {
                foreach(var oandaCandleResult in oandaCandleResults) 
                {
                    if(oandaCandleResult.mid != null)
                    {
                        var startDateTime = DateTime.Parse(oandaCandleResult.Time);
                        var endDateTime = startDateTime.AddMinutes(14);
                        oandaPriceData.AddOHLC(
                            instrument,
                            startDateTime, endDateTime,
                            oandaCandleResult.mid.GetOpen(),
                            oandaCandleResult.mid.GetHigh(),
                            oandaCandleResult.mid.GetLow(),
                            oandaCandleResult.mid.GetClose());
                    }
                }
            }

            return oandaPriceData;
        }
    }
}
