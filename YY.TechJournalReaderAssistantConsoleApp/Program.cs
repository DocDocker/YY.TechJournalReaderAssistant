using System;
using System.Collections.Generic;
using YY.TechJournalReaderAssistant;
using YY.TechJournalReaderAssistant.Models;

namespace YY.TechJournalReaderAssistantConsoleApp
{
    class Program
    {
        private static int _eventNumber;

        static void Main(string[] args)
        {
            List<RowData> allRows = new List<RowData>();
            RowData lastRow = null;

            TechJournalManager tjManager = new TechJournalManager(args[0]);
            foreach (var tjDirectory in tjManager.Directories)
            {
                TechJournalReader tjReader = TechJournalReader.CreateReader(tjDirectory.DirectoryData.FullName);
                while (tjReader.Read())
                {
                    _eventNumber += 1;
                    lastRow = tjReader.CurrentRow;
                    allRows.Add(tjReader.CurrentRow);
                    Console.WriteLine($"[{tjDirectory.DirectoryData.Name}]: {_eventNumber}");
                }
            }

            Console.WriteLine($"Total event count: {_eventNumber}");
            if (allRows.Count > 0) Console.WriteLine($"First period: {allRows[0].Period}");
            if (lastRow != null) Console.WriteLine($"Last period: {lastRow.Period}");
            Console.WriteLine($"{DateTime.Now}: Для выхода нажмите любую клавишу...");
            Console.ReadKey();
        }
    }
}
