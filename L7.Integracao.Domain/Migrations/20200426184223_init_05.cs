using Microsoft.EntityFrameworkCore.Migrations;

namespace L7.Integracao.Domain.Migrations
{
    public partial class init_05 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MensagemRetorno",
                table: "Orders",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MensagemRetorno",
                table: "Orders");
        }
    }
}
