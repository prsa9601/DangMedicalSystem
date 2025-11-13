using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateUserDocumentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BirthCertificatePhoto",
                schema: "user",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "NationalCardPhoto",
                schema: "user",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "NationalityCode",
                schema: "user",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "user",
                table: "Users");

            migrationBuilder.CreateTable(
                name: "UserDocument",
                schema: "user",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NationalityCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthCertificatePhoto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NationalCardPhoto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDocument", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_UserDocument_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "user",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserDocument_UserId",
                schema: "user",
                table: "UserDocument",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserDocument",
                schema: "user");

            migrationBuilder.AddColumn<string>(
                name: "BirthCertificatePhoto",
                schema: "user",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NationalCardPhoto",
                schema: "user",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NationalityCode",
                schema: "user",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                schema: "user",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
