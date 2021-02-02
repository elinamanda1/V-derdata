using Microsoft.EntityFrameworkCore.Migrations;

namespace WeatherData.Migrations
{
    public partial class SecondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AvgHumInside",
                table: "Averages",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "AvgTempInside",
                table: "Averages",
                type: "float",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvgHumInside",
                table: "Averages");

            migrationBuilder.DropColumn(
                name: "AvgTempInside",
                table: "Averages");
        }
    }
}
