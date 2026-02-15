using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OneClickSocialMedia.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUserIdToTwitterOAuthTokens : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "TwitterOAuthTokens",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_TwitterOAuthTokens_UserId",
                table: "TwitterOAuthTokens",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TwitterOAuthTokens_AspNetUsers_UserId",
                table: "TwitterOAuthTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TwitterOAuthTokens_AspNetUsers_UserId",
                table: "TwitterOAuthTokens");

            migrationBuilder.DropIndex(
                name: "IX_TwitterOAuthTokens_UserId",
                table: "TwitterOAuthTokens");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TwitterOAuthTokens");
        }
    }
}
