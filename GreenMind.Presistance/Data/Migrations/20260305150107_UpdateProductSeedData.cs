using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GreenMind.Presistance.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProductSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "Desc", "StockQuantity" },
                values: new object[] { new DateTime(2026, 3, 5, 15, 1, 7, 100, DateTimeKind.Utc).AddTicks(5240), "Refreshing aroma, vibrant leaves. Perfect for teas, cooking, and home gardens. Easy to grow!", 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "Desc", "StockQuantity" },
                values: new object[] { new DateTime(2026, 3, 5, 15, 1, 7, 100, DateTimeKind.Utc).AddTicks(6569), "Grow coffee at home with premium seeds. Cultivate aromatic beans for your daily brew. Perfect for enthusiasts.", 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "Desc", "StockQuantity" },
                values: new object[] { new DateTime(2026, 3, 5, 15, 1, 7, 100, DateTimeKind.Utc).AddTicks(6573), "Peppery and aromatic. Fast-growing, easy care. Perfect for pickling, fish, and salads.", 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "Desc", "StockQuantity" },
                values: new object[] { new DateTime(2026, 3, 5, 15, 1, 7, 100, DateTimeKind.Utc).AddTicks(6574), "Fresh, sweet garden peas. Easy to grow, tender, and delicious pods. Enjoy homegrown peas in 60-70 days.", 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "Desc", "Img", "Name", "StockQuantity" },
                values: new object[] { new DateTime(2026, 3, 5, 15, 1, 7, 100, DateTimeKind.Utc).AddTicks(6575), "High yield, excellent taste. Ideal for paddy, disease resistant. Perfect for the Egyptian climate.", "/images/ri.png", "Premium Rice Seeds", 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedDate", "Desc", "StockQuantity" },
                values: new object[] { new DateTime(2026, 3, 5, 15, 1, 7, 100, DateTimeKind.Utc).AddTicks(6580), "8 Quarts formula. Specialized for seed germination and cuttings. Approved for organic growing.", 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedDate", "Desc", "StockQuantity" },
                values: new object[] { new DateTime(2026, 3, 5, 15, 1, 7, 100, DateTimeKind.Utc).AddTicks(6581), "Available in .75 or 1.5 cubic feet. Formulated for herbs, vegetables, and indoor plants.", 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedDate", "Desc", "StockQuantity" },
                values: new object[] { new DateTime(2026, 3, 5, 15, 1, 7, 100, DateTimeKind.Utc).AddTicks(6582), "1 Cubic foot. Formulated for flower beds, vegetable gardens, trees, and shrubs. For in-ground use", 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedDate", "Desc", "StockQuantity" },
                values: new object[] { new DateTime(2026, 3, 5, 15, 1, 7, 100, DateTimeKind.Utc).AddTicks(6587), null, 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreatedDate", "Desc", "StockQuantity" },
                values: new object[] { new DateTime(2026, 3, 5, 15, 1, 7, 100, DateTimeKind.Utc).AddTicks(6588), null, 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CreatedDate", "Desc", "StockQuantity" },
                values: new object[] { new DateTime(2026, 3, 5, 15, 1, 7, 100, DateTimeKind.Utc).AddTicks(6589), null, 0 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedDate", "Desc", "Img", "Name", "Price", "StockQuantity" },
                values: new object[,]
                {
                    { 6, 1, new DateTime(2026, 3, 5, 15, 1, 7, 100, DateTimeKind.Utc).AddTicks(6577), "Crisp, peppery leaves. Ideal for salads and sandwiches. Fast-growing and rich in vitamins.", "/images/arugula.png", "Premium Arugula Seeds", 50m, 0 },
                    { 7, 1, new DateTime(2026, 3, 5, 15, 1, 7, 100, DateTimeKind.Utc).AddTicks(6578), "Rich in protein and fiber. Ideal for healthy Egyptian cooking. Grow fresh beans for soups and stews.", "/images/white bean.png", "Premium White Bean Seeds", 50m, 0 },
                    { 11, 2, new DateTime(2026, 3, 5, 15, 1, 7, 100, DateTimeKind.Utc).AddTicks(6584), "Enriched with Humus. Available in .75 cubic feet bags for nutrient-rich soil.", "/images/composted manure.png", "Composted Manure", 100m, 0 },
                    { 12, 2, new DateTime(2026, 3, 5, 15, 1, 7, 100, DateTimeKind.Utc).AddTicks(6585), "1 Cubic foot. Ideal for container gardens, hanging baskets, and window boxes.", "/images/potting soil.png", "Potting Soil", 100m, 0 },
                    { 16, 3, new DateTime(2026, 3, 5, 15, 1, 7, 100, DateTimeKind.Utc).AddTicks(6590), null, "/images/watering.png", "Watering Can", 200m, 0 },
                    { 17, 3, new DateTime(2026, 3, 5, 15, 1, 7, 100, DateTimeKind.Utc).AddTicks(6591), null, "/images/Hand.png", "Hand Cultivator", 200m, 0 },
                    { 18, 3, new DateTime(2026, 3, 5, 15, 1, 7, 100, DateTimeKind.Utc).AddTicks(6593), null, "/images/point Shovel.png", "Round-Point Shovel", 200m, 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "Desc", "StockQuantity" },
                values: new object[] { new DateTime(2026, 3, 4, 6, 28, 20, 135, DateTimeKind.Utc).AddTicks(9905), "Refreshing aroma, vibrant leaves. Perfect for teas.", 100 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "Desc", "StockQuantity" },
                values: new object[] { new DateTime(2026, 3, 4, 6, 28, 20, 136, DateTimeKind.Utc).AddTicks(1473), "Grow coffee at home with premium seeds.", 100 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "Desc", "StockQuantity" },
                values: new object[] { new DateTime(2026, 3, 4, 6, 28, 20, 136, DateTimeKind.Utc).AddTicks(1476), "Peppery and aromatic. Fast-growing, easy care.", 100 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "Desc", "StockQuantity" },
                values: new object[] { new DateTime(2026, 3, 4, 6, 28, 20, 136, DateTimeKind.Utc).AddTicks(1478), "Fresh, sweet garden peas. Easy to grow.", 100 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "Desc", "Img", "Name", "StockQuantity" },
                values: new object[] { new DateTime(2026, 3, 4, 6, 28, 20, 136, DateTimeKind.Utc).AddTicks(1479), "Fast-growing leafy green with a peppery taste.", "/images/arugula.png", "Arugula Seeds", 100 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedDate", "Desc", "StockQuantity" },
                values: new object[] { new DateTime(2026, 3, 4, 6, 28, 20, 136, DateTimeKind.Utc).AddTicks(1480), "Specialized for seed germination.", 50 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedDate", "Desc", "StockQuantity" },
                values: new object[] { new DateTime(2026, 3, 4, 6, 28, 20, 136, DateTimeKind.Utc).AddTicks(1482), "Formulated for herbs and vegetables.", 50 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedDate", "Desc", "StockQuantity" },
                values: new object[] { new DateTime(2026, 3, 4, 6, 28, 20, 136, DateTimeKind.Utc).AddTicks(1483), "Ideal for flower beds and vegetable gardens.", 50 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedDate", "Desc", "StockQuantity" },
                values: new object[] { new DateTime(2026, 3, 4, 6, 28, 20, 136, DateTimeKind.Utc).AddTicks(1485), "Durable fork for soil preparation.", 20 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreatedDate", "Desc", "StockQuantity" },
                values: new object[] { new DateTime(2026, 3, 4, 6, 28, 20, 136, DateTimeKind.Utc).AddTicks(1486), "High-quality shovel for gardening.", 20 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CreatedDate", "Desc", "StockQuantity" },
                values: new object[] { new DateTime(2026, 3, 4, 6, 28, 20, 136, DateTimeKind.Utc).AddTicks(1487), "Perfect for moving loose materials.", 20 });
        }
    }
}
