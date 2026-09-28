using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class FixRelationUserCompany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_companies_user_id",
                schema: "public",
                table: "companies");

            migrationBuilder.CreateIndex(
                name: "ix_companies_user_id",
                schema: "public",
                table: "companies",
                column: "user_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_companies_user_id",
                schema: "public",
                table: "companies");

            migrationBuilder.CreateIndex(
                name: "ix_companies_user_id",
                schema: "public",
                table: "companies",
                column: "user_id");
        }
    }
}
