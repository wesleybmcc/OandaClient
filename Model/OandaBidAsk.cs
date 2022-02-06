using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OandaClient
{
    public interface IOandaBidAsk
    {
        double GetOpen();
        double GetHigh();
        double GetLow();
        double GetClose();
        double Convert(string value);
    }
    public class Bid : RawBidAsk, IOandaBidAsk
    {
        public double Convert(string value)
        {
            double convertedValue = 0;
            double.TryParse(value, out convertedValue);
            return convertedValue;
        }

        public double GetClose()
        {
            return Convert(this.c);
        }

        public double GetHigh()
        {
            return Convert(this.h);
        }

        public double GetLow()
        {
            return Convert(this.l);
        }

        public double GetOpen()
        {
            return Convert(this.o);
        }
    }
    public class RawBidAsk
    {
        public string o { get; set; } = "";
        public string h { get; set; } = "";
        public string l { get; set; } = "";
        public string c { get; set; } = "";
    }
}
