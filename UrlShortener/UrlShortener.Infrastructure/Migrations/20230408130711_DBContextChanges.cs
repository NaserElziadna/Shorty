using Microsoft.EntityFrameworkCore.Migrations;

namespace UrlShortener.Infrastructure.Migrations
{
    public partial class DBContextChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShortUrls_ShortUrlHash_ShortUrlHashId",
                table: "ShortUrls");

            migrationBuilder.DropForeignKey(
                name: "FK_StatisticLocation_LinkStatistics_LinkStatisticsId",
                table: "StatisticLocation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StatisticLocation",
                table: "StatisticLocation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShortUrlHash",
                table: "ShortUrlHash");

            migrationBuilder.RenameTable(
                name: "StatisticLocation",
                newName: "StatisticLocations");

            migrationBuilder.RenameTable(
                name: "ShortUrlHash",
                newName: "ShortUrlHashes");

            migrationBuilder.RenameIndex(
                name: "IX_StatisticLocation_LinkStatisticsId",
                table: "StatisticLocations",
                newName: "IX_StatisticLocations_LinkStatisticsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StatisticLocations",
                table: "StatisticLocations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShortUrlHashes",
                table: "ShortUrlHashes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ShortUrls_ShortUrlHashes_ShortUrlHashId",
                table: "ShortUrls",
                column: "ShortUrlHashId",
                principalTable: "ShortUrlHashes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StatisticLocations_LinkStatistics_LinkStatisticsId",
                table: "StatisticLocations",
                column: "LinkStatisticsId",
                principalTable: "LinkStatistics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShortUrls_ShortUrlHashes_ShortUrlHashId",
                table: "ShortUrls");

            migrationBuilder.DropForeignKey(
                name: "FK_StatisticLocations_LinkStatistics_LinkStatisticsId",
                table: "StatisticLocations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StatisticLocations",
                table: "StatisticLocations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShortUrlHashes",
                table: "ShortUrlHashes");

            migrationBuilder.RenameTable(
                name: "StatisticLocations",
                newName: "StatisticLocation");

            migrationBuilder.RenameTable(
                name: "ShortUrlHashes",
                newName: "ShortUrlHash");

            migrationBuilder.RenameIndex(
                name: "IX_StatisticLocations_LinkStatisticsId",
                table: "StatisticLocation",
                newName: "IX_StatisticLocation_LinkStatisticsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StatisticLocation",
                table: "StatisticLocation",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShortUrlHash",
                table: "ShortUrlHash",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ShortUrls_ShortUrlHash_ShortUrlHashId",
                table: "ShortUrls",
                column: "ShortUrlHashId",
                principalTable: "ShortUrlHash",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StatisticLocation_LinkStatistics_LinkStatisticsId",
                table: "StatisticLocation",
                column: "LinkStatisticsId",
                principalTable: "LinkStatistics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
