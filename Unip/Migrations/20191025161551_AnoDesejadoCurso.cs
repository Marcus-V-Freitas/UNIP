using Microsoft.EntityFrameworkCore.Migrations;

namespace Unip.Migrations
{
    public partial class AnoDesejadoCurso : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AnoDesejadoCurso",
                table: "Clientes",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnoDesejadoCurso",
                table: "Clientes");
        }
    }
}
