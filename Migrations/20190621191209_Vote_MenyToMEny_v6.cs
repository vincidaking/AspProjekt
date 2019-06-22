using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Apka2.Migrations
{
    public partial class Vote_MenyToMEny_v6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LawUser_VoteTypes_VoteTypeId",
                table: "LawUser");

            migrationBuilder.DropIndex(
                name: "IX_LawUser_VoteTypeId",
                table: "LawUser");

            migrationBuilder.DropColumn(
                name: "VoteTypeId",
                table: "LawUser");

            migrationBuilder.CreateTable(
                name: "Votes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    VoteTypeId = table.Column<int>(nullable: true),
                    LawId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Votes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Votes_Laws_LawId",
                        column: x => x.LawId,
                        principalTable: "Laws",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Votes_VoteTypes_VoteTypeId",
                        column: x => x.VoteTypeId,
                        principalTable: "VoteTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Votes_LawId",
                table: "Votes",
                column: "LawId");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_VoteTypeId",
                table: "Votes",
                column: "VoteTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Votes");

            migrationBuilder.AddColumn<int>(
                name: "VoteTypeId",
                table: "LawUser",
                nullable: true);

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
    }
}
