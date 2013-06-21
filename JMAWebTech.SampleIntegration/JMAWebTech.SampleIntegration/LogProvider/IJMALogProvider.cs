using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JMA.Plugin.Accounting.QuickBooks.LogProvider
{
    public interface IJMALogProvider
    {
        string WriteToLog(ErrorMessage message);
        string ClearLog();
    }
}
