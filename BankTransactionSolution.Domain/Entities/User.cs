﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BankTransactionSolution.Domain.Entities
{
    public class User : BaseEntity
    {
        public User()
        {
            
        }
        public User(string user_name, string full_name, string email, string phone, string password)
        {
            this.user_name = user_name;
            this.full_name = full_name;
            this.email = email;
            this.phone = phone;
            this.password = password;
            bank_accounts = new HashSet<BankAccount>();
        }
        public string user_name { get; set; }   
        public string full_name { get; set; }   
        public string email { get; set; }   
        public string phone { get; set; }
        public string password { get; set; }
     
        public virtual ICollection<BankAccount> bank_accounts { get; set; }
    }
}
