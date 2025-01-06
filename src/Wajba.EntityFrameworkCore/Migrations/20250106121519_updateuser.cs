using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wajba.Migrations
{
    /// <inheritdoc />
    public partial class updateuser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_APPUser_Branches_BranchId",
                table: "APPUser");

            migrationBuilder.DropIndex(
                name: "IX_APPUser_BranchId",
                table: "APPUser");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "APPUser");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "APPUser");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "APPUser");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "APPUser");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "APPUser");

            migrationBuilder.DropColumn(
                name: "ProfileId",
                table: "APPUser");

            migrationBuilder.DropColumn(
                name: "SecoundName",
                table: "APPUser");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "APPUser",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "BranchId",
                table: "APPUser",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "APPUser",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "APPUser",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "APPUser",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ProfileId",
                table: "APPUser",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SecoundName",
                table: "APPUser",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_APPUser_BranchId",
                table: "APPUser",
                column: "BranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_APPUser_Branches_BranchId",
                table: "APPUser",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
