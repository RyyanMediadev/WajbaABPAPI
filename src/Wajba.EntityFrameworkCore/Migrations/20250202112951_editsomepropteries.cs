using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wajba.Migrations
{
    /// <inheritdoc />
    public partial class editsomepropteries : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartItemAddon");

            migrationBuilder.DropTable(
                name: "CartItemExtra");

            migrationBuilder.DropTable(
                name: "CartItemVariations");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CartItemAddon",
                columns: table => new
                {
                    AddonId = table.Column<int>(type: "int", nullable: false),
                    CartItemId = table.Column<int>(type: "int", nullable: false),
                    AdditionalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AddonName = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    CartItemId = table.Column<int>(type: "int", nullable: false),
                    AdditionalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ExtraName = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    CartItemId = table.Column<int>(type: "int", nullable: false),
                    AdditionalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Attributename = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VariationName = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
        }
    }
}
