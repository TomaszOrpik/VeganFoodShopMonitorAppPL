using System;
using System.Collections.Generic;
using System.Text;

namespace VFSMonitor.Models
{
    public class UserAverageData
    {
        public string UserId { get; set; }
        public string UserIp { get; set; }
        public string MostUsedDevice { get; set; }
        public string MostUsedBrowser { get; set; }
        public string MostPopularLocation { get; set; }
        public string MostPopularReffer { get; set; }
        public decimal AverageTimeOnPages { get; set; }
        public string AvCartAction { get; set; }
        public decimal AvItemBuy { get; set; }
        public bool MostlyLogged { get; set; }
    }
}
