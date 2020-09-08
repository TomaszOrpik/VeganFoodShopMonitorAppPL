using System;
using System.Collections.Generic;
using System.Text;

namespace VFSMonitor.Models
{
    public class Session
    {
        public string UserId { get; set; }
        public string SessionId { get; set; }
        public string UserIp { get; set; }
        public int VisitCounter { get; set; }
        public string VisitDate { get; set; }
        public string Device { get; set; }
        public string Browser { get; set; }
        public string Location { get; set; }
        public string Reffer { get; set; }
        public IList<Page> Pages { get; set; }
        public IList<CartItem> CartItems { get; set; }
        public IList<BuyedItem> BuyedItems { get; set; }
        public bool DidLogged { get; set; }
        public bool DidContacted { get; set; }
    }
}
