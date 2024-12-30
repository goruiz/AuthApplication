using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthApplication.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Address", "BirthDate", "Email", "FirstName", "LastName" },
                values: new object[] { "Address", new DateTime(1998, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "lameyiy197@gholar.com", "FirstName", "LastName" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Address", "BirthDate", "Email", "FirstName", "LastName" },
                values: new object[] { "Address", new DateTime(1998, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "lameyiy197@gholar.com", "FirstName", "LastName" });
        }
    }
}
