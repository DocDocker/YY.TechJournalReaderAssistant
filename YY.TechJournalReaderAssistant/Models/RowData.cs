using System;
using System.Collections.Generic;
using System.Text;

namespace YY.TechJournalReaderAssistant.Models
{
    public class RowData
    {
        public RowData()
        {
            Properties = new Dictionary<string, string>();
        }

        public DateTime Period { set; get; }
        public long PeriodMoment { set; get; }
        public long Duration { set; get; }
        public long DurationSec => Duration / 1000000;
        public string EventName { set; get; }
        public int Level { set; get; }
        public Dictionary<string, string> Properties { set; get; }
    }
}
