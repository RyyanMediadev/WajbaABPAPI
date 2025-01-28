using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wajba.Migrations
{
    /// <inheritdoc />
    public partial class AdduserAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "CartItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                table: "CartItems",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleterId",
                table: "CartItems",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "CartItems",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "CartItems",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "CartItems",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LastModifierId",
                table: "CartItems",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "WajbaUserAddresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    WajbaUserId = table.Column<int>(type: "int", nullable: true),
                    BuildingName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApartmentNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Floor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressLabel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressType = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_WajbaUserAddresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WajbaUserAddresses_WajbaUsers_WajbaUserId",
                        column: x => x.WajbaUserId,
                        principalTable: "WajbaUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_WajbaUserAddresses_WajbaUserId",
                table: "WajbaUserAddresses",
                column: "WajbaUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WajbaUserAddresses");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "DeleterId",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "LastModifierId",
                table: "CartItems");
        }
    }
}
