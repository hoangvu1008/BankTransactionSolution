using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BankTransactionSolution.Domain.Model
{
    public class TransactionHistoryModel
    {
        public int id {  get; set; }
        public DateTime date_created { get; set; }  
        public int from_account_id { get; set; }
        public string from_account { get; set; }
        public string from_account_bank { get; set; }
        public int to_account_id { get; set; }
        public string to_account { get; set; }
        public string to_account_bank { get; set; }
        public double amount { get; set; }
        public string currency { get; set; } = "VND";
        public string description { get; set; }
        public TransactionStatus status { get; set; }
        public string status_text => status.ToString();
    }
}
