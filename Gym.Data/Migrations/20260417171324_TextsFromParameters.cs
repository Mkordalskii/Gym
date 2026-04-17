using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gym.Data.Migrations
{
    /// <inheritdoc />
    public partial class TextsFromParameters : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_PortalPage_Slug",
                table: "PortalPage",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Parameter_Name",
                table: "Parameter",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PortalPage_Slug",
                table: "PortalPage");

            migrationBuilder.DropIndex(
                name: "IX_Parameter_Name",
                table: "Parameter");
        }
    }
}
