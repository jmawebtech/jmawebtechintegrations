using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace JMA.Plugin.Accounting.QuickBooks.DTO
{
    /// <summary>
    /// associates orders and settings with a particular user
    /// </summary>
    public class JMAUser
    {
        public List<JMAOrder> Orders { get; set; }
        public JMASettings Settings { get; set; }
    }
}
