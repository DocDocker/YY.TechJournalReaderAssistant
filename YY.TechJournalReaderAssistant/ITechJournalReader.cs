namespace YY.TechJournalReaderAssistant
{
    public interface ITechJournalReader
    {
        bool Read();
        bool GoToEvent(long eventNumber);
        TechJournalPosition GetCurrentPosition();
        void SetCurrentPosition(TechJournalPosition newPosition);
        long Count();
        void Reset();
        void NextFile();
    }
}
