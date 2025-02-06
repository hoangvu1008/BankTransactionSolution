using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankTransactionSolution.Data.Migrations
{
    public partial class updatedatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "account_number",
                table: "BankAccount",
                newName: "bank_name");

            migrationBuilder.AddColumn<string>(
                name: "password",
                table: "Member",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "bank_account",
                table: "BankAccount",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "bank_code",
                table: "BankAccount",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "password",
                table: "Member");

            migrationBuilder.DropColumn(
                name: "bank_account",
                table: "BankAccount");

            migrationBuilder.DropColumn(
                name: "bank_code",
                table: "BankAccount");

            migrationBuilder.RenameColumn(
                name: "bank_name",
                table: "BankAccount",
                newName: "account_number");
        }
    }
}
