using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Microsoft.DSX.ProjectTemplate.Data.Migrations
{
    public partial class SeedLibraryValues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Libraries",
                columns: new[] { "Id", "CreatedDate", "Name", "UpdatedDate", "Address_LocationAddressLine1", "Address_LocationAddressLine2", "Address_LocationCity", "Address_LocationCountry", "Address_LocationStateProvince", "Address_LocationZipCode" },
                values: new object[] { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bellevue Library", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "1111 110th Ave NE", null, "Bellevue", "US", "WA", "98004" });

            migrationBuilder.InsertData(
                table: "Libraries",
                columns: new[] { "Id", "CreatedDate", "Name", "UpdatedDate", "Address_LocationAddressLine1", "Address_LocationAddressLine2", "Address_LocationCity", "Address_LocationCountry", "Address_LocationStateProvince", "Address_LocationZipCode" },
                values: new object[] { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Newcastle Library", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "12901 Newcastle Way", null, "Newcastle", "US", "WA", "98056" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Libraries",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Libraries",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
