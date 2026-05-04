using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OneClickSocialMedia.Data.Migrations
{
    /// <inheritdoc />
    public partial class PageId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PageId",
                table: "FacebookOAuthTokens",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PageId",
                table: "FacebookOAuthTokens");
        }
    }
}
