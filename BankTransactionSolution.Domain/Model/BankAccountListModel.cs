using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTransactionSolution.Domain.Model
{
    public class BankAccountListModel
    {
        public int id {  get; set; }    
        public int user_id { get; set; }
        public string user_full_name { get; set; }
        public string bank_account { get; set; }
        public string bank_code { get; set; }
        public string bank_name { get; set; }
        public double balance { get; set; } 
    }
}
