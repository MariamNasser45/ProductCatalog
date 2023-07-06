using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductCatalog.Data.Migrations
{
    public partial class usingCamelCase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "Products",
                newName: "startDate");

            migrationBuilder.RenameColumn(
                name: "ProductName",
                table: "Products",
                newName: "productName");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Products",
                newName: "price");

            migrationBuilder.RenameColumn(
                name: "Duration",
                table: "Products",
                newName: "duration");

            migrationBuilder.RenameColumn(
                name: "CreationDate",
                table: "Products",
                newName: "creationDate");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Products",
                newName: "createdBy");

            migrationBuilder.RenameColumn(
                name: "CategoryName",
                table: "Categories",
                newName: "categoryName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "startDate",
                table: "Products",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "productName",
                table: "Products",
                newName: "ProductName");

            migrationBuilder.RenameColumn(
                name: "price",
                table: "Products",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "duration",
                table: "Products",
                newName: "Duration");

            migrationBuilder.RenameColumn(
                name: "creationDate",
                table: "Products",
                newName: "CreationDate");

            migrationBuilder.RenameColumn(
                name: "createdBy",
                table: "Products",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "categoryName",
                table: "Categories",
                newName: "CategoryName");
        }
    }
}
