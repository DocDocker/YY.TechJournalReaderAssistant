using System;
using System.Collections.Generic;
using System.Text;

namespace YY.TechJournalReaderAssistant.Models
{
    public class RowData
    {
        public DateTime Period { set; get; }
        public long PeriodMoment { set; get; }
        public long Duration { set; get; }
        public long DurationSec { set; get; }
        public string EventName { set; get; }
    }
}
