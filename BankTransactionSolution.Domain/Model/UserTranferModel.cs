using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTransactionSolution.Domain.Model
{
    public class UserTranferModel
    {
        public int id_user_tranfer {  get; set; }
        public int from_account_id { get; set; }
        public int to_account_id { get; set; }
        public double amount { get; set; }
        public string currency { get; set; }
        public string description { get; set; }
    }
}
