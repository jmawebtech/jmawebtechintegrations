using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using JMA.Plugin.Accounting.QuickBooks.DTO;

namespace JMA.Plugin.Accounting.QuickBooks.LogProvider
{
    public class ErrorLogResult
    {

        public int ID { get; set; }
        public string UserName { get; set; }
        public string ErrorMessageType { get; set; }
        public string ErrorMessage { get; set; }
        public DateTime ErrorWriteToLoged { get; set; }

    }

    public class SqlLogProvider : IJMALogProvider
    {
        #region private variables

        private DataTable _dt;
        string _connectionString;
        string userName;
        #endregion

        #region ctor

        public SqlLogProvider(string connectionString, string userName = "admin")
        {
            _connectionString = connectionString;
            this.userName = userName;
            _dt = new DataTable();
            Install();
        }

        #endregion

        public void GetErrorMesssages(DateTime beginTime, DateTime endTime)
        {
            if (_dt.Rows.Count == 0)
            {
                string query = "select * from jma_error_log";

                if (beginTime != null && endTime != null)
                {
                    query = String.Format("select * from jma_error_log where ErrorWriteToLoged >= '{0}' AND ErrorWriteToLoged <= '{1}'", beginTime, endTime);
                }

                SqlConnection conn = new SqlConnection(_connectionString);
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(_dt);
                conn.Close();
                da.Dispose();
            }
        }


        /// <summary>
        /// creates the sql script for the settings
        /// </summary>
        /// <returns></returns>
        public string Install()
        {
            try
            {
                SqlConnection con = new SqlConnection(_connectionString);
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText =
                        @" 
                       IF (NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'jma_error_log'))
BEGIN
CREATE TABLE [dbo].[jma_error_log](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](100) NOT NULL,
    [Application] [nvarchar](100) NOT NULL,
	[ErrorMessageType] [nvarchar](100) NOT NULL,
	[ErrorMessage] [nvarchar](max) NOT NULL,
    [ErrorWriteToLoged] [datetime2](7) NOT NULL,
); END";
                    cmd.ExecuteNonQuery();
                }
                con.Close();
                return "OK";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }


        public string Uninstall()
        {
            try
            {
                SqlConnection con = new SqlConnection(_connectionString);
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText =
                        @"IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[jma_error_log]') AND type in (N'U'))
DROP TABLE [jma_error_log]";
                    cmd.ExecuteNonQuery();
                }
                con.Close();
                return "OK";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }


        public string ClearLog()
        {
            try
            {
                SqlConnection con = new SqlConnection(_connectionString);
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText =
                        String.Format("delete jma_error_log where UserName = '{0}'", userName);
                    cmd.ExecuteNonQuery();
                }
                con.Close();
                return "OK";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        private DataSet DoSql(string sql)
        {
            DataSet dt = new DataSet();
            SqlConnection con = new SqlConnection(_connectionString);
            SqlDataAdapter sQLDA;
            sQLDA = new SqlDataAdapter(sql, con);
            sQLDA.Fill(dt);
            return dt;

        }


        public string WriteToLog(ErrorMessage message)
        {
            string userName = message.UserName;
            string application = message.ApplicationName;
            string errorMessageType = message.Severity.ToString();
            string errorMessage = message.Message;

            try
            {
                string SqlStatement = String.Format("INSERT INTO jma_error_log (UserName, Application, ErrorMessageType, ErrorMessage, ErrorWriteToLoged) VALUES ('{0}','{1}','{2}','{3}', '{4}')",
                userName, application, errorMessageType, errorMessage, DateTime.Now);
                DoSql(SqlStatement);
                return "OK";
            }
            catch (Exception ex)
            {
                return "ERROR";
            }

        }
    }
}
