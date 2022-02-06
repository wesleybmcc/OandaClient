using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TechAnalysis.Model;

namespace OandaClient
{
    public class OandaDataService
    {
        const string DAILY_GRANULARITY = "D";
        const string MINUTE_GRANULARITY = "M";
        private OandaClient _oandaClient = new OandaClient();

        public BidAskResponse GetBidAsk(string instrument)
        {
            var oandaRequest = new OandaRequest { Instrument = instrument, IsBidAsk = true, Granularity = MINUTE_GRANULARITY, GranularityValue = 1, Count = 1 };
            var response = _oandaClient.GetBidAsk(oandaRequest);
            var bidAskResponse = CreateBidAskResponse(response, oandaRequest);

            return bidAskResponse;
        }

        public HistoricalDataResponse GetHistorialData(string instrument, string granularity = DAILY_GRANULARITY, int granularityValue = 15)
        {
            var oandaRequest = new OandaRequest { Instrument = instrument, IsBidAsk = false, Granularity = granularity, GranularityValue = granularityValue };
            var response = _oandaClient.LoadHistoricalData(oandaRequest);
            return CreateHistorialDataResponse(response, oandaRequest);
        }

        private HistoricalDataResponse CreateHistorialDataResponse(OandaCandleResult[]? oandaCandleResults, OandaRequest oandaRequest)
        {
            var historicalDataResposne = new HistoricalDataResponse { Instrument = oandaRequest.Instrument };

            if (oandaCandleResults != null && oandaCandleResults.Length > 0)
            {
                var first = oandaCandleResults.First();
                var last = oandaCandleResults.Last();

                historicalDataResposne.StartDateTime = DateTime.Parse(first.Time);
                historicalDataResposne.EndDateTime = DateTime.Parse(last.Time);

                oandaCandleResults.ToList().ForEach(r => {

                    if (r.mid != null)
                    {
                        var ohlc = new OHLC
                        {
                            StartDateTime = DateTime.Parse(r.Time),
                            EndDateTime = DateTime.Parse(r.Time).AddMinutes(14),
                            Open = r.mid.GetOpen(),
                            High = r.mid.GetHigh(),
                            Low = r.mid.GetLow(),
                            Close = r.mid.GetClose(),
                            //Symbol = instrument
                        };

                        historicalDataResposne.OHLC.Add(ohlc);
                    }
                });
            }
            return historicalDataResposne;
        }

        private BidAskResponse CreateBidAskResponse(OandaBidAskResult[]? oandaBidAskResult, OandaRequest oandaRequest)
        {
            var bidAskResponse = new BidAskResponse { Instrument = oandaRequest.Instrument };

            if (oandaBidAskResult != null && oandaBidAskResult.Length > 0)
            {
                var first = oandaBidAskResult.First();
                bidAskResponse.StartDateTime = DateTime.Parse(first.Time);
                bidAskResponse.EndDateTime = bidAskResponse.StartDateTime;
                bidAskResponse.Volume = first.Volume;

                if (first.ask != null)
                {
                    bidAskResponse.Ask = new OHLC
                    {
                        StartDateTime = bidAskResponse.StartDateTime,
                        EndDateTime = bidAskResponse.EndDateTime,
                        Open = first.ask.GetOpen(),
                        High = first.ask.GetHigh(),
                        Low = first.ask.GetLow(),
                        Close = first.ask.GetClose(),
                        //Symbol = instrument
                    };
                }

                if (first.bid != null)
                {
                    bidAskResponse.Bid = new OHLC
                    {
                        StartDateTime = bidAskResponse.StartDateTime,
                        EndDateTime = bidAskResponse.EndDateTime,
                        Open = first.bid.GetOpen(),
                        High = first.bid.GetHigh(),
                        Low = first.bid.GetLow(),
                        Close = first.bid.GetClose(),
                        //Symbol = instrument
                    };
                }
            }

            return bidAskResponse;
        }
    }
}