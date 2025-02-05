using BankTransactionSolution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTransactionSolution.Domain.Model
{
    public class UserModel
    {
        public User user {  get; set; } 
        public List<BankAccount> bank_accounts { get; set; }
    }
}
