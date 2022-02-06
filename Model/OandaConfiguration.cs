using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OandaClient
{
    internal class OandaConfiguration
    {
        private readonly string _baseUrl = "https://api-fxpractice.oanda.com";
        private readonly string _token = "Bearer 1aa2f86b2ce2b328b9a2c415e68c7ae3-cc0091d0eb4a28b72073d3e58fe99b3c";
        private readonly string _urlMethod = "GET";
        private readonly string _contentType = "application/json";
        private readonly WebHeaderCollection _headers = new WebHeaderCollection();

        public OandaConfiguration()
        {
            _headers.Add("Authorization", this._token);
        }

        public string URLMethod { get { return this._urlMethod; } }
        public string ContentType { get { return this._contentType; } }
        public WebHeaderCollection Headers { get { return _headers; } }

        public string CreateURL(OandaRequest oandaRequest)
        {
            var granularity = string.Format("{0}{1}", oandaRequest.Granularity, oandaRequest.Granularity == "D" ? "" : oandaRequest.GranularityValue);
            var count = string.Format("&count={0}", oandaRequest.Count);
            var bidAsk = string.Format("{0}", oandaRequest.IsBidAsk ? "&price=BA" : "");
            var instrument = oandaRequest.Instrument.Contains('/') ? oandaRequest.Instrument.Replace("/", "_") : oandaRequest.Instrument;
            return string.Format("{0}/v3/instruments/{1}/candles?granularity={2}{3}{4}", 
                _baseUrl, instrument, granularity, count, bidAsk);
        }
        public string CreateURL(string instrument, string periodName, int periodValue, bool latestOnly = false)
        {
            var granularity = string.Format("{0}{1}", periodName, periodName == "D" ? "": periodValue);
            var count = string.Format("{0}", latestOnly ? "&count=1" : string.Empty);
            return string.Format("{0}/v3/instruments/{1}/candles?granularity={2}{3}", _baseUrl, instrument, granularity, count);
        }
    }
}
