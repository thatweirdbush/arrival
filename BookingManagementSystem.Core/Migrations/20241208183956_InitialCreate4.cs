using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingManagementSystem.Core.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Properties_CountryInfo_CountryGeoNameId",
                table: "Properties");

            migrationBuilder.RenameColumn(
                name: "CountryGeoNameId",
                table: "Properties",
                newName: "CountryId");

            migrationBuilder.RenameIndex(
                name: "IX_Properties_CountryGeoNameId",
                table: "Properties",
                newName: "IX_Properties_CountryId");

            migrationBuilder.RenameColumn(
                name: "GeoNameId",
                table: "CountryInfo",
                newName: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_CountryInfo_CountryId",
                table: "Properties",
                column: "CountryId",
                principalTable: "CountryInfo",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Properties_CountryInfo_CountryId",
                table: "Properties");

            migrationBuilder.RenameColumn(
                name: "CountryId",
                table: "Properties",
                newName: "CountryGeoNameId");

            migrationBuilder.RenameIndex(
                name: "IX_Properties_CountryId",
                table: "Properties",
                newName: "IX_Properties_CountryGeoNameId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "CountryInfo",
                newName: "GeoNameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_CountryInfo_CountryGeoNameId",
                table: "Properties",
                column: "CountryGeoNameId",
                principalTable: "CountryInfo",
                principalColumn: "GeoNameId");
        }
    }
}
