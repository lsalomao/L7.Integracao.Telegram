using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace L7.Integracao.Domain.Migrations
{
    public partial class init_04 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConfiguracoesMsg",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(nullable: true),
                    Sender = table.Column<bool>(nullable: false),
                    Receiver = table.Column<bool>(nullable: false),
                    NomeTopico = table.Column<string>(nullable: true),
                    NomeFila = table.Column<string>(nullable: true),
                    UrlMsg = table.Column<string>(nullable: true),
                    Login = table.Column<string>(nullable: true),
                    Senha = table.Column<string>(nullable: true),
                    Porta = table.Column<int>(nullable: false),
                    Status = table.Column<bool>(nullable: false),
                    DataCadastro = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfiguracoesMsg", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConfiguracoesMsg");
        }
    }
}
