using System;
using System.Collections.Generic;

namespace YY.TechJournalReaderAssistant.Models
{
    public class RowData
    {
        public RowData()
        {
            Properties = new Dictionary<string, string>();
        }

        public DateTime Period { set; get; }
        public long PeriodMoment { set; get; }
        public long Duration { set; get; }
        public long DurationSec => Duration / 1000000;
        public string EventName { set; get; }
        public int Level { set; get; }
        public string Process
        {
            get
            {
                if (Properties.ContainsKey("process"))
                    return Properties["process"];

                return null;
            }
        }
        public string ProcessName
        {
            get
            {
                if (Properties.ContainsKey("p:processName"))
                    return Properties["p:processName"];

                return null;
            }
        }
        public int? ClientId
        {
            get
            {
                if (Properties.ContainsKey("t:clientID") && int.TryParse(Properties["t:clientID"], out var clientIdValue))
                    return clientIdValue;

                return null;
            }
        }
        public string ApplicationName
        {
            get
            {
                if (Properties.ContainsKey("t:applicationName"))
                    return Properties["t:applicationName"];

                return null;
            }
        }
        public string ComputerName
        {
            get
            {
                if (Properties.ContainsKey("t:computerName"))
                    return Properties["t:computerName"];

                return null;
            }
        }
        public int? ConnectionId
        {
            get
            {
                if (Properties.ContainsKey("t:connectID") && int.TryParse(Properties["t:connectID"], out var connectionIdValue))
                    return connectionIdValue;

                return null;
            }
        }
        public int? SessionId
        {
            get
            {
                if (Properties.ContainsKey("SessionID") && int.TryParse(Properties["SessionID"], out var sessionIdValue))
                    return sessionIdValue;

                return null;
            }
        }
        public string UserName
        {
            get
            {
                if (Properties.ContainsKey("Usr"))
                    return Properties["Usr"];

                return null;
            }
        }
        public int? ApplicationId
        {
            get
            {
                if (Properties.ContainsKey("AppID") && int.TryParse(Properties["AppID"], out var applicationIdValue))
                    return applicationIdValue;

                return null;
            }
        }
        public int? DatabasePid
        {
            get
            {
                if (Properties.ContainsKey("dbpid") && int.TryParse(Properties["dbpid"], out var databasePIDValue))
                    return databasePIDValue;

                return null;
            }
        }
        public string SQL
        {
            get
            {
                if (Properties.ContainsKey("Sql"))
                    return Properties["Sql"];

                return null;
            }
        }
        public string SDBL
        {
            get
            {
                if (Properties.ContainsKey("Sdbl"))
                    return Properties["Sdbl"];

                return null;
            }
        }
        public string Parameters
        {
            get
            {
                if (Properties.ContainsKey("Prm"))
                    return Properties["Prm"];

                return null;
            }
        }
        public object _ILev
        {
            get
            {
                if (Properties.ContainsKey("ILev"))
                    return Properties["ILev"];

                return null;
            }
        }
        public int? Rows
        {
            get
            {
                if (Properties.ContainsKey("Rows") && int.TryParse(Properties["Rows"], out var rowsValue))
                    return rowsValue;

                return null;
            }
        }
        public string Context
        {
            get
            {
                if (Properties.ContainsKey("Context"))
                    return Properties["Context"];

                return null;
            }
        }
        public object _Trans
        {
            get
            {
                if (Properties.ContainsKey("Trans"))
                    return Properties["Trans"];

                return null;
            }
        }
        public object _Func
        {
            get
            {
                if (Properties.ContainsKey("Func"))
                    return Properties["Func"];

                return null;
            }
        }
        public int? RowsAffected
        {
            get
            {
                if (Properties.ContainsKey("RowsAffected") && int.TryParse(Properties["RowsAffected"], out var rowsAffectedValue))
                    return rowsAffectedValue;

                return null;
            }
        }
        public string Description
        {
            get
            {
                if (Properties.ContainsKey("Descr"))
                    return Properties["Descr"];

                return null;
            }
        }
        public string PlanSQLAsText
        {
            get
            {
                if (Properties.ContainsKey("planSQLText"))
                    return Properties["planSQLText"];

                return null;
            }
        }
        public string Exception
        {
            get
            {
                if (Properties.ContainsKey("Exception"))
                    return Properties["Exception"];

                return null;
            }
        }
        public Dictionary<string, string> Properties { set; get; }
    }
}
