using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web;
using System.Reflection;
using JMA.Plugin.Accounting.QuickBooks.LogProvider;

namespace JMA.Plugin.Accounting.QuickBooks
{
    public enum MessageSeverity
    {
        Info,Error
    }


    //public class ErrorMessageDataSource
    //{
    //    SqlLogProvider log;

    //    public ErrorMessageDataSource(string connectionString)
    //    {
    //        if (!String.IsNullOrEmpty(connectionString))
    //        {
    //            log = new SqlLogProvider(connectionString);
    //        }
    //    }
       
    //    /// <summary>
    //    /// Removes the log file
    //    /// </summary>
    //    /// <returns></returns>
    //    public string ClearLog()
    //    {
    //        try
    //        {
    //            string path = HttpContext.Current.Server.MapPath("/qblog.txt");

    //            if (File.Exists(path))
    //            {
    //                File.Delete(path);
    //            }

    //            if (log != null)
    //            {
    //                return log.ClearLog();
    //            }

    //            return "OK";

    //        }
    //        catch (Exception ex) 
    //        { 
    //            return ex.ToString(); 
    //        }
    //    }

    //    /// <summary>
    //    /// inserts data into a text file for logging
    //    /// </summary>
    //    /// <param name="message">error message to insert</param>
    //    public string WriteToLog(ErrorMessage message)
    //    {
    //        if (!String.IsNullOrEmpty(message.Message))
    //        {
    //            string path = HttpContext.Current.Server.MapPath(HttpContext.Current.Request.ApplicationPath + "/qblog.txt");
    //            FileStream fileStream = null;

    //            //If the file exists, then append it
    //            if (File.Exists(path))
    //            {
    //                fileStream = new FileStream(path, FileMode.Append);
    //            }
    //            else
    //            {
    //                fileStream = new FileStream(path, FileMode.OpenOrCreate);
    //            }
    //            StreamWriter sw = new StreamWriter(fileStream);
    //            try
    //            {
    //                sw.WriteLine(String.Format("{0} : {1} {2} {3}", message.ApplicationName, message.Severity, DateTime.Now, message.Message));
    //            }
    //            //If there is an error, just do nothing. Don't stop.
    //            catch (Exception)
    //            {

    //            }
    //            finally
    //            {
    //                sw.Close();
    //                fileStream.Close();
    //            }
    //        }

    //        if (log == null)
    //        {
    //            return "OK";
    //        }
    //        return log.WriteToLog(new ErrorMessage(message.Severity, message.ApplicationName, message.Message, message.UserName));
    //    }
    //}

    public class ErrorMessage
    {
        private MessageSeverity _severity { get; set; }
        private string _applicationName { get; set; }
        private string _message { get; set; }
        string _userName { get; set; }

        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                _userName = value;
            }
        }

        public MessageSeverity Severity
        {
            get
            {
                return _severity;
            }
        }

        public string ApplicationName
        {
            get
            {
                return _applicationName;
            }
        }

        public string Message
        {
            get
            {
                return _message;
            }
        }



        public ErrorMessage(MessageSeverity severity, string applicationName, string message, string userName = "")
        {
            _severity = severity;
            _applicationName = applicationName;
            _message = message;
            _userName = userName;
        }
    }
}
