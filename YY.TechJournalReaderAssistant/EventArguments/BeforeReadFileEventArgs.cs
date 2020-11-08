namespace YY.TechJournalReaderAssistant.EventArguments
{
    public sealed class BeforeReadFileEventArgs : System.EventArgs
    {
        public BeforeReadFileEventArgs(string fileName)
        {
            FileName = fileName;
            Cancel = false;
        }

        public string FileName { get; }
        public bool Cancel { get; }
    }
}
