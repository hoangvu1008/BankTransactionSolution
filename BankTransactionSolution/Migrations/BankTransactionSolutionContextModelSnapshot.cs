﻿// <auto-generated />
using System;
using BankTransactionSolution.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BankTransactionSolution.Data.Migrations
{
    [DbContext(typeof(BankTransactionSolutionContext))]
    partial class BankTransactionSolutionContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.36")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("BankTransactionSolution.Domain.Entities.BankAccount", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<string>("account_number")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("balance")
                        .HasColumnType("float");

                    b.Property<string>("currency")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("date_created")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("date_updated")
                        .HasColumnType("datetime2");

                    b.Property<bool>("is_deleted")
                        .HasColumnType("bit");

                    b.Property<int>("user_id")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("user_id");

                    b.ToTable("BankAccount", (string)null);
                });

            modelBuilder.Entity("BankTransactionSolution.Domain.Entities.Transaction", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<double>("amount")
                        .HasColumnType("float");

                    b.Property<string>("currency")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("date_created")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("date_updated")
                        .HasColumnType("datetime2");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("from_account_id")
                        .HasColumnType("int");

                    b.Property<bool>("is_deleted")
                        .HasColumnType("bit");

                    b.Property<int>("status")
                        .HasColumnType("int");

                    b.Property<int>("to_account_id")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("from_account_id");

                    b.HasIndex("to_account_id");

                    b.ToTable("Transaction", (string)null);
                });

            modelBuilder.Entity("BankTransactionSolution.Domain.Entities.TransactionLogs", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<DateTime>("date_created")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("date_updated")
                        .HasColumnType("datetime2");

                    b.Property<string>("from_account_json")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("is_deleted")
                        .HasColumnType("bit");

                    b.Property<int>("status")
                        .HasColumnType("int");

                    b.Property<string>("to_account_json")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("transaction_id")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("transaction_id");

                    b.ToTable("TransactionLogs", (string)null);
                });

            modelBuilder.Entity("BankTransactionSolution.Domain.Entities.User", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<DateTime>("date_created")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("date_updated")
                        .HasColumnType("datetime2");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("full_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("is_deleted")
                        .HasColumnType("bit");

                    b.Property<string>("phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("user_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Member", (string)null);
                });

            modelBuilder.Entity("BankTransactionSolution.Domain.Entities.BankAccount", b =>
                {
                    b.HasOne("BankTransactionSolution.Domain.Entities.User", "user")
                        .WithMany("bank_accounts")
                        .HasForeignKey("user_id")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("FK_User_BankAccount");

                    b.Navigation("user");
                });

            modelBuilder.Entity("BankTransactionSolution.Domain.Entities.Transaction", b =>
                {
                    b.HasOne("BankTransactionSolution.Domain.Entities.BankAccount", "from_account")
                        .WithMany("from_transactions")
                        .HasForeignKey("from_account_id")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("FK_FormAccount_Transaction");

                    b.HasOne("BankTransactionSolution.Domain.Entities.BankAccount", "to_account")
                        .WithMany("to_transactions")
                        .HasForeignKey("to_account_id")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("FK_ToAccount_Transaction");

                    b.Navigation("from_account");

                    b.Navigation("to_account");
                });

            modelBuilder.Entity("BankTransactionSolution.Domain.Entities.TransactionLogs", b =>
                {
                    b.HasOne("BankTransactionSolution.Domain.Entities.Transaction", "transaction")
                        .WithMany("transaction_logs")
                        .HasForeignKey("transaction_id")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("FK_Transaction_TransactionLogs");

                    b.Navigation("transaction");
                });

            modelBuilder.Entity("BankTransactionSolution.Domain.Entities.BankAccount", b =>
                {
                    b.Navigation("from_transactions");

                    b.Navigation("to_transactions");
                });

            modelBuilder.Entity("BankTransactionSolution.Domain.Entities.Transaction", b =>
                {
                    b.Navigation("transaction_logs");
                });

            modelBuilder.Entity("BankTransactionSolution.Domain.Entities.User", b =>
                {
                    b.Navigation("bank_accounts");
                });
#pragma warning restore 612, 618
        }
    }
}
