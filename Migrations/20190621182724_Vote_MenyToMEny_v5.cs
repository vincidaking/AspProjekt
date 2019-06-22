using Microsoft.EntityFrameworkCore.Migrations;

namespace Apka2.Migrations
{
    public partial class Vote_MenyToMEny_v5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VoteType_Laws_LawId",
                table: "VoteType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VoteType",
                table: "VoteType");

            migrationBuilder.DropIndex(
                name: "IX_VoteType_LawId",
                table: "VoteType");

            migrationBuilder.DropColumn(
                name: "LawId",
                table: "VoteType");

            migrationBuilder.RenameTable(
                name: "VoteType",
                newName: "VoteTypes");

            migrationBuilder.AddColumn<int>(
                name: "VoteTypeId",
                table: "LawUser",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_VoteTypes",
                table: "VoteTypes",
                column: "Id");

            migrationBuilder.InsertData(
                table: "VoteTypes",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Za" });

            migrationBuilder.InsertData(
                table: "VoteTypes",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Przeciw" });

            migrationBuilder.InsertData(
                table: "VoteTypes",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3, "Wstrzymanie" });

            migrationBuilder.CreateIndex(
                name: "IX_LawUser_VoteTypeId",
                table: "LawUser",
                column: "VoteTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_LawUser_VoteTypes_VoteTypeId",
                table: "LawUser",
                column: "VoteTypeId",
                principalTable: "VoteTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LawUser_VoteTypes_VoteTypeId",
                table: "LawUser");

            migrationBuilder.DropIndex(
                name: "IX_LawUser_VoteTypeId",
                table: "LawUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VoteTypes",
                table: "VoteTypes");

            migrationBuilder.DeleteData(
                table: "VoteTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "VoteTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "VoteTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "VoteTypeId",
                table: "LawUser");

            migrationBuilder.RenameTable(
                name: "VoteTypes",
                newName: "VoteType");

            migrationBuilder.AddColumn<int>(
                name: "LawId",
                table: "VoteType",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_VoteType",
                table: "VoteType",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_VoteType_LawId",
                table: "VoteType",
                column: "LawId");

            migrationBuilder.AddForeignKey(
                name: "FK_VoteType_Laws_LawId",
                table: "VoteType",
                column: "LawId",
                principalTable: "Laws",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
