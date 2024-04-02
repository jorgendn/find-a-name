using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FindANameServer.Migrations
{
    /// <inheritdoc />
    public partial class BuildingUpModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CandidateNameId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CandidateNames",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateNames", x => x.Id);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_CandidateNames_CandidateNameId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "CandidateNames");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CandidateNameId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CandidateNameId",
                table: "AspNetUsers");
        }
    }
}
