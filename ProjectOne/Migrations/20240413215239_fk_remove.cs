using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectFuse.Migrations
{
    /// <inheritdoc />
    public partial class fk_remove : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ForumMessage_ProjectOneUser_UserId",
                table: "ForumMessage");

            migrationBuilder.DropTable(
                name: "ProjectOneUser");

            migrationBuilder.DropIndex(
                name: "IX_ForumMessage_UserId",
                table: "ForumMessage");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProjectOneUser",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "text", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "text", nullable: true),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    UserName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectOneUser", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ForumMessage_UserId",
                table: "ForumMessage",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ForumMessage_ProjectOneUser_UserId",
                table: "ForumMessage",
                column: "UserId",
                principalTable: "ProjectOneUser",
                principalColumn: "Id");
        }
    }
}
