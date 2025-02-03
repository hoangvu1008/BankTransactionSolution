using BankTransactionSolution.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTransactionSolution.Data
{
    public class BankTransactionSolutionContext: DbContext
    {
        public BankTransactionSolutionContext()
        {

        }
        public BankTransactionSolutionContext(DbContextOptions<BankTransactionSolutionContext> options) : base(options)
        {

        }

        public virtual DbSet<User> user { get; set; } = null!;
        public virtual DbSet<AuditLogs> audit_logs { get; set; } = null!;
        public virtual DbSet<Transaction> transation { get; set; } = null!;
        public virtual DbSet<TransactionLogs> transaction_logs { get; set; } = null!;
        public virtual DbSet<BankAccount> bank_account { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=tcp:somofarmtask-db.database.windows.net,1433;Initial Catalog=SomoTaskManagementDb;Persist Security Info=False;User ID=minhanh01;Password=somo123@;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }
    }
}
