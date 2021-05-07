using Microsoft.EntityFrameworkCore.Migrations;

namespace Exercicio.Migrations
{
    public partial class AddUsuarioCep : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Cep",
                table: "Usuario",
                type: "nvarchar(8)",
                maxLength: 8,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cep",
                table: "Usuario");
        }
    }
}
