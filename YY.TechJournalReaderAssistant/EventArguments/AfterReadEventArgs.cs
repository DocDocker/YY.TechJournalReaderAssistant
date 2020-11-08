using System;
using YY.TechJournalReaderAssistant.Models;

namespace YY.TechJournalReaderAssistant.EventArguments
{
    public sealed class AfterReadEventArgs : EventArgs
    {
        public AfterReadEventArgs(RowData rowData, long eventNumber)
        {
            RowData = rowData;
            EventNumber = eventNumber;
        }

        public RowData RowData { get; }
        public long EventNumber { get; }
    }    
}
