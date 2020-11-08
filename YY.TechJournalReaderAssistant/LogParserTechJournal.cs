using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using YY.TechJournalReaderAssistant.Helpers;
using YY.TechJournalReaderAssistant.Models;

namespace YY.TechJournalReaderAssistant
{
    public static class LogParserTechJournal
    {
        #region Public Static Methods

        public static bool ItsBeginOfEvent(string sourceString)
        {
            if (sourceString == null)
                return false;

            return Regex.IsMatch(sourceString, @"^\d\d:\d\d.\d\d\d\d(\d\d)?-");
        }
        public static bool ItsEndOfEvent(StreamReader stream, string sourceString)
        {
            long previousStreamPosition = stream.GetPosition();
            string nextString = stream.ReadLineWithoutNull();
            stream.SetPosition(previousStreamPosition);

            if (ItsBeginOfEvent(nextString))
                return true;
            else
                return false;
        }
        public static RowData Parse(string originEventSource, string currentFile)
        {
            string bufferEventSource = String.Copy(originEventSource);

            RowData dataRow = new RowData();

            FileInfo currentFileInfo = new FileInfo(currentFile);
            string dateFromFileAsString = currentFileInfo.Name.Replace(".log", string.Empty);

            int indexEndOfDate = bufferEventSource.IndexOf('-');
            string periodAsString = bufferEventSource.Substring(0, indexEndOfDate);

            dataRow.Period = new DateTime(
                2000 + int.Parse(dateFromFileAsString.Substring(0, 2)),
                int.Parse(dateFromFileAsString.Substring(2, 2)),
                int.Parse(dateFromFileAsString.Substring(4, 2)),
                int.Parse(dateFromFileAsString.Substring(6, 2)),
                int.Parse(periodAsString.Substring(0, 2)),
                int.Parse(periodAsString.Substring(3, 2))
            );

            int periodMilliseconds = int.Parse(periodAsString.Substring(6, 3));
            dataRow.Period.AddMilliseconds(periodMilliseconds);

            bool isFormat_8_3 = periodAsString.Length == 12;
            if (isFormat_8_3)
                dataRow.PeriodMoment = int.Parse(periodAsString.Substring(6, 6));
            else
                dataRow.PeriodMoment = int.Parse(periodAsString.Substring(6, 4)) * 100;

            int indexEndOfDuration = bufferEventSource.IndexOf(',');
            string durationAsString = bufferEventSource.Substring(indexEndOfDate + 1, indexEndOfDuration - indexEndOfDate - 1);
            dataRow.Duration = int.Parse(durationAsString);
            dataRow.DurationSec = dataRow.Duration / (isFormat_8_3 ? 100000 : 10000);
            
            bufferEventSource = bufferEventSource.Substring(indexEndOfDuration + 1, bufferEventSource.Length - indexEndOfDuration - 1);
            int indexEndOfEventName = bufferEventSource.IndexOf(',');
            dataRow.EventName = bufferEventSource.Substring(0, indexEndOfEventName);

            // TODO:
            // Реализовать разбор остальных свойств событий и их обработку

            return dataRow;
        }

        #endregion
    }
}
