using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Mvc;
using JMA.Plugin.Accounting.QuickBooks;
using JMA.Plugin.Accounting.QuickBooks.DTO;
using JMA.Plugin.Accounting.QuickBooks.LogProvider;
using JMA.Plugin.Accounting.QuickBooks.SettingProvider;

namespace CloudCartConnector.QuickBooks.Core.Interfaces
{
    public class JMAWebServiceCommonBase
    {
        #region public

        public string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
            }
        }

        //public CloudCartConnector.QuickBooks.Core.QBServiceReference.QBOWebService sc { get; set; }


        public Controller Page { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string QBUrl { get; set; }
        public string ECommerceSystem { get; set; }
        public JMASettings Settings { get; set; }
        public string FriendlyEmail { get; set; }
        public ISqlSettingProvider SettingProvider { get; set; }
        public IJMALogProvider LogProvider { get; set; }

        #endregion

        #region ctor

        public JMAWebServiceCommonBase(Controller page, string userName, string password, string qbUrl, string eCommerceSystem, JMASettings jmaSettings)
        {
            Page = page;
            UserName = userName; 
            Password = password; 
            QBUrl = qbUrl;
            ECommerceSystem = eCommerceSystem;

            if (page.Session["FriendlyEmail"] != null)
            {
                FriendlyEmail = page.Session["FriendlyEmail"].ToString();
            }
            else
            {
                FriendlyEmail = userName;
            }
            
            //SettingProvider = SettingFactory.Get(ConnectionString, FriendlyEmail);
            LogProvider = LogFactory.Get(ConnectionString, FriendlyEmail);

            if (jmaSettings != null)
            {
                Settings = jmaSettings;
            }
            else
            {
                Settings = SettingProvider.GetSettings(FriendlyEmail);
            }

        }

        #endregion

        public virtual bool AuthenticateToWebService()
        {
            if(Page.Session != null)
            {
                Page.Session["QBUrl"] = QBUrl;
                Page.Session["UserName"] = UserName;
                Page.Session["Password"] = Password;
                Page.Session["ECommerceSystem"] = ECommerceSystem;
                Page.Session["LoggedIn"] = true;
            }

            return true;
        }

        public string ClearLog() 
        {
            return LogProvider.ClearLog();
        }

        public virtual JMAUser GetOrders()
        {
            return new JMAUser();
        }

        public JMASettings GetSettings()
        {
            return SettingProvider.GetSettings(FriendlyEmail);
        }

        public string SaveSettings(JMASettings model)
        {
            return SettingProvider.Update_BulkSetting(model, FriendlyEmail);
        }

        public string WriteSetting(string name, string value)
        {
            return SettingProvider.WriteSetting(name, value, FriendlyEmail);
        }
    }
}
