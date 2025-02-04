﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Transactions;

namespace BankTransactionSolution.Domain.Entities
{
    public class Transaction : BaseEntity
    {
        public Transaction(int from_account_id, int to_account_id, double amount, string currency, TransactionStatus status)
        {
            this.from_account_id = from_account_id;
            this.to_account_id = to_account_id;
            this.amount = amount;
            this.currency = currency;
            this.status = status;
            transaction_logs = new HashSet<TransactionLogs>();
        }
        public int from_account_id { get; set; }
        public int to_account_id { get; set; }
        public double amount { get; set; }
        public string currency {  get; set; }   
        public string description { get; set; } 
        public TransactionStatus status { get; set; }
        public string status_text => status.ToString();

        [JsonIgnore]
        public virtual BankAccount from_account { get; set; }
        [JsonIgnore]
        public virtual BankAccount to_account { get; set; }
        [JsonIgnore]
        public virtual ICollection<TransactionLogs> transaction_logs {  get; set; }
    }
}
