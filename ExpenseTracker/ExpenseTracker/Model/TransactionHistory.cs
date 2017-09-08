using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Model
{
    public class TransactionHistory
    {
        public string primaryKey { get; set; }
        public string itemName { get; set; }
        public string qty { get; set; }
        public string amount { get; set; }
        public string cash { get; set; }
        public string total { get; set; }
        public string change { get; set; }
        public string tax { get; set; }
        public string transDate { get; set; }
        public string estName { get; set; }
        public int? establishment_Id { get; set; }

    }
}
