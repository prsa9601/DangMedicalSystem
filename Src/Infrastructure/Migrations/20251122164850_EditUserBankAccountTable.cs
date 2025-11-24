using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EditUserBankAccountTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpirationDateMonth",
                schema: "user",
                table: "BankAccounts");

            migrationBuilder.DropColumn(
                name: "ExpirationDateYear",
                schema: "user",
                table: "BankAccounts");

            migrationBuilder.DropColumn(
                name: "FirstName",
                schema: "user",
                table: "BankAccounts");

            migrationBuilder.RenameColumn(
                name: "LastName",
                schema: "user",
                table: "BankAccounts",
                newName: "FullName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FullName",
                schema: "user",
                table: "BankAccounts",
                newName: "LastName");

            migrationBuilder.AddColumn<int>(
                name: "ExpirationDateMonth",
                schema: "user",
                table: "BankAccounts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ExpirationDateYear",
                schema: "user",
                table: "BankAccounts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                schema: "user",
                table: "BankAccounts",
                type: "nvarchar(70)",
                maxLength: 70,
                nullable: false,
                defaultValue: "");
        }
    }
}
