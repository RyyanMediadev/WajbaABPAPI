using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wajba.Migrations
{
    /// <inheritdoc />
    public partial class GenderTypeuser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GenderType",
                table: "WajbaUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GenderType",
                table: "WajbaUsers");
        }
    }
}
