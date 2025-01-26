using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wajba.Migrations
{
    /// <inheritdoc />
    public partial class userrolesandbranches : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_coupons_APPUser_APPUserId",
            //    table: "coupons");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_ItemVariations_ItemAttributes_ItemAttributeId",
            //    table: "ItemVariations");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_UserAddress_APPUser_CustomerId1",
            //    table: "UserAddress");

            //migrationBuilder.DropIndex(
            //    name: "IX_ItemVariations_ItemAttributeId",
            //    table: "ItemVariations");

            //migrationBuilder.DropIndex(
            //    name: "IX_coupons_APPUserId",
            //    table: "coupons");

            //migrationBuilder.DropPrimaryKey(
            //    name: "PK_WajbaUser",
            //    table: "WajbaUser");

            //migrationBuilder.DropColumn(
            //    name: "ItemAttributeId",
            //    table: "ItemVariations");

            //migrationBuilder.DropColumn(
            //    name: "APPUserId",
            //    table: "coupons");

            //migrationBuilder.RenameTable(
            //    name: "WajbaUser",
            //    newName: "WajbaUsers");

            //migrationBuilder.RenameColumn(
            //    name: "CustomerId1",
            //    table: "UserAddress",
            //    newName: "APPUserId");

            //migrationBuilder.RenameIndex(
            //    name: "IX_UserAddress_CustomerId1",
            //    table: "UserAddress",
            //    newName: "IX_UserAddress_APPUserId");

            //migrationBuilder.AddPrimaryKey(
            //    name: "PK_WajbaUsers",
            //    table: "WajbaUsers",
            //    column: "Id");

            //migrationBuilder.CreateTable(
            //    name: "WajbaUserBranches",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        WajbaUserId = table.Column<int>(type: "int", nullable: false),
            //        BranchId = table.Column<int>(type: "int", nullable: true),
            //        CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
            //        LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
            //        LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
            //        IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
            //        DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
            //        DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_WajbaUserBranches", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_WajbaUserBranches_Branches_BranchId",
            //            column: x => x.BranchId,
            //            principalTable: "Branches",
            //            principalColumn: "Id");
            //        table.ForeignKey(
            //            name: "FK_WajbaUserBranches_WajbaUsers_WajbaUserId",
            //            column: x => x.WajbaUserId,
            //            principalTable: "WajbaUsers",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "WajbaUserRoles",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        WajbaUserId = table.Column<int>(type: "int", nullable: false),
            //        RoleId = table.Column<int>(type: "int", nullable: true),
            //        CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
            //        LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
            //        LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
            //        IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
            //        DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
            //        DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_WajbaUserRoles", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_WajbaUserRoles_WajbaUsers_WajbaUserId",
            //            column: x => x.WajbaUserId,
            //            principalTable: "WajbaUsers",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_WajbaUserBranches_BranchId",
            //    table: "WajbaUserBranches",
            //    column: "BranchId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_WajbaUserBranches_WajbaUserId",
            //    table: "WajbaUserBranches",
            //    column: "WajbaUserId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_WajbaUserRoles_WajbaUserId",
            //    table: "WajbaUserRoles",
            //    column: "WajbaUserId");

           //migrationBuilder.AddForeignKey(
           //     name: "FK_UserAddress_APPUser_APPUserId",
           //     table: "UserAddress",
           //     column: "APPUserId",
           //     principalTable: "APPUser",
           //     principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_UserAddress_APPUser_APPUserId",
            //    table: "UserAddress");

            migrationBuilder.DropTable(
                name: "WajbaUserBranches");

            migrationBuilder.DropTable(
                name: "WajbaUserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WajbaUsers",
                table: "WajbaUsers");

            migrationBuilder.RenameTable(
                name: "WajbaUsers",
                newName: "WajbaUser");

            migrationBuilder.RenameColumn(
                name: "APPUserId",
                table: "UserAddress",
                newName: "CustomerId1");

            migrationBuilder.RenameIndex(
                name: "IX_UserAddress_APPUserId",
                table: "UserAddress",
                newName: "IX_UserAddress_CustomerId1");

            migrationBuilder.AddColumn<int>(
                name: "ItemAttributeId",
                table: "ItemVariations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "APPUserId",
                table: "coupons",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_WajbaUser",
                table: "WajbaUser",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ItemVariations_ItemAttributeId",
                table: "ItemVariations",
                column: "ItemAttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_coupons_APPUserId",
                table: "coupons",
                column: "APPUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_coupons_APPUser_APPUserId",
                table: "coupons",
                column: "APPUserId",
                principalTable: "APPUser",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemVariations_ItemAttributes_ItemAttributeId",
                table: "ItemVariations",
                column: "ItemAttributeId",
                principalTable: "ItemAttributes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAddress_APPUser_CustomerId1",
                table: "UserAddress",
                column: "CustomerId1",
                principalTable: "APPUser",
                principalColumn: "Id");
        }
    }
}
