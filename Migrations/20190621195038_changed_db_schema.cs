using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Apka2.Migrations
{
    public partial class changed_db_schema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Votes_VoteTypes_VoteTypeId",
                table: "Votes");

            migrationBuilder.DropTable(
                name: "LawUser");

            migrationBuilder.DropTable(
                name: "VoteTypes");

            migrationBuilder.RenameColumn(
                name: "VoteTypeId",
                table: "Votes",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Votes_VoteTypeId",
                table: "Votes",
                newName: "IX_Votes_UserId");

            migrationBuilder.AddColumn<int>(
                name: "VoteType",
                table: "Votes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Votes_Users_UserId",
                table: "Votes",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Votes_Users_UserId",
                table: "Votes");

            migrationBuilder.DropColumn(
                name: "VoteType",
                table: "Votes");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Votes",
                newName: "VoteTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Votes_UserId",
                table: "Votes",
                newName: "IX_Votes_VoteTypeId");

            migrationBuilder.CreateTable(
                name: "LawUser",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LawId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LawUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LawUser_Laws_LawId",
                        column: x => x.LawId,
                        principalTable: "Laws",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LawUser_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VoteTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoteTypes", x => x.Id);
                });

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
                name: "IX_LawUser_LawId",
                table: "LawUser",
                column: "LawId");

            migrationBuilder.CreateIndex(
                name: "IX_LawUser_UserId",
                table: "LawUser",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Votes_VoteTypes_VoteTypeId",
                table: "Votes",
                column: "VoteTypeId",
                principalTable: "VoteTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
