using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankTransactionSolution.Data.Migrations
{
    public partial class updatedatabase2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_TransactionLogs",
                table: "TransactionLogs");

            migrationBuilder.DropIndex(
                name: "IX_TransactionLogs_transaction_id",
                table: "TransactionLogs");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_TransactionLogs_transaction_id",
                table: "TransactionLogs",
                column: "transaction_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_TransactionLogs",
                table: "TransactionLogs",
                column: "transaction_id",
                principalTable: "Transaction",
                principalColumn: "id");
        }
    }
}
