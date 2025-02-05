using BankTransactionSolution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTransactionSolution.Data.Abtract
{
    public interface IUnitOfWork
    {
        Repository<BankAccount> bank_account_repositoty { get; }
        Repository<User> user_repositoty { get; }
        Repository<TransactionLogs> transaction_logs_repositoty { get; }
        Repository<Transaction> transaction_repositoty { get; }

        Task BeginTransactionAsync();
        void CommitTransaction();
        void RollbackTransaction();
    }
}
