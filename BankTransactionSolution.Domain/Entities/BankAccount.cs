using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BankTransactionSolution.Domain.Entities
{
    public class BankAccount :BaseEntity
    {
        public BankAccount(int user_id, string account_number, double balance, string currency)
        {
            this.user_id = user_id;
            this.account_number = account_number;
            this.balance = balance;
            this.currency = currency;
            from_transactions = new HashSet<Transaction>();
            to_transactions = new HashSet<Transaction>();
        }
        public int user_id { get; set; }    
        public string account_number { get; set; }  
        public double balance { get; set; } 
        public string currency { get; set; }

        [JsonIgnore]
        public virtual User user { get; set; }
        [JsonIgnore]
        public   virtual ICollection<Transaction> from_transactions { get; set; }
        public   virtual ICollection<Transaction> to_transactions { get; set; }
    }
}
