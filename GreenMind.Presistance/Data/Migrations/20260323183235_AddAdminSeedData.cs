using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GreenMind.Presistance.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddAdminSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "Id", "CreatedDate", "Email", "Name", "Password" },
                values: new object[] { 1, new DateTime(2026, 3, 23, 18, 32, 34, 365, DateTimeKind.Utc).AddTicks(91), "admin01@gmail.com", "SalmaAdmin", "AQAAAAEAACcQAAAAEBy9Mjk9Z3lR5jL2PqX9H3L0T4M5Z6X7Y8W9V0U1A2B3C4D5E6F7G8H9I0J==" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 3, 23, 18, 32, 34, 364, DateTimeKind.Utc).AddTicks(7504));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 3, 23, 18, 32, 34, 364, DateTimeKind.Utc).AddTicks(8786));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 3, 23, 18, 32, 34, 364, DateTimeKind.Utc).AddTicks(8789));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2026, 3, 23, 18, 32, 34, 364, DateTimeKind.Utc).AddTicks(8791));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2026, 3, 23, 18, 32, 34, 364, DateTimeKind.Utc).AddTicks(8792));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2026, 3, 23, 18, 32, 34, 364, DateTimeKind.Utc).AddTicks(8793));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2026, 3, 23, 18, 32, 34, 364, DateTimeKind.Utc).AddTicks(8795));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2026, 3, 23, 18, 32, 34, 364, DateTimeKind.Utc).AddTicks(8796));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedDate",
                value: new DateTime(2026, 3, 23, 18, 32, 34, 364, DateTimeKind.Utc).AddTicks(8798));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedDate",
                value: new DateTime(2026, 3, 23, 18, 32, 34, 364, DateTimeKind.Utc).AddTicks(8799));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedDate",
                value: new DateTime(2026, 3, 23, 18, 32, 34, 364, DateTimeKind.Utc).AddTicks(8800));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedDate",
                value: new DateTime(2026, 3, 23, 18, 32, 34, 364, DateTimeKind.Utc).AddTicks(8802));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedDate",
                value: new DateTime(2026, 3, 23, 18, 32, 34, 364, DateTimeKind.Utc).AddTicks(8803));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedDate",
                value: new DateTime(2026, 3, 23, 18, 32, 34, 364, DateTimeKind.Utc).AddTicks(8804));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedDate",
                value: new DateTime(2026, 3, 23, 18, 32, 34, 364, DateTimeKind.Utc).AddTicks(8806));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedDate",
                value: new DateTime(2026, 3, 23, 18, 32, 34, 364, DateTimeKind.Utc).AddTicks(8807));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 17,
                column: "CreatedDate",
                value: new DateTime(2026, 3, 23, 18, 32, 34, 364, DateTimeKind.Utc).AddTicks(8808));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 18,
                column: "CreatedDate",
                value: new DateTime(2026, 3, 23, 18, 32, 34, 364, DateTimeKind.Utc).AddTicks(8809));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 3, 18, 17, 10, 9, 578, DateTimeKind.Utc).AddTicks(9405));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 3, 18, 17, 10, 9, 579, DateTimeKind.Utc).AddTicks(883));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 3, 18, 17, 10, 9, 579, DateTimeKind.Utc).AddTicks(886));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2026, 3, 18, 17, 10, 9, 579, DateTimeKind.Utc).AddTicks(887));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2026, 3, 18, 17, 10, 9, 579, DateTimeKind.Utc).AddTicks(889));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2026, 3, 18, 17, 10, 9, 579, DateTimeKind.Utc).AddTicks(890));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2026, 3, 18, 17, 10, 9, 579, DateTimeKind.Utc).AddTicks(891));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2026, 3, 18, 17, 10, 9, 579, DateTimeKind.Utc).AddTicks(892));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedDate",
                value: new DateTime(2026, 3, 18, 17, 10, 9, 579, DateTimeKind.Utc).AddTicks(894));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedDate",
                value: new DateTime(2026, 3, 18, 17, 10, 9, 579, DateTimeKind.Utc).AddTicks(895));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedDate",
                value: new DateTime(2026, 3, 18, 17, 10, 9, 579, DateTimeKind.Utc).AddTicks(896));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedDate",
                value: new DateTime(2026, 3, 18, 17, 10, 9, 579, DateTimeKind.Utc).AddTicks(898));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedDate",
                value: new DateTime(2026, 3, 18, 17, 10, 9, 579, DateTimeKind.Utc).AddTicks(899));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedDate",
                value: new DateTime(2026, 3, 18, 17, 10, 9, 579, DateTimeKind.Utc).AddTicks(900));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedDate",
                value: new DateTime(2026, 3, 18, 17, 10, 9, 579, DateTimeKind.Utc).AddTicks(901));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedDate",
                value: new DateTime(2026, 3, 18, 17, 10, 9, 579, DateTimeKind.Utc).AddTicks(903));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 17,
                column: "CreatedDate",
                value: new DateTime(2026, 3, 18, 17, 10, 9, 579, DateTimeKind.Utc).AddTicks(904));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 18,
                column: "CreatedDate",
                value: new DateTime(2026, 3, 18, 17, 10, 9, 579, DateTimeKind.Utc).AddTicks(905));
        }
    }
}
