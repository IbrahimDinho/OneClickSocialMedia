using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OneClickSocialMedia.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddFacebookOuathTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FacebookOAuthTokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    UserAccessToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacebookOAuthTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FacebookOAuthTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FacebookOAuthTokens_UserId",
                table: "FacebookOAuthTokens",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FacebookOAuthTokens");
        }
    }
}
