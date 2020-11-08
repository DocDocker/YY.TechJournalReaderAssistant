using System;
using System.Collections.Generic;
using System.Text;

namespace YY.TechJournalReaderAssistant
{
    public sealed class TechJournalPosition
    {
        #region Constructor

        public TechJournalPosition(long eventNumber, string currentFileData, long? streamPosition)
        {
            EventNumber = eventNumber;
            CurrentFileData = currentFileData;
            StreamPosition = streamPosition;
        }

        #endregion

        #region Public Properties

        public long EventNumber { get; }
        public string CurrentFileData { get; }
        public long? StreamPosition { get; }

        #endregion
    }
}
