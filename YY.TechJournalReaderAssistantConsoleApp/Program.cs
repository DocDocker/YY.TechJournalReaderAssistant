using System;
using YY.TechJournalReaderAssistant;

namespace YY.TechJournalReaderAssistantConsoleApp
{
    class Program
    {
        private static int _eventNumber;

        static void Main(string[] args)
        {
            TechJournalManager tjManager = new TechJournalManager(args[0]);
            foreach (var tjDirectory in tjManager.Directories)
            {
                int eventNumber = 0;
                TechJournalReader tjReader = TechJournalReader.CreateReader(tjDirectory.DirectoryData.FullName);
                while (tjReader.Read())
                {
                    eventNumber += 1;
                    Console.WriteLine($"[{tjDirectory.DirectoryData.Name}]: {eventNumber}");
                }
            }

            Console.WriteLine($"{DateTime.Now}: Для выхода нажмите любую клавишу...");
            Console.ReadKey();
        }
    }
}
