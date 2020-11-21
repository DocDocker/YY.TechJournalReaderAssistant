using System;
using System.Collections.Generic;
using YY.TechJournalReaderAssistant;
using YY.TechJournalReaderAssistant.EventArguments;
using YY.TechJournalReaderAssistant.Models;

namespace YY.TechJournalReaderAssistantConsoleApp
{
    static class Program
    {
        private static int _totalEventNumber;
        private static int _eventNumber;
        private static DateTime _lastPeriodEvent = DateTime.MinValue;
        private static TechJournalDirectory _lastLogDirectory;

        static void Main(string[] args)
        {
            if (args.Length == 0)
                return;
            
            string dataDirectoryPath = args[0];
            Console.WriteLine($"{DateTime.Now}: Инициализация чтения логов \"{dataDirectoryPath}\"...");
            Console.WriteLine();

            TechJournalManager tjManager = new TechJournalManager(dataDirectoryPath);
            foreach (var tjDirectory in tjManager.Directories)
            {
                _lastLogDirectory = tjDirectory;

                using (TechJournalReader tjReader = TechJournalReader.CreateReader(tjDirectory.DirectoryData.FullName))
                {
                    tjReader.AfterReadEvent += Reader_AfterReadEvent;
                    tjReader.AfterReadFile += Reader_AfterReadFile;
                    tjReader.BeforeReadEvent += Reader_BeforeReadEvent;
                    tjReader.BeforeReadFile += Reader_BeforeReadFile;
                    tjReader.OnErrorEvent += Reader_OnErrorEvent;

                    Console.WriteLine($"{DateTime.Now}: [{_lastLogDirectory}] Всего событий к обработке: ({tjReader.Count()})...");

                    while (tjReader.Read())
                    {
                        // reader.CurrentRow - данные текущего события
                        _eventNumber += 1;
                        _totalEventNumber += 1;
                    }
                }
            }

            Console.WriteLine($"{DateTime.Now}: Всего событий прочитано: ({_totalEventNumber})...");
            Console.WriteLine($"{DateTime.Now}: Для выхода нажмите любую клавишу...");
            Console.ReadKey();
        }

        #region Events

        private static void Reader_BeforeReadFile(TechJournalReader sender, BeforeReadFileEventArgs args)
        {
            _eventNumber = 0;
            Console.WriteLine($"{DateTime.Now}: [{_lastLogDirectory}] Начало чтения файла \"{args.FileName}\"");
            Console.WriteLine($"{DateTime.Now}: [{_lastLogDirectory}] {_eventNumber}");
            Console.WriteLine($"{DateTime.Now}: [{_lastLogDirectory}] {_lastPeriodEvent}");
        }

        private static void Reader_AfterReadFile(TechJournalReader sender, AfterReadFileEventArgs args)
        {
            Console.WriteLine($"{DateTime.Now}: [{_lastLogDirectory}] Окончание чтения файла \"{args.FileName}\"");
            Console.WriteLine();
        }

        private static void Reader_BeforeReadEvent(TechJournalReader sender, BeforeReadEventArgs args)
        {
            Console.SetCursorPosition(0, Console.CursorTop - 2);
            Console.WriteLine($"{DateTime.Now}: [{_lastLogDirectory}] (+){_eventNumber}");
            Console.WriteLine($"{DateTime.Now}: [{_lastLogDirectory}] {_lastPeriodEvent}");
        }

        private static void Reader_AfterReadEvent(TechJournalReader sender, AfterReadEventArgs args)
        {
            if (args.RowData != null)
                _lastPeriodEvent = args.RowData.Period;

            Console.SetCursorPosition(0, Console.CursorTop - 2);
            Console.WriteLine($"{DateTime.Now}: [{_lastLogDirectory}] [+]{_eventNumber}");
            Console.WriteLine($"{DateTime.Now}: [{_lastLogDirectory}] {_lastPeriodEvent}");
        }

        private static void Reader_OnErrorEvent(TechJournalReader sender, OnErrorEventArgs args)
        {
            Console.WriteLine($"{DateTime.Now}: [{_lastLogDirectory}] Ошибка чтения логов \"{args.Exception}\"");
        }

        #endregion
    }
}
