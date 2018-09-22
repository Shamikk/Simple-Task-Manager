using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SimpleTaskManager.API.Migrations
{
    public partial class InitialSetup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SimpleTask",
                columns: table => new
                {
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Status = table.Column<int>(nullable: false),
                    CreationDateTime = table.Column<DateTime>(nullable: false),
                    LastUpdateDateTime = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    LastUpdateBy = table.Column<string>(nullable: true),
                    NumberOfUpdates = table.Column<int>(nullable: false),
                    IsLocked = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SimpleTask", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "TaskName",
                columns: table => new
                {
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskName", x => x.Name);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SimpleTask");

            migrationBuilder.DropTable(
                name: "TaskName");
        }
    }
}
