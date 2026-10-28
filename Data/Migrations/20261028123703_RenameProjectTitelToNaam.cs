using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortfolioApi.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenameProjectTitelToNaam : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Projects",
                newName: "LastUpdated");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastUpdated",
                table: "Projects",
                newName: "CreatedAt");
        }
    }
}
