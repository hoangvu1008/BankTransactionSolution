﻿using BankTransactionSolution.Domain.Enum;
using Newtonsoft.Json;
namespace BankTransactionSolution.Domain.Entities
{
    public class TransactionLogs: BaseEntity
    {
        public TransactionLogs()
        {
            
        }
        public TransactionLogs(int transaction_id, TransactionStatus status, string from_account_json, string to_account_json)
        {
            this.transaction_id = transaction_id;
            this.status = status;
            this.from_account_json = from_account_json;
            this.to_account_json = to_account_json;
        }
        public int transaction_id { get; set; }
        public TransactionStatus status { get; set; }   
        public string status_text => status.ToString(); 
        public string from_account_json { get; set; }   
        public string to_account_json { get;set; }

    }
}
