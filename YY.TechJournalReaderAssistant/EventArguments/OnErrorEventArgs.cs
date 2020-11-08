using System;

namespace YY.TechJournalReaderAssistant.EventArguments
{
    public sealed class OnErrorEventArgs : EventArgs
    {
        public OnErrorEventArgs(Exception exception, string sourceData, bool critical)
        {
            Exception = exception;
            SourceData = sourceData;
            Critical = critical;
        }

        public Exception Exception { get; }
        public string SourceData { get; }
        public bool Critical { get; }
    }    
}
