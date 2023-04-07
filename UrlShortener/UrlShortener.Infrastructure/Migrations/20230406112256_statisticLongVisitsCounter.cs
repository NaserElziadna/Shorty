using Microsoft.EntityFrameworkCore.Migrations;

namespace UrlShortener.Infrastructure.Migrations
{
    public partial class statisticLongVisitsCounter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "clicks",
                table: "LinkStatistics");

            migrationBuilder.AddColumn<long>(
                name: "VisitsCount",
                table: "LinkStatistics",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VisitsCount",
                table: "LinkStatistics");

            migrationBuilder.AddColumn<int>(
                name: "clicks",
                table: "LinkStatistics",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
