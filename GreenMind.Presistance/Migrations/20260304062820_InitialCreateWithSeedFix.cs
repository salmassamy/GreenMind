using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GreenMind.Presistance.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateWithSeedFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "ImageURL",
                table: "Products",
                newName: "Img");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Products",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Desc",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Seeds");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedDate", "Name" },
                values: new object[,]
                {
                    { 2, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Soil" },
                    { 3, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tools" }
                });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "Desc", "Img", "Name", "Price", "StockQuantity" },
                values: new object[] { new DateTime(2026, 3, 4, 6, 28, 20, 135, DateTimeKind.Utc).AddTicks(9905), "Refreshing aroma, vibrant leaves. Perfect for teas.", "/images/mint seeds.png", "Premium Mint Seeds", 50m, 100 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedDate", "Desc", "Img", "Name", "Price", "StockQuantity" },
                values: new object[,]
                {
                    { 2, 1, new DateTime(2026, 3, 4, 6, 28, 20, 136, DateTimeKind.Utc).AddTicks(1473), "Grow coffee at home with premium seeds.", "/images/coffee.png", "Coffee Seeds", 50m, 100 },
                    { 3, 1, new DateTime(2026, 3, 4, 6, 28, 20, 136, DateTimeKind.Utc).AddTicks(1476), "Peppery and aromatic. Fast-growing, easy care.", "/images/s.png", "Premium Dill Seeds", 50m, 100 },
                    { 4, 1, new DateTime(2026, 3, 4, 6, 28, 20, 136, DateTimeKind.Utc).AddTicks(1478), "Fresh, sweet garden peas. Easy to grow.", "/images/pea.png", "Premium Pea Seeds", 50m, 100 },
                    { 5, 1, new DateTime(2026, 3, 4, 6, 28, 20, 136, DateTimeKind.Utc).AddTicks(1479), "Fast-growing leafy green with a peppery taste.", "/images/arugula.png", "Arugula Seeds", 50m, 100 },
                    { 8, 2, new DateTime(2026, 3, 4, 6, 28, 20, 136, DateTimeKind.Utc).AddTicks(1480), "Specialized for seed germination.", "/images/organic seed.png", "Organic Seed Starting", 100m, 50 },
                    { 9, 2, new DateTime(2026, 3, 4, 6, 28, 20, 136, DateTimeKind.Utc).AddTicks(1482), "Formulated for herbs and vegetables.", "/images/P.png", "Potting Mix", 100m, 50 },
                    { 10, 2, new DateTime(2026, 3, 4, 6, 28, 20, 136, DateTimeKind.Utc).AddTicks(1483), "Ideal for flower beds and vegetable gardens.", "/images/garden soil.png", "Garden Soil", 100m, 50 },
                    { 13, 3, new DateTime(2026, 3, 4, 6, 28, 20, 136, DateTimeKind.Utc).AddTicks(1485), "Durable fork for soil preparation.", "/images/Rectangle.png", "Digging Fork", 200m, 20 },
                    { 14, 3, new DateTime(2026, 3, 4, 6, 28, 20, 136, DateTimeKind.Utc).AddTicks(1486), "High-quality shovel for gardening.", "/images/shovel.png", "Shovel", 200m, 20 },
                    { 15, 3, new DateTime(2026, 3, 4, 6, 28, 20, 136, DateTimeKind.Utc).AddTicks(1487), "Perfect for moving loose materials.", "/images/T.png", "Square-Point Shovel", 200m, 20 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "Desc",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "Img",
                table: "Products",
                newName: "ImageURL");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Garden Supplies");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "Description", "ImageURL", "Name", "Price", "StockQuantity" },
                values: new object[] { new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Organic soil", "soil.jpg", "Potting Mix", 100m, 50 });
        }
    }
}
