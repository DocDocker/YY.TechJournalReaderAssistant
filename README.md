# Помощник чтения технологического журнала 1С:Предприятие 8.x [![NuGet version](https://badge.fury.io/nu/YY.TechJournalReaderAssistant.svg)](https://badge.fury.io/nu/YY.TechJournalReaderAssistant)

Библиотека для чтения файлов технологического журнала платформы 1С:Предприятие 8.x.

### Состояние сборки
| Windows |  Linux |
|:-------:|:------:|
| NONE | ![.NET Core](https://github.com/YPermitin/YY.TechJournalReaderAssistant/workflows/.NET%20Core/badge.svg) |

## Благодарности

Выражаю большую благодарность **[Алексею Бочкову](https://github.com/alekseybochkov)** как идейному вдохновителю. 

Именно его разработка была первой реализацией чтения и экспорта технологического журнала 1С - **[TJ_LOADER](https://github.com/alekseybochkov/tj_loader)**. Основную идею и некоторые примеры реализации взял именно из нее, но с полной переработкой архитектуры библиотеки.

## Состав репозитория

* **YY.TechJournalReaderAssistant** - исходный код библиотеки
* **YY.TechJournalReaderAssistant.Tests** - unit-тесты для проверки работоспособности библиотеки.
* **YY.TechJournalReaderAssistantConsoleApp** - консольное приложение с примерами использования библиотеки.

## Требования и совместимость

Работа библиотеки тестировалась с платформой 1С:Предприятие версии от 8.3.6 и выше.

В большинстве случаев работоспособность подтверждается и на более старых версиях, но меньше тестируется. Основная разработка ведется для Microsoft Windows, но некоторый функционал проверялся под *.nix.*

## Примеры использования

Для примера создадим консольное приложение с таким содержимым в методе "Main()":

```csharp
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
                        // tjReader.CurrentRow - данные текущего события
                        // tjReader.CurrentRow.Properties - сырые данные события "как есть" без обработки в виде словаря Dictionary<string, string>
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

```

Для удобной обработки результатов чтения и других связанных событий можно использовать события (инициализировали подписки на события выше), но не обязательно. Для подписки доступны события:

* **BeforeReadFile** - перед чтением файла.
* **AfterReadFile** - после чтения файла.
* **BeforeReadEvent** - перед чтением события.
* **AfterReadEvent** - после чтения события.
* **OnErrorEvent** - событие при возникновении ошибки.

Пример обработчиков событий приведен выше в листинге кода.

## TODO

* Оптимизация производительности
* LINQ-провайдер для чтения данных

## Лицензия

MIT - делайте все, что посчитаете нужным. Никакой гарантии и никаких ограничений по использованию.