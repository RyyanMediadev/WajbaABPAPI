using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wajba.Migrations
{
    /// <inheritdoc />
    public partial class addnotificationtable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FireBasePublicVapidKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FireBaseAPIKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FireBaseProjectId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FireBaseAuthDomain = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FireBaseStorageBucket = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FireBaseMessageSenderId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FireBaseAppId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FireBaseMeasurementId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notifications");
        }
    }
}
