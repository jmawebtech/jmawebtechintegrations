using System;
using JMA.Plugin.Accounting.QuickBooks.DTO;

namespace JMA.Plugin.Accounting.QuickBooks.SettingProvider
{
    public interface ISqlSettingProvider
    {
        string ClearSettings();
        JMASettings GetSettings(string userName = "admin", string password = "admin");
        string Install();
        string Uninstall();
        string Update_BulkSetting(JMASettings qboSettings, string userName = "admin");
        string WriteSetting(string name, string value, string userName = "admin", string password = "admin");
    }
}
