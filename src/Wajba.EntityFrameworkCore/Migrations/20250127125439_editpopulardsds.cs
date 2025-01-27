using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wajba.Migrations
{
    /// <inheritdoc />
    public partial class editpopulardsds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PopularItems_Branches_BranchId",
                table: "PopularItems");

            migrationBuilder.DropIndex(
                name: "IX_PopularItems_BranchId",
                table: "PopularItems");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "PopularItems");

            migrationBuilder.CreateTable(
                name: "PopulartItemBranches",
                columns: table => new
                {
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    PopularItemId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PopulartItemBranches", x => new { x.BranchId, x.PopularItemId });
                    table.ForeignKey(
                        name: "FK_PopulartItemBranches_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PopulartItemBranches_PopularItems_PopularItemId",
                        column: x => x.PopularItemId,
                        principalTable: "PopularItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PopulartItemBranches_PopularItemId",
                table: "PopulartItemBranches",
                column: "PopularItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PopulartItemBranches");

            migrationBuilder.AddColumn<int>(
                name: "BranchId",
                table: "PopularItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PopularItems_BranchId",
                table: "PopularItems",
                column: "BranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_PopularItems_Branches_BranchId",
                table: "PopularItems",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
