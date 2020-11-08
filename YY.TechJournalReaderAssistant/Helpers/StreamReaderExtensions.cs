using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace YY.TechJournalReaderAssistant.Helpers
{
    internal static class StreamReaderExtensions
    {
        #region Private Member Variables

        private static readonly FieldInfo CharPosField = typeof(StreamReader).GetField("_charPos", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);
        static readonly FieldInfo ByteLenField = typeof(StreamReader).GetField("_byteLen", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);
        static readonly FieldInfo CharBufferField = typeof(StreamReader).GetField("_charBuffer", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);

        #endregion

        #region Public Methods

        public static string ReadLineWithoutNull(this StreamReader reader)
        {
            StringBuilder sb = new StringBuilder();
            while (!reader.EndOfStream)
            {
                Char c = (char)reader.Read();
                if (c == '\0') 
                    continue;
                sb.Append(c);
                if (c == '\n')
                    break;
            }

            var lineString = sb.Length > 0 ? sb.ToString().Trim() : null;
            return lineString;
        }
        public static long GetPosition(this StreamReader reader)
        {
            int byteLen = (int)ByteLenField.GetValue(reader);
            var position = reader.BaseStream.Position - byteLen;

            int charPos = (int)CharPosField.GetValue(reader);
            if (charPos > 0)
            {
                var charBuffer = (char[])CharBufferField.GetValue(reader);
                var encoding = reader.CurrentEncoding;
                var bytesConsumed = encoding.GetBytes(charBuffer, 0, charPos).Length;
                position += bytesConsumed;
            }

            return position;
        }
        public static void SetPosition(this StreamReader reader, long position)
        {
            reader.DiscardBufferedData();
            reader.BaseStream.Seek(position, SeekOrigin.Begin);
        }
        public static void SkipLine(this StreamReader stream, long numberToSkip)
        {
            for (int i = 0; i < numberToSkip; i++)
            {
                stream.ReadLine();
            }
        }

        #endregion
    }
}
