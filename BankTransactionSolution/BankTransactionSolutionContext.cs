using BankTransactionSolution.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Metrics;
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
        public virtual DbSet<Transaction> transation { get; set; } = null!;
        public virtual DbSet<TransactionLogs> transaction_logs { get; set; } = null!;
        public virtual DbSet<BankAccount> bank_account { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-CJORDSC\\SQLEXPRESS;Database=BankTransactionDB;User Id=sa;Password=sa123;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Member");
                entity.HasKey(e => e.id);
                entity.Property(e => e.id).ValueGeneratedOnAdd();
                entity.Property(e => e.is_deleted);
                entity.Property(e => e.date_created);
                entity.Property(e => e.date_updated);
                entity.Property(e => e.user_name).IsRequired();
                entity.Property(e => e.full_name).IsRequired();
                entity.Property(e => e.email).IsRequired();
                entity.Property(e => e.phone).IsRequired();
            });

            modelBuilder.Entity<BankAccount>(entity =>
            {
                entity.ToTable("BankAccount");
                entity.HasKey(e => e.id);
                entity.Property(e => e.id).ValueGeneratedOnAdd();
                entity.Property(e => e.is_deleted);
                entity.Property(e => e.date_created);
                entity.Property(e => e.date_updated);
                entity.Property(e => e.user_id).IsRequired();
                entity.Property(e => e.account_number).IsRequired();
                entity.Property(e => e.balance).IsRequired();
                entity.Property(e => e.currency).IsRequired();
                entity.Property(e => e.currency).IsRequired();

                entity.HasOne(d => d.user).WithMany(p => p.bank_accounts).HasForeignKey(d => d.user_id).HasConstraintName("FK_User_BankAccount").OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.ToTable("Transaction");
                entity.HasKey(e => e.id);
                entity.Property(e => e.id).ValueGeneratedOnAdd();
                entity.Property(e => e.is_deleted);
                entity.Property(e => e.date_created);
                entity.Property(e => e.date_updated);
                entity.Property(e => e.from_account_id).IsRequired();
                entity.Property(e => e.to_account_id).IsRequired();
                entity.Property(e => e.amount).IsRequired();
                entity.Property(e => e.currency).IsRequired();
                entity.Property(e => e.status).IsRequired();

                entity.HasOne(d => d.from_account).WithMany(p => p.from_transactions).HasForeignKey(d => d.from_account_id).HasConstraintName("FK_FormAccount_Transaction").OnDelete(DeleteBehavior.NoAction);
                entity.HasOne(d => d.to_account).WithMany(p => p.to_transactions).HasForeignKey(d => d.to_account_id).HasConstraintName("FK_ToAccount_Transaction").OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<TransactionLogs>(entity =>
            {
                entity.ToTable("TransactionLogs");
                entity.HasKey(e => e.id);
                entity.Property(e => e.id).ValueGeneratedOnAdd();
                entity.Property(e => e.is_deleted);
                entity.Property(e => e.date_created);
                entity.Property(e => e.date_updated);
                entity.Property(e => e.transaction_id).IsRequired();
                entity.Property(e => e.status).IsRequired();
                entity.Property(e => e.from_account_json).IsRequired();
                entity.Property(e => e.to_account_json).IsRequired();

                entity.HasOne(d => d.transaction).WithMany(p => p.transaction_logs).HasForeignKey(d => d.transaction_id).HasConstraintName("FK_Transaction_TransactionLogs").OnDelete(DeleteBehavior.NoAction);
            });
        }
    }
}
