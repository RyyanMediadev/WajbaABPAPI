using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wajba.Migrations
{
    /// <inheritdoc />
    public partial class addconfigurationes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CartItemAddon",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddonId = table.Column<int>(type: "int", nullable: false),
                    AddonName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdditionalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CartItemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItemAddon", x => x.Id);
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExtraId = table.Column<int>(type: "int", nullable: false),
                    ExtraName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdditionalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CartItemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItemExtra", x => x.Id);
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VariationId = table.Column<int>(type: "int", nullable: false),
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartItemAddon");

            migrationBuilder.DropTable(
                name: "CartItemExtra");

            migrationBuilder.DropTable(
                name: "CartItemVariations");
        }
    }
}
