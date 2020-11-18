using System.Collections.Generic;
using System.IO;
using YY.TechJournalReaderAssistant.Models;

namespace YY.TechJournalReaderAssistant
{
    public class TechJournalManager
    {
        public List<TechJournalDirectory> Directories { set; get; }

        public TechJournalManager(string logFilePath)
        {
            Directories = new List<TechJournalDirectory>();

            /* 
             * Варианты:
             * (+) 1. Передан путь на конкретный файл
             * 2. Передан путь на каталог ТЖ процесса
             * 3. Передан путь на каталог ТЖ с подканалогами по процессам
             * 4. Передан путь на файл или каталог, не связанный с ТЖ
             */

            if (File.GetAttributes(logFilePath).HasFlag(FileAttributes.Directory))
            {
                DirectoryInfo logDirectoryInfo = new DirectoryInfo(logFilePath);
                if (TechJournalDirectory.ParseDataFromDirectoryName(logDirectoryInfo.Name)
                    && TechJournalDirectory.ContainsLogFiles(logFilePath))
                {
                    Directories.Add(new TechJournalDirectory(logFilePath));
                }
                else
                {
                    string[] techJournalSubDirectories = Directory.GetDirectories(logFilePath);
                    foreach (var techJournalSubDirectory in techJournalSubDirectories)
                    {
                        DirectoryInfo techJournalSubDirectoryInfo = new DirectoryInfo(techJournalSubDirectory);
                        if (TechJournalDirectory.ParseDataFromDirectoryName(logDirectoryInfo.Name)
                            && TechJournalDirectory.ContainsLogFiles(techJournalSubDirectoryInfo.FullName))
                        {
                            Directories.Add(new TechJournalDirectory(techJournalSubDirectoryInfo.FullName));
                        }
                    }
                }
            }
            else
            {
                Directories.Add(new TechJournalDirectory(logFilePath));
            }
        }
    }
}
