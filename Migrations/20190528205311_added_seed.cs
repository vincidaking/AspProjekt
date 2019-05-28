using Microsoft.EntityFrameworkCore.Migrations;

namespace Apka2.Migrations
{
    public partial class added_seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "FirstName", "LastName", "Password", "Role", "Token", "Username" },
                values: new object[] { 1, "SA", "SA", "123456", "Admin", null, "Admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
