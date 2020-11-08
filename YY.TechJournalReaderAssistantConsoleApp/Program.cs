using System;
using YY.TechJournalReaderAssistant;

namespace YY.TechJournalReaderAssistantConsoleApp
{
    class Program
    {
        private static int _eventNumber;

        static void Main(string[] args)
        {
            TechJournalReader reader = TechJournalReader.CreateReader(args[0]);

            _eventNumber = 0;
            while (reader.Read())
            {
                _eventNumber += 1;
                Console.WriteLine($"Прочитано событий: {_eventNumber}");
            }

            Console.WriteLine($"{DateTime.Now}: Для выхода нажмите любую клавишу...");
            Console.ReadKey();
        }
    }
}
