using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OandaClient
{
    public class OandaCandleResult : OandaSearchResult
    {
        public Bid? mid { get; set; }
    }
    public class OandaBidAskResult : OandaSearchResult
    {
        public Bid? bid { get; set; }
        public Bid? ask { get; set; }
    }

    public class OandaSearchResult
    {
        public string Complete { get; set; } = "";
        public int Volume { get; set; } = 0;
        public string Time { get; set; } = "";
        public string Instrument { get; set; } = "";
    }
}
