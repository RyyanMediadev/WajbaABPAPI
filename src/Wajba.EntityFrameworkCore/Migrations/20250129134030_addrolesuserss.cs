using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wajba.Migrations
{
    /// <inheritdoc />
    public partial class addrolesuserss : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "WajbaUserRoles");
            migrationBuilder.CreateTable(
                name: "WajbaUserRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WajbaUserId = table.Column<int>(type: "int", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RoleId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WajbaUserRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WajbaUserRoles_WajbaUsers_WajbaUserId",
                        column: x => x.WajbaUserId,
                        principalTable: "WajbaUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WajbaUserRoles_WajbaUserId",
                table: "WajbaUserRoles",
                column: "WajbaUserId");

            migrationBuilder.AddColumn<int>(
                name: "UserRoleId",
                table: "WajbaUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WajbaUserId",
                table: "coupons",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_UserRoles", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WajbaUsers_UserRoleId",
                table: "WajbaUsers",
                column: "UserRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_coupons_WajbaUserId",
                table: "coupons",
                column: "WajbaUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_coupons_WajbaUsers_WajbaUserId",
                table: "coupons",
                column: "WajbaUserId",
                principalTable: "WajbaUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WajbaUsers_UserRoles_UserRoleId",
                table: "WajbaUsers",
                column: "UserRoleId",
                principalTable: "UserRoles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_coupons_WajbaUsers_WajbaUserId",
                table: "coupons");

            migrationBuilder.DropForeignKey(
                name: "FK_WajbaUsers_UserRoles_UserRoleId",
                table: "WajbaUsers");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropIndex(
                name: "IX_WajbaUsers_UserRoleId",
                table: "WajbaUsers");

            migrationBuilder.DropIndex(
                name: "IX_coupons_WajbaUserId",
                table: "coupons");

            migrationBuilder.DropColumn(
                name: "UserRoleId",
                table: "WajbaUsers");

            migrationBuilder.DropColumn(
                name: "WajbaUserId",
                table: "coupons");

               }
    }
}
