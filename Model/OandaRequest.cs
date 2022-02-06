using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OandaClient
{
    internal class OandaRequest
    {
        public string Instrument { get; set; } = "";
        public string Granularity { get; set; } = "M";
        public int GranularityValue { get; set; } = 1;
        public bool IsBidAsk { get; set; } = false;
        public int Count { get; set; } = 100;
    }
}
