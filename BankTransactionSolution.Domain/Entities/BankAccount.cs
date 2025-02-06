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
        public BankAccount(int user_id, string bank_account, double balance, string currency, string bank_code, string bank_name)
        {
            this.user_id = user_id;
            this.bank_account = bank_account;
            this.balance = balance;
            this.currency = currency;
            from_transactions = new HashSet<Transaction>();
            to_transactions = new HashSet<Transaction>();
            this.bank_code = bank_code;
            this.bank_name = bank_name;
        }
        public int user_id { get; set; }    
        public string bank_account { get; set; }  
        public string bank_code { get; set; }
        public string bank_name { get; set; }   
        public double balance { get; set; } 
        public string currency { get; set; }
        [JsonIgnore]
        public virtual User user { get; set; }
        [JsonIgnore]
        public   virtual ICollection<Transaction> from_transactions { get; set; }
        [JsonIgnore]
        public   virtual ICollection<Transaction> to_transactions { get; set; }
    }
}
