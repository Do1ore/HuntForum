using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectFuse.Migrations
{
    /// <inheritdoc />
    public partial class user_fk_removed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Topics_ProjectOneUser_UserId",
                table: "Topics");

            migrationBuilder.DropIndex(
                name: "IX_Topics_UserId",
                table: "Topics");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Topics_UserId",
                table: "Topics",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Topics_ProjectOneUser_UserId",
                table: "Topics",
                column: "UserId",
                principalTable: "ProjectOneUser",
                principalColumn: "Id");
        }
    }
}
