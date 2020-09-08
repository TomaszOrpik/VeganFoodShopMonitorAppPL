using System;
using System.Collections.Generic;
using System.Text;

namespace VFSMonitor.Models
{
    public class GlobalAverageData
    {
        public string MostUsedDevice { get; set; }
        public string MostUsedBrowser { get; set; }
        public string MostPopularLocation { get; set; }
        public string MostPopularReffer { get; set; }
        public decimal AverageTimeOnPages { get; set; }
        public decimal AvItemBuy { get; set; }
        public bool MostlyLogged { get; set; }
    }
}
