using System.Net;
using Newtonsoft.Json;

namespace OandaClient
{
    internal class OandaClient
    {
        public OandaCandleResult[]? LoadHistoricalData(OandaRequest oandaRequest)
        {
            var candles = CallAPI(oandaRequest);
            return JsonConvert.DeserializeObject<OandaCandleResult[]>(candles);
        }

        public OandaBidAskResult[]? GetBidAsk(OandaRequest oandaRequest)
        {
            var candles = CallAPI(oandaRequest);
            return JsonConvert.DeserializeObject<OandaBidAskResult[]>(candles);
        }

        private string CallAPI(OandaRequest oandaRequest)
        {
            var configuration = new OandaConfiguration();
            var url = configuration.CreateURL(oandaRequest);
#pragma warning disable SYSLIB0014 // Type or member is obsolete
            var request = (HttpWebRequest)WebRequest.Create(url);
#pragma warning restore SYSLIB0014 // Type or member is obsolete

            request.Method = configuration.URLMethod;
            request.ContentType = configuration.ContentType;
            request.Headers = configuration.Headers;

            HttpWebResponse webresponse = (HttpWebResponse)request.GetResponse();

            var sw = new StreamReader(webresponse.GetResponseStream(), System.Text.Encoding.ASCII);
            var json = sw.ReadToEnd();
            sw.Close();

            return ParseJSON(json);

        }

        private string ParseJSON(string json)
        {
            var candlesIndex = json.IndexOf("[", json.IndexOf("candles"));
            var candles = json.Substring(candlesIndex);
            return candles.Remove(candles.Length - 1, 1);
        }

    }
}
