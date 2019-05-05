using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryMobileBank
{
    class TodoItem
    {
        public string id { get; set; }
        public string BillingCompany { get; set; }
        public string Text { get; set; }

        public string Type { get; set; }
        public string DateRecieved { get; set; }
        public string DateDue { get; set; }
        public string payment { get; set; }
        public bool Complete { get; set; }
    }
}
