using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingManagementSystem.Core.Migrations
{
    /// <inheritdoc />
    public partial class ScaffoldedMigration4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_BadReports_HandledByAdminId",
                table: "BadReports",
                column: "HandledByAdminId");

            migrationBuilder.AddForeignKey(
                name: "FK_BadReports_Users_HandledByAdminId",
                table: "BadReports",
                column: "HandledByAdminId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BadReports_Users_HandledByAdminId",
                table: "BadReports");

            migrationBuilder.DropIndex(
                name: "IX_BadReports_HandledByAdminId",
                table: "BadReports");
        }
    }
}
