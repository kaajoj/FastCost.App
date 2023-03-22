using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastCost.Models
{
    public class Cost
    {
        public decimal Value { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string FileName { get; set; }

        // test
        public string Title => AppInfo.Name;
        public string Version => AppInfo.VersionString;
        public string Message => "This app is written in XAML and C# with .NET MAUI.";
        public string Text { get; set; }
    }
}
