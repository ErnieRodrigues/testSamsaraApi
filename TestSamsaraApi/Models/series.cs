using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSamsaraApi.Models
{
    public class series
    {
        public long widgetId {get;set;}
        public string field { get; set; }
    }


    public enum field
    {
        ambienTemperature  = 1,
        probeTemperatue = 2,
        currentLoop1Raw = 3,
        currentLoop1Mapped = 4,
        currentLoop2Raw = 5,
        currentLoop2Mapped = 6,
        pmPowerTotal = 7,
        pmPhase1Power = 8,
        pmPhase2Power = 9,
        pmPhase3Power = 10,
        pmPhase1PowerFactor = 11,
        pmPhase2PowerFactor = 12,
        pmPhase3PowerFactor = 13
    }
}
