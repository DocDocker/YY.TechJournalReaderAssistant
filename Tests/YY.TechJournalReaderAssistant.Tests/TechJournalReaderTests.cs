using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Xunit;
using System.IO.Compression;
using YY.TechJournalReaderAssistant.Models;

namespace YY.TechJournalReaderAssistant.Tests
{
    public class TechJournalReaderTests
    {
        [Fact]
        public void Test_ReadSampleData()
        {
            string unitTestDirectory = Directory.GetCurrentDirectory();

            string logArchive = Path.Combine(unitTestDirectory, "TestData", "TestData_ServerAndClusterLogs.zip");
            string logDataPath = Path.Combine(unitTestDirectory, "TestData", "TestData_ServerAndClusterLogs");
            if(Directory.Exists(logDataPath)) Directory.Delete(logDataPath, true);
            ZipFile.ExtractToDirectory(logArchive, logDataPath);

            int eventNumber = 0;
            RowData lastRow = null;
            List<RowData> allRows = new List<RowData>();

            TechJournalManager tjManager = new TechJournalManager(logDataPath);
            foreach (var tjDirectory in tjManager.Directories)
            {
                TechJournalReader tjReader = TechJournalReader.CreateReader(tjDirectory.DirectoryData.FullName);
                while (tjReader.Read())
                {
                    eventNumber += 1;
                    lastRow = tjReader.CurrentRow;
                    allRows.Add(tjReader.CurrentRow);
                }
            }

            Assert.NotNull(lastRow);
            Assert.NotEqual(0, eventNumber);
            Assert.Equal(3113, eventNumber);
            Assert.Equal(lastRow.Properties["Txt"], "1C:Enterprise 8.3 (x86-64) (8.3.17.1496) Working Process (debug) terminated.");
        }
    }
}
