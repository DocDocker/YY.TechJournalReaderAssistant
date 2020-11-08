using System;

namespace YY.TechJournalReaderAssistant.EventArguments
{
    public sealed class AfterReadFileEventArgs : EventArgs
    {
        public AfterReadFileEventArgs(string fileName)
        {
            FileName = fileName;
        }

        public string FileName { get; }
    }
}
