using BankTransactionSolution.Domain.Entities;
using BankTransactionSolution.Domain.Enum;
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

                // 🟢 Seed Data cho bảng User
                entity.HasData(
                    new User { id = 1, user_name = "admin", full_name = "Admin User", email = "admin@example.com", phone = "123456789", is_deleted = false, date_created = DateTime.UtcNow, date_updated = DateTime.UtcNow },
                    new User { id = 2, user_name = "john_doe", full_name = "John Doe", email = "john@example.com", phone = "987654321", is_deleted = false, date_created = DateTime.UtcNow, date_updated = DateTime.UtcNow }
                );
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
                entity.Property(e => e.balance).IsRequired();
                entity.Property(e => e.currency).IsRequired();

                entity.HasOne(d => d.user).WithMany(p => p.bank_accounts)
                    .HasForeignKey(d => d.user_id)
                    .HasConstraintName("FK_User_BankAccount")
                    .OnDelete(DeleteBehavior.NoAction);

                // 🟢 Seed Data cho bảng BankAccount
                entity.HasData(
                    new BankAccount { id = 1, user_id = 1, balance = 10000000, currency = "USD", is_deleted = false, date_created = DateTime.UtcNow, date_updated = DateTime.UtcNow },
                    new BankAccount { id = 2, user_id = 2, balance = 10000000, currency = "EUR", is_deleted = false, date_created = DateTime.UtcNow, date_updated = DateTime.UtcNow }
                );
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

                entity.HasOne(d => d.from_account).WithMany(p => p.from_transactions)
                    .HasForeignKey(d => d.from_account_id)
                    .HasConstraintName("FK_FormAccount_Transaction")
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(d => d.to_account).WithMany(p => p.to_transactions)
                    .HasForeignKey(d => d.to_account_id)
                    .HasConstraintName("FK_ToAccount_Transaction")
                    .OnDelete(DeleteBehavior.NoAction);

                // 🟢 Seed Data cho bảng Transaction
                entity.HasData(
                    new Transaction { id = 1, from_account_id = 1, to_account_id = 2, amount = 100000, currency = "USD", status = TransactionStatus.success, is_deleted = false, date_created = DateTime.UtcNow, date_updated = DateTime.UtcNow },
                    new Transaction { id = 2, from_account_id = 2, to_account_id = 1, amount = 100000, currency = "EUR", status = TransactionStatus.success, is_deleted = false, date_created = DateTime.UtcNow, date_updated = DateTime.UtcNow }
                );
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

                // 🟢 Seed Data cho bảng TransactionLogs
                entity.HasData(
                    new TransactionLogs { id = 1, transaction_id = 1, status = TransactionStatus.success, from_account_json = "{\"id\":1, \"balance\":900.00}", to_account_json = "{\"id\":2, \"balance\":5100.00}", is_deleted = false, date_created = DateTime.UtcNow, date_updated = DateTime.UtcNow }
                );
            });
        }

    }
}
