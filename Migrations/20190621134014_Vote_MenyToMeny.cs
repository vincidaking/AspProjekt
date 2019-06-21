using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Apka2.Migrations
{
    public partial class Vote_MenyToMeny : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "VoteType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    LawId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoteType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VoteType_Laws_LawId",
                        column: x => x.LawId,
                        principalTable: "Laws",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Password", "Username" },
                values: new object[] { "admin", "admin" });

            migrationBuilder.CreateIndex(
                name: "IX_LawUser_LawId",
                table: "LawUser",
                column: "LawId");

            migrationBuilder.CreateIndex(
                name: "IX_LawUser_UserId",
                table: "LawUser",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_VoteType_LawId",
                table: "VoteType",
                column: "LawId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LawUser");

            migrationBuilder.DropTable(
                name: "VoteType");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Password", "Username" },
                values: new object[] { "123456", "Admin" });
        }
    }
}
