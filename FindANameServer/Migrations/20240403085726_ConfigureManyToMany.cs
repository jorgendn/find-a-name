using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FindANameServer.Migrations
{
    /// <inheritdoc />
    public partial class ConfigureManyToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_CandidateNames_CandidateNameId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CandidateNameId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CandidateNameId",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "CandidateNameUser",
                columns: table => new
                {
                    CandidateNameId = table.Column<int>(type: "int", nullable: false),
                    RejectedById = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateNameUser", x => new { x.CandidateNameId, x.RejectedById });
                    table.ForeignKey(
                        name: "FK_CandidateNameUser_AspNetUsers_RejectedById",
                        column: x => x.RejectedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CandidateNameUser_CandidateNames_CandidateNameId",
                        column: x => x.CandidateNameId,
                        principalTable: "CandidateNames",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CandidateNameUser_RejectedById",
                table: "CandidateNameUser",
                column: "RejectedById");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CandidateNameUser");

            migrationBuilder.AddColumn<int>(
                name: "CandidateNameId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CandidateNameId",
                table: "AspNetUsers",
                column: "CandidateNameId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_CandidateNames_CandidateNameId",
                table: "AspNetUsers",
                column: "CandidateNameId",
                principalTable: "CandidateNames",
                principalColumn: "Id");
        }
    }
}
