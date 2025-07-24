using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddTablePicturesAndRemoveFieldsUrlImageOfEntitiesUserCompanyAndProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "url_image",
                schema: "public",
                table: "users");

            migrationBuilder.DropColumn(
                name: "url_image",
                schema: "public",
                table: "products");

            migrationBuilder.DropColumn(
                name: "url_image",
                schema: "public",
                table: "companies");

            migrationBuilder.CreateTable(
                name: "pictures",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    bucket = table.Column<string>(type: "text", nullable: false),
                    path = table.Column<string>(type: "text", nullable: false),
                    entity_type = table.Column<string>(type: "text", nullable: false),
                    entity_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_pictures", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "pictures",
                schema: "public");

            migrationBuilder.AddColumn<string>(
                name: "url_image",
                schema: "public",
                table: "users",
                type: "character varying(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "url_image",
                schema: "public",
                table: "products",
                type: "character varying(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "url_image",
                schema: "public",
                table: "companies",
                type: "character varying(150)",
                maxLength: 150,
                nullable: true);
        }
    }
}
