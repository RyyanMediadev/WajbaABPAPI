using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wajba.Migrations
{
    /// <inheritdoc />
    public partial class AddRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.RenameColumn(
            //    name: "Role",
            //    table: "WajbaUsers",
            //    newName: "CustomerRole");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CustomerRole",
                table: "WajbaUsers",
                newName: "CustomerRole");
        }
    }
}
