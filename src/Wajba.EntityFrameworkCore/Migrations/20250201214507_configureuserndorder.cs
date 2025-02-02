using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wajba.Migrations
{
    /// <inheritdoc />
    public partial class configureuserndorder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CartItemAddon",
                columns: table => new
                {
                    AddonId = table.Column<int>(type: "int", nullable: false),
                    AddonName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdditionalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CartItemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItemAddon", x => x.AddonId);
                    table.ForeignKey(
                        name: "FK_CartItemAddon_CartItems_CartItemId",
                        column: x => x.CartItemId,
                        principalTable: "CartItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CartItemExtra",
                columns: table => new
                {
                    ExtraId = table.Column<int>(type: "int", nullable: false),
                    ExtraName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdditionalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CartItemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItemExtra", x => x.ExtraId);
                    table.ForeignKey(
                        name: "FK_CartItemExtra_CartItems_CartItemId",
                        column: x => x.CartItemId,
                        principalTable: "CartItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CartItemVariations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    VariationName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Attributename = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdditionalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CartItemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItemVariations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartItemVariations_CartItems_CartItemId",
                        column: x => x.CartItemId,
                        principalTable: "CartItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WajbaUserRoles",
                columns: table => new
                {
                    WajbaUserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    UserRoleId = table.Column<int>(type: "int", nullable: true),
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
                    table.PrimaryKey("PK_WajbaUserRoles", x => new { x.RoleId, x.WajbaUserId });
                    table.ForeignKey(
                        name: "FK_WajbaUserRoles_UserRoles_UserRoleId",
                        column: x => x.UserRoleId,
                        principalTable: "UserRoles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WajbaUserRoles_WajbaUsers_WajbaUserId",
                        column: x => x.WajbaUserId,
                        principalTable: "WajbaUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartItemAddon_CartItemId",
                table: "CartItemAddon",
                column: "CartItemId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItemExtra_CartItemId",
                table: "CartItemExtra",
                column: "CartItemId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItemVariations_CartItemId",
                table: "CartItemVariations",
                column: "CartItemId");

            migrationBuilder.CreateIndex(
                name: "IX_WajbaUserRoles_UserRoleId",
                table: "WajbaUserRoles",
                column: "UserRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_WajbaUserRoles_WajbaUserId",
                table: "WajbaUserRoles",
                column: "WajbaUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartItemAddon");

            migrationBuilder.DropTable(
                name: "CartItemExtra");

            migrationBuilder.DropTable(
                name: "CartItemVariations");

            migrationBuilder.DropTable(
                name: "WajbaUserRoles");
        }
    }
}
