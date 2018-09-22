using Microsoft.EntityFrameworkCore.Migrations;

namespace SimpleTaskManager.API.Migrations
{
    public partial class RemovedunecessarytableremovedIsLockedfromSimpleTask : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskName");

            migrationBuilder.DropColumn(
                name: "IsLocked",
                table: "SimpleTask");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsLocked",
                table: "SimpleTask",
                nullable: false,
                defaultValue: false);

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
    }
}
