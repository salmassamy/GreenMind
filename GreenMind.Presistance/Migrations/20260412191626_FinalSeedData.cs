using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GreenMind.Presistance.Data.Migrations
{
    /// <inheritdoc />
    public partial class FinalSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 4, 12, 21, 16, 25, 537, DateTimeKind.Local).AddTicks(8129));

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedDate", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seeds" },
                    { 2, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Soil" },
                    { 3, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tools" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedDate", "Desc", "Img", "Name", "Price", "StockQuantity" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2026, 4, 12, 19, 16, 25, 549, DateTimeKind.Utc).AddTicks(1839), "Refreshing aroma, vibrant leaves. Perfect for teas, cooking, and home gardens. Easy to grow!", "/images/mint-seeds.png", "Premium Mint Seeds", 50m, 0 },
                    { 2, 1, new DateTime(2026, 4, 12, 19, 16, 25, 549, DateTimeKind.Utc).AddTicks(3387), "Grow coffee at home with premium seeds. Cultivate aromatic beans for your daily brew. Perfect for enthusiasts.", "/images/coffee.png", "Coffee Seeds", 50m, 0 },
                    { 3, 1, new DateTime(2026, 4, 12, 19, 16, 25, 549, DateTimeKind.Utc).AddTicks(3390), "Peppery and aromatic. Fast-growing, easy care. Perfect for pickling, fish, and salads.", "/images/s.png", "Premium Dill Seeds", 50m, 0 },
                    { 4, 1, new DateTime(2026, 4, 12, 19, 16, 25, 549, DateTimeKind.Utc).AddTicks(3391), "Fresh, sweet garden peas. Easy to grow, tender, and delicious pods. Enjoy homegrown peas in 60-70 days.", "/images/pea.png", "Premium Pea Seeds", 50m, 0 },
                    { 5, 1, new DateTime(2026, 4, 12, 19, 16, 25, 549, DateTimeKind.Utc).AddTicks(3393), "High yield, excellent taste. Ideal for paddy, disease resistant. Perfect for the Egyptian climate.", "/images/ri.png", "Premium Rice Seeds", 50m, 0 },
                    { 6, 1, new DateTime(2026, 4, 12, 19, 16, 25, 549, DateTimeKind.Utc).AddTicks(3395), "Crisp, peppery leaves. Ideal for salads and sandwiches. Fast-growing and rich in vitamins.", "/images/arugula.png", "Premium Arugula Seeds", 50m, 0 },
                    { 7, 1, new DateTime(2026, 4, 12, 19, 16, 25, 549, DateTimeKind.Utc).AddTicks(3475), "Rich in protein and fiber. Ideal for healthy Egyptian cooking. Grow fresh beans for soups and stews.", "/images/white-bean.png", "Premium White Bean Seeds", 50m, 0 },
                    { 8, 2, new DateTime(2026, 4, 12, 19, 16, 25, 549, DateTimeKind.Utc).AddTicks(3477), "8 Quarts formula. Specialized for seed germination and cuttings. Approved for organic growing.", "/images/organic-seed.png", "Organic Seed Starting", 100m, 0 },
                    { 9, 2, new DateTime(2026, 4, 12, 19, 16, 25, 549, DateTimeKind.Utc).AddTicks(3479), "Available in .75 or 1.5 cubic feet. Formulated for herbs, vegetables, and indoor plants.", "/images/P.png", "Potting Mix", 100m, 0 },
                    { 10, 2, new DateTime(2026, 4, 12, 19, 16, 25, 549, DateTimeKind.Utc).AddTicks(3480), "1 Cubic foot. Formulated for flower beds, vegetable gardens, trees, and shrubs. For in-ground use", "/images/garden-soil.png", "Garden Soil", 100m, 0 },
                    { 11, 2, new DateTime(2026, 4, 12, 19, 16, 25, 549, DateTimeKind.Utc).AddTicks(3481), "Enriched with Humus. Available in .75 cubic feet bags for nutrient-rich soil.", "/images/composted-manure.png", "Composted Manure", 100m, 0 },
                    { 12, 2, new DateTime(2026, 4, 12, 19, 16, 25, 549, DateTimeKind.Utc).AddTicks(3483), "1 Cubic foot. Ideal for container gardens, hanging baskets, and window boxes.", "/images/potting-soil.png", "Potting Soil", 100m, 0 },
                    { 13, 3, new DateTime(2026, 4, 12, 19, 16, 25, 549, DateTimeKind.Utc).AddTicks(3484), null, "/images/Rectangle.png", "Digging Fork", 200m, 0 },
                    { 14, 3, new DateTime(2026, 4, 12, 19, 16, 25, 549, DateTimeKind.Utc).AddTicks(3485), null, "/images/shovel.png", "Shovel", 200m, 0 },
                    { 15, 3, new DateTime(2026, 4, 12, 19, 16, 25, 549, DateTimeKind.Utc).AddTicks(3486), null, "/images/T.png", "Square-Point Shovel", 200m, 0 },
                    { 16, 3, new DateTime(2026, 4, 12, 19, 16, 25, 549, DateTimeKind.Utc).AddTicks(3488), null, "/images/watering.png", "Watering Can", 200m, 0 },
                    { 17, 3, new DateTime(2026, 4, 12, 19, 16, 25, 549, DateTimeKind.Utc).AddTicks(3489), null, "/images/Hand.png", "Hand Cultivator", 200m, 0 },
                    { 18, 3, new DateTime(2026, 4, 12, 19, 16, 25, 549, DateTimeKind.Utc).AddTicks(3490), null, "/images/point-Shovel.png", "Round-Point Shovel", 200m, 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

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
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7);

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
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12);

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

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 4, 12, 21, 6, 45, 271, DateTimeKind.Local).AddTicks(2710));
        }
    }
}
