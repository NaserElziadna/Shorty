using Microsoft.EntityFrameworkCore.Migrations;

namespace UrlShortener.Infrastructure.Migrations
{
    public partial class UserWithManyURL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_ShortUrls_ShortUrlId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ShortUrlId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ShortUrlId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "ShortUrls",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShortUrls_UserId",
                table: "ShortUrls",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShortUrls_AspNetUsers_UserId",
                table: "ShortUrls",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShortUrls_AspNetUsers_UserId",
                table: "ShortUrls");

            migrationBuilder.DropIndex(
                name: "IX_ShortUrls_UserId",
                table: "ShortUrls");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ShortUrls");

            migrationBuilder.AddColumn<int>(
                name: "ShortUrlId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ShortUrlId",
                table: "AspNetUsers",
                column: "ShortUrlId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_ShortUrls_ShortUrlId",
                table: "AspNetUsers",
                column: "ShortUrlId",
                principalTable: "ShortUrls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
