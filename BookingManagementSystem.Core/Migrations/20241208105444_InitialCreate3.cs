using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BookingManagementSystem.Core.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Country_AreaInSqKm",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "Country_Capital",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "Country_Continent",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "Country_ContinentName",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "Country_CountryCode",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "Country_CountryName",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "Country_CurrencyCode",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "Country_East",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "Country_FipsCode",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "Country_IsoAlpha3",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "Country_IsoNumeric",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "Country_Languages",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "Country_North",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "Country_Population",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "Country_PostalCodeFormat",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "Country_South",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "Country_West",
                table: "Properties");

            migrationBuilder.RenameColumn(
                name: "Country_GeoNameId",
                table: "Properties",
                newName: "CountryGeoNameId");

            migrationBuilder.CreateTable(
                name: "CountryInfo",
                columns: table => new
                {
                    GeoNameId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Continent = table.Column<string>(type: "text", nullable: true),
                    Capital = table.Column<string>(type: "text", nullable: true),
                    Languages = table.Column<string>(type: "text", nullable: true),
                    South = table.Column<double>(type: "double precision", nullable: false),
                    IsoAlpha3 = table.Column<string>(type: "text", nullable: true),
                    North = table.Column<double>(type: "double precision", nullable: false),
                    FipsCode = table.Column<string>(type: "text", nullable: true),
                    Population = table.Column<string>(type: "text", nullable: true),
                    East = table.Column<double>(type: "double precision", nullable: false),
                    IsoNumeric = table.Column<string>(type: "text", nullable: true),
                    AreaInSqKm = table.Column<string>(type: "text", nullable: true),
                    CountryCode = table.Column<string>(type: "text", nullable: true),
                    West = table.Column<double>(type: "double precision", nullable: false),
                    CountryName = table.Column<string>(type: "text", nullable: true),
                    PostalCodeFormat = table.Column<string>(type: "text", nullable: true),
                    ContinentName = table.Column<string>(type: "text", nullable: true),
                    CurrencyCode = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryInfo", x => x.GeoNameId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Properties_CountryGeoNameId",
                table: "Properties",
                column: "CountryGeoNameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_CountryInfo_CountryGeoNameId",
                table: "Properties",
                column: "CountryGeoNameId",
                principalTable: "CountryInfo",
                principalColumn: "GeoNameId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Properties_CountryInfo_CountryGeoNameId",
                table: "Properties");

            migrationBuilder.DropTable(
                name: "CountryInfo");

            migrationBuilder.DropIndex(
                name: "IX_Properties_CountryGeoNameId",
                table: "Properties");

            migrationBuilder.RenameColumn(
                name: "CountryGeoNameId",
                table: "Properties",
                newName: "Country_GeoNameId");

            migrationBuilder.AddColumn<string>(
                name: "Country_AreaInSqKm",
                table: "Properties",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country_Capital",
                table: "Properties",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country_Continent",
                table: "Properties",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country_ContinentName",
                table: "Properties",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country_CountryCode",
                table: "Properties",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country_CountryName",
                table: "Properties",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country_CurrencyCode",
                table: "Properties",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Country_East",
                table: "Properties",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country_FipsCode",
                table: "Properties",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country_IsoAlpha3",
                table: "Properties",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country_IsoNumeric",
                table: "Properties",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country_Languages",
                table: "Properties",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Country_North",
                table: "Properties",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country_Population",
                table: "Properties",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country_PostalCodeFormat",
                table: "Properties",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Country_South",
                table: "Properties",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Country_West",
                table: "Properties",
                type: "double precision",
                nullable: true);
        }
    }
}
