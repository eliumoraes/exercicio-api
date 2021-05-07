using Microsoft.EntityFrameworkCore.Migrations;

namespace Exercicio.Migrations
{
    public partial class ViaCep : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Bairro",
                table: "Usuario",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cidade",
                table: "Usuario",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Logradouro",
                table: "Usuario",
                type: "nvarchar(80)",
                maxLength: 80,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bairro",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "Cidade",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "Logradouro",
                table: "Usuario");
        }
    }
}
