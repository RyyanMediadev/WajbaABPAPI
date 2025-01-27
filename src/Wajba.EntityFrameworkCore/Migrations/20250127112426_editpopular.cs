using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wajba.Migrations
{
    /// <inheritdoc />
    public partial class editpopular : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_itemBranches",
                table: "itemBranches");

            migrationBuilder.DropIndex(
                name: "IX_itemBranches_BranchId",
                table: "itemBranches");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "itemBranches");

            migrationBuilder.AddPrimaryKey(
                name: "PK_itemBranches",
                table: "itemBranches",
                columns: new[] { "BranchId", "ItemId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_itemBranches",
                table: "itemBranches");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "itemBranches",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_itemBranches",
                table: "itemBranches",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_itemBranches_BranchId",
                table: "itemBranches",
                column: "BranchId");
        }
    }
}
