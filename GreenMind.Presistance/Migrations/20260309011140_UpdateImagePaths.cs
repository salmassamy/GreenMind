using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GreenMind.Presistance.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateImagePaths : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "Img" },
                values: new object[] { new DateTime(2026, 3, 9, 1, 11, 39, 609, DateTimeKind.Utc).AddTicks(846), "/images/mint-seeds.png" });

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
                columns: new[] { "CreatedDate", "Img" },
                values: new object[] { new DateTime(2026, 3, 9, 1, 11, 39, 609, DateTimeKind.Utc).AddTicks(2146), "/images/white-bean.png" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedDate", "Img" },
                values: new object[] { new DateTime(2026, 3, 9, 1, 11, 39, 609, DateTimeKind.Utc).AddTicks(2147), "/images/organic-seed.png" });

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
                columns: new[] { "CreatedDate", "Img" },
                values: new object[] { new DateTime(2026, 3, 9, 1, 11, 39, 609, DateTimeKind.Utc).AddTicks(2149), "/images/garden-soil.png" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedDate", "Img" },
                values: new object[] { new DateTime(2026, 3, 9, 1, 11, 39, 609, DateTimeKind.Utc).AddTicks(2151), "/images/composted-manure.png" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedDate", "Img" },
                values: new object[] { new DateTime(2026, 3, 9, 1, 11, 39, 609, DateTimeKind.Utc).AddTicks(2152), "/images/potting-soil.png" });

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
                columns: new[] { "CreatedDate", "Img" },
                values: new object[] { new DateTime(2026, 3, 9, 1, 11, 39, 609, DateTimeKind.Utc).AddTicks(2160), "/images/point-Shovel.png" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "Img" },
                values: new object[] { new DateTime(2026, 3, 5, 21, 6, 44, 675, DateTimeKind.Utc).AddTicks(7353), "/images/mint seeds.png" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 3, 5, 21, 6, 44, 675, DateTimeKind.Utc).AddTicks(8616));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 3, 5, 21, 6, 44, 675, DateTimeKind.Utc).AddTicks(8618));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2026, 3, 5, 21, 6, 44, 675, DateTimeKind.Utc).AddTicks(8620));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2026, 3, 5, 21, 6, 44, 675, DateTimeKind.Utc).AddTicks(8621));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2026, 3, 5, 21, 6, 44, 675, DateTimeKind.Utc).AddTicks(8623));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedDate", "Img" },
                values: new object[] { new DateTime(2026, 3, 5, 21, 6, 44, 675, DateTimeKind.Utc).AddTicks(8624), "/images/white bean.png" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedDate", "Img" },
                values: new object[] { new DateTime(2026, 3, 5, 21, 6, 44, 675, DateTimeKind.Utc).AddTicks(8625), "/images/organic seed.png" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedDate",
                value: new DateTime(2026, 3, 5, 21, 6, 44, 675, DateTimeKind.Utc).AddTicks(8627));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedDate", "Img" },
                values: new object[] { new DateTime(2026, 3, 5, 21, 6, 44, 675, DateTimeKind.Utc).AddTicks(8628), "/images/garden soil.png" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedDate", "Img" },
                values: new object[] { new DateTime(2026, 3, 5, 21, 6, 44, 675, DateTimeKind.Utc).AddTicks(8629), "/images/composted manure.png" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedDate", "Img" },
                values: new object[] { new DateTime(2026, 3, 5, 21, 6, 44, 675, DateTimeKind.Utc).AddTicks(8630), "/images/potting soil.png" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedDate",
                value: new DateTime(2026, 3, 5, 21, 6, 44, 675, DateTimeKind.Utc).AddTicks(8632));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedDate",
                value: new DateTime(2026, 3, 5, 21, 6, 44, 675, DateTimeKind.Utc).AddTicks(8633));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedDate",
                value: new DateTime(2026, 3, 5, 21, 6, 44, 675, DateTimeKind.Utc).AddTicks(8634));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedDate",
                value: new DateTime(2026, 3, 5, 21, 6, 44, 675, DateTimeKind.Utc).AddTicks(8636));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 17,
                column: "CreatedDate",
                value: new DateTime(2026, 3, 5, 21, 6, 44, 675, DateTimeKind.Utc).AddTicks(8637));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "CreatedDate", "Img" },
                values: new object[] { new DateTime(2026, 3, 5, 21, 6, 44, 675, DateTimeKind.Utc).AddTicks(8638), "/images/point Shovel.png" });
        }
    }
}
