using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assessment.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DateDeleted", "Username" },
                values: new object[] { new Guid("43baf7ac-2ba3-46f8-acf4-10b0795f34d1"), null, "Fin Coder" });

            migrationBuilder.InsertData(
                table: "Calls",
                columns: new[] { "Id", "CallingUserId", "DateCallStarted", "DateDeleted" },
                values: new object[] { new Guid("f6fd57fa-c0fb-4c2d-b94c-2e7de08c0e89"), new Guid("43baf7ac-2ba3-46f8-acf4-10b0795f34d1"), new DateTimeOffset(new DateTime(2024, 3, 22, 8, 29, 35, 219, DateTimeKind.Unspecified).AddTicks(7185), new TimeSpan(0, 0, 0, 0, 0)), null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Calls",
                keyColumn: "Id",
                keyValue: new Guid("f6fd57fa-c0fb-4c2d-b94c-2e7de08c0e89"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("43baf7ac-2ba3-46f8-acf4-10b0795f34d1"));
        }
    }
}
