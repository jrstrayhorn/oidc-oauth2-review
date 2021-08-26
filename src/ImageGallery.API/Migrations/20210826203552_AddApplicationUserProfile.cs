using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ImageGallery.API.Migrations
{
    public partial class AddApplicationUserProfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationUserProfiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Subject = table.Column<string>(maxLength: 50, nullable: false),
                    SubscriptionLevel = table.Column<string>(maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserProfiles", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserProfiles");
        }
    }
}
