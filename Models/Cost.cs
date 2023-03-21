using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastCost.Models
{
    public class Cost
    {
        public string FileName { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }

        // test
        public string Title => AppInfo.Name;
        public string Version => AppInfo.VersionString;
        public string Message => "This app is written in XAML and C# with .NET MAUI.";
    }
}
