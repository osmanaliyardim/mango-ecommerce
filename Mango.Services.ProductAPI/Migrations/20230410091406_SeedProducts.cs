using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Mango.Services.ProductAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CategoryName", "Description", "ImageUrl", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Appetizer", "Kebap ipsum dolor sit amet, consectetur adipiscing elit. Ut condimentum auctor diam, et suscipit sem. Proin ut est dictum, dapibus arcu quis, porta lectus. Maecenas molestie sit amet lorem in auctor. Praesent nec libero quam. Nullam scelerisque enim dui, non vehicula ligula faucibus non. Etiam rutrum vehicula dui in ultricies.", "https://mediastormango.blob.core.windows.net/mango/kebap.jpg", "Kebap", 25.0 },
                    { 2, "Appetizer", "Iskender ipsum dolor sit amet, consectetur adipiscing elit. Ut condimentum auctor diam, et suscipit sem. Proin ut est dictum, dapibus arcu quis, porta lectus. Maecenas molestie sit amet lorem in auctor. Praesent nec libero quam. Nullam scelerisque enim dui, non vehicula ligula faucibus non. Etiam rutrum vehicula dui in ultricies.", "https://mediastormango.blob.core.windows.net/mango/iskender.jpg", "Iskender", 34.990000000000002 },
                    { 3, "Appetizer", "Doner ipsum dolor sit amet, consectetur adipiscing elit. Ut condimentum auctor diam, et suscipit sem. Proin ut est dictum, dapibus arcu quis, porta lectus. Maecenas molestie sit amet lorem in auctor. Praesent nec libero quam. Nullam scelerisque enim dui, non vehicula ligula faucibus non. Etiam rutrum vehicula dui in ultricies.", "https://mediastormango.blob.core.windows.net/mango/doner.jpg", "Doner", 15.99 },
                    { 4, "Appetizer", "Pide ipsum dolor sit amet, consectetur adipiscing elit. Ut condimentum auctor diam, et suscipit sem. Proin ut est dictum, dapibus arcu quis, porta lectus. Maecenas molestie sit amet lorem in auctor. Praesent nec libero quam. Nullam scelerisque enim dui, non vehicula ligula faucibus non. Etiam rutrum vehicula dui in ultricies.", "https://mediastormango.blob.core.windows.net/mango/pide.jpg", "Pide", 13.85 },
                    { 5, "Dessert", "Baklava ipsum dolor sit amet, consectetur adipiscing elit. Ut condimentum auctor diam, et suscipit sem. Proin ut est dictum, dapibus arcu quis, porta lectus. Maecenas molestie sit amet lorem in auctor. Praesent nec libero quam. Nullam scelerisque enim dui, non vehicula ligula faucibus non. Etiam rutrum vehicula dui in ultricies.", "https://mediastormango.blob.core.windows.net/mango/baklava.jpg", "Baklava", 10.35 },
                    { 6, "Dessert", "Donut ipsum dolor sit amet, consectetur adipiscing elit. Ut condimentum auctor diam, et suscipit sem. Proin ut est dictum, dapibus arcu quis, porta lectus. Maecenas molestie sit amet lorem in auctor. Praesent nec libero quam. Nullam scelerisque enim dui, non vehicula ligula faucibus non. Etiam rutrum vehicula dui in ultricies.", "https://mediastormango.blob.core.windows.net/mango/donut.jpg", "Donut", 8.8800000000000008 },
                    { 7, "Entree", "Atom ipsum dolor sit amet, consectetur adipiscing elit. Ut condimentum auctor diam, et suscipit sem. Proin ut est dictum, dapibus arcu quis, porta lectus. Maecenas molestie sit amet lorem in auctor. Praesent nec libero quam. Nullam scelerisque enim dui, non vehicula ligula faucibus non. Etiam rutrum vehicula dui in ultricies.", "https://mediastormango.blob.core.windows.net/mango/atom.jpg", "Atom", 8.8800000000000008 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 7);
        }
    }
}
