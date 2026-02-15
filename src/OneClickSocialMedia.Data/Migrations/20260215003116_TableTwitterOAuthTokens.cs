using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OneClickSocialMedia.Data.Migrations
{
    /// <inheritdoc />
    public partial class TableTwitterOAuthTokens : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TwitterOAuthTokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TwitterApiKey = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    TwitterApiSecret = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    TwitterAccessToken = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    TwitterAccessTokenSecret = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TwitterOAuthTokens", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TwitterOAuthTokens");
        }
    }
}
