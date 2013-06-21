using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace JMA.Plugin.Accounting.QuickBooks.LogProvider
{
    public class TextLogProvider : IJMALogProvider
    {
        public string WriteToLog(ErrorMessage message)
        {
            string path = string.Empty;
            if (!String.IsNullOrEmpty(message.Message))
            {
                try
                {
                    path = HttpContext.Current.Server.MapPath(HttpContext.Current.Request.ApplicationPath + "/qblog.txt");
                }
                catch (Exception)
                {
                    path = "/qblog.txt";
                }
                FileStream fileStream = null;

                //If the file exists, then append it
                if (File.Exists(path))
                {
                    fileStream = new FileStream(path, FileMode.Append);
                }
                else
                {
                    fileStream = new FileStream(path, FileMode.OpenOrCreate);
                }
                StreamWriter sw = new StreamWriter(fileStream);
                try
                {
                    sw.WriteLine(String.Format("{0} : {1} {2} {3}", message.ApplicationName, message.Severity, DateTime.Now, message.Message));
                    return "OK";
                }
                //If there is an error, just do nothing. Don't stop.
                catch (Exception ex)
                {
                    return ex.ToString();
                }
                finally
                {
                    sw.Close();
                    fileStream.Close();
                }

            }

            return "Message is null or empty";
        }

        public string ClearLog()
        {
            try
            {
                string path = HttpContext.Current.Server.MapPath("~/qblog.txt");

                if (File.Exists(path))
                {
                    File.Delete(path);
                }

                return "OK";

            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
    }
}
