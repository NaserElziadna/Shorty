using Microsoft.EntityFrameworkCore.Migrations;

namespace UrlShortener.Infrastructure.Migrations
{
    public partial class addS : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LinkStatisticsId",
                table: "ShortUrls",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "LinkStatistics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    clicks = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinkStatistics", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShortUrls_LinkStatisticsId",
                table: "ShortUrls",
                column: "LinkStatisticsId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShortUrls_LinkStatistics_LinkStatisticsId",
                table: "ShortUrls",
                column: "LinkStatisticsId",
                principalTable: "LinkStatistics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShortUrls_LinkStatistics_LinkStatisticsId",
                table: "ShortUrls");

            migrationBuilder.DropTable(
                name: "LinkStatistics");

            migrationBuilder.DropIndex(
                name: "IX_ShortUrls_LinkStatisticsId",
                table: "ShortUrls");

            migrationBuilder.DropColumn(
                name: "LinkStatisticsId",
                table: "ShortUrls");
        }
    }
}
