using BankTransactionSolution.Data.Abtract;
using BankTransactionSolution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTransactionSolution.Data
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly BankTransactionSolutionContext _context;
        private bool disposedValue;
        public UnitOfWork(BankTransactionSolutionContext context)
        {
            _context = context;
        }

        Repository<BankAccount> repositoryBankAccount;
        Repository<User> repositoryUser;
        Repository<Transaction> repositoryTransaction;
        Repository<TransactionLogs> repositoryTransactionLogs;

        public Repository<BankAccount> bank_account_repositoty { get { return repositoryBankAccount ??= new Repository<BankAccount>(_context); } }
        public Repository<User> user_repositoty { get { return repositoryUser ??= new Repository<User>(_context); } }
        public Repository<Transaction> transaction_repositoty { get { return repositoryTransaction ??= new Repository<Transaction>(_context); } }
        public Repository<TransactionLogs> transaction_logs_repositoty { get { return repositoryTransactionLogs ??= new Repository<TransactionLogs>(_context); } }

        public async Task BeginTransactionAsync()
        {
            if (_context.Database.CurrentTransaction == null)
            {
                await _context.Database.BeginTransactionAsync();
            }
        }

        public void CommitTransaction()
        {
            if (_context.Database.CurrentTransaction != null)
            {
                _context.Database.CurrentTransaction.Commit();
            }
        }

        public void RollbackTransaction()
        {
            if (_context.Database.CurrentTransaction != null)
            {
                _context.Database.CurrentTransaction.Rollback();
            }
        }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }

        public virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
