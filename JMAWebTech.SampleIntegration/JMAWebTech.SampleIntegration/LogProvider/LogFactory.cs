using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace JMA.Plugin.Accounting.QuickBooks.LogProvider
{
    public static class LogFactory
    {
        public static IJMALogProvider Get(string connectionString, string userName = "admin")
        {
            if (ConfigurationManager.AppSettings["LogProvider"] != null && ConfigurationManager.AppSettings["LogProvider"] == "SQL")
            {
                if (String.IsNullOrEmpty(connectionString))
                {
                    throw new Exception("Connection string for SQL Log Provider cannot be null. Please enter a valid connection string.");
                }
                return new SqlLogProvider(connectionString, userName);
            }

            return new TextLogProvider();
        }
    }
}
