using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GreenMind.Presistance.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixUserTableAndIdentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 3, 9, 1, 11, 39, 609, DateTimeKind.Utc).AddTicks(846));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 3, 9, 1, 11, 39, 609, DateTimeKind.Utc).AddTicks(2137));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 3, 9, 1, 11, 39, 609, DateTimeKind.Utc).AddTicks(2140));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2026, 3, 9, 1, 11, 39, 609, DateTimeKind.Utc).AddTicks(2142));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2026, 3, 9, 1, 11, 39, 609, DateTimeKind.Utc).AddTicks(2143));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2026, 3, 9, 1, 11, 39, 609, DateTimeKind.Utc).AddTicks(2144));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2026, 3, 9, 1, 11, 39, 609, DateTimeKind.Utc).AddTicks(2146));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2026, 3, 9, 1, 11, 39, 609, DateTimeKind.Utc).AddTicks(2147));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedDate",
                value: new DateTime(2026, 3, 9, 1, 11, 39, 609, DateTimeKind.Utc).AddTicks(2148));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedDate",
                value: new DateTime(2026, 3, 9, 1, 11, 39, 609, DateTimeKind.Utc).AddTicks(2149));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedDate",
                value: new DateTime(2026, 3, 9, 1, 11, 39, 609, DateTimeKind.Utc).AddTicks(2151));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedDate",
                value: new DateTime(2026, 3, 9, 1, 11, 39, 609, DateTimeKind.Utc).AddTicks(2152));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedDate",
                value: new DateTime(2026, 3, 9, 1, 11, 39, 609, DateTimeKind.Utc).AddTicks(2154));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedDate",
                value: new DateTime(2026, 3, 9, 1, 11, 39, 609, DateTimeKind.Utc).AddTicks(2155));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedDate",
                value: new DateTime(2026, 3, 9, 1, 11, 39, 609, DateTimeKind.Utc).AddTicks(2156));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedDate",
                value: new DateTime(2026, 3, 9, 1, 11, 39, 609, DateTimeKind.Utc).AddTicks(2157));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 17,
                column: "CreatedDate",
                value: new DateTime(2026, 3, 9, 1, 11, 39, 609, DateTimeKind.Utc).AddTicks(2158));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 18,
                column: "CreatedDate",
                value: new DateTime(2026, 3, 9, 1, 11, 39, 609, DateTimeKind.Utc).AddTicks(2160));
        }
    }
}
