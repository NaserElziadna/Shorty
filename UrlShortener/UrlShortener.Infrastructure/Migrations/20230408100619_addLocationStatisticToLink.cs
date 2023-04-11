using Microsoft.EntityFrameworkCore.Migrations;

namespace UrlShortener.Infrastructure.Migrations
{
    public partial class addLocationStatisticToLink : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StatisticLocation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    dataDecoded = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LinkStatisticsId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatisticLocation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StatisticLocation_LinkStatistics_LinkStatisticsId",
                        column: x => x.LinkStatisticsId,
                        principalTable: "LinkStatistics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StatisticLocation_LinkStatisticsId",
                table: "StatisticLocation",
                column: "LinkStatisticsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StatisticLocation");
        }
    }
}
