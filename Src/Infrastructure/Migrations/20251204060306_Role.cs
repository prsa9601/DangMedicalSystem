using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Role : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RolePermission_Roles_RoleId",
                table: "RolePermission");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RolePermission",
                table: "RolePermission");

            migrationBuilder.EnsureSchema(
                name: "role");

            migrationBuilder.RenameTable(
                name: "Roles",
                newName: "Roles",
                newSchema: "role");

            migrationBuilder.RenameTable(
                name: "RolePermission",
                newName: "RolePermissions",
                newSchema: "role");

            migrationBuilder.RenameIndex(
                name: "IX_RolePermission_RoleId",
                schema: "role",
                table: "RolePermissions",
                newName: "IX_RolePermissions_RoleId");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                schema: "role",
                table: "Roles",
                type: "nvarchar(2)",
                maxLength: 2,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RolePermissions",
                schema: "role",
                table: "RolePermissions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermissions_Roles_RoleId",
                schema: "role",
                table: "RolePermissions",
                column: "RoleId",
                principalSchema: "role",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RolePermissions_Roles_RoleId",
                schema: "role",
                table: "RolePermissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RolePermissions",
                schema: "role",
                table: "RolePermissions");

            migrationBuilder.RenameTable(
                name: "Roles",
                schema: "role",
                newName: "Roles");

            migrationBuilder.RenameTable(
                name: "RolePermissions",
                schema: "role",
                newName: "RolePermission");

            migrationBuilder.RenameIndex(
                name: "IX_RolePermissions_RoleId",
                table: "RolePermission",
                newName: "IX_RolePermission_RoleId");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Roles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(2)",
                oldMaxLength: 2,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RolePermission",
                table: "RolePermission",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermission_Roles_RoleId",
                table: "RolePermission",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
