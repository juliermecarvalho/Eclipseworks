using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eclipseworks.Persistencia.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "usuarios",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nome = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuarios", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "projetos",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nome = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    criador_do_projeto_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_projetos", x => x.id);
                    table.ForeignKey(
                        name: "FK_projetos_usuarios_criador_do_projeto_id",
                        column: x => x.criador_do_projeto_id,
                        principalTable: "usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tarefas",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    titulo = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    descricao = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    data_vencimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    status = table.Column<int>(name: "status ", type: "int", nullable: false),
                    Prioridade = table.Column<int>(type: "int", nullable: false),
                    projeto_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tarefas", x => x.id);
                    table.ForeignKey(
                        name: "FK_tarefas_projetos_projeto_id",
                        column: x => x.projeto_id,
                        principalTable: "projetos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "comentarios",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    texto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tarefa_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_comentarios", x => x.id);
                    table.ForeignKey(
                        name: "FK_comentarios_tarefas_tarefa_id",
                        column: x => x.tarefa_id,
                        principalTable: "tarefas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "historicos",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    usuario_id = table.Column<long>(type: "bigint", nullable: false),
                    operacao = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    tarefa_id = table.Column<long>(type: "bigint", nullable: false),
                    campo = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    valor_antigo = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    novo_valor = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_historicos", x => x.id);
                    table.ForeignKey(
                        name: "FK_historicos_tarefas_tarefa_id",
                        column: x => x.tarefa_id,
                        principalTable: "tarefas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_historicos_usuarios_usuario_id",
                        column: x => x.usuario_id,
                        principalTable: "usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_comentarios_tarefa_id",
                table: "comentarios",
                column: "tarefa_id");

            migrationBuilder.CreateIndex(
                name: "IX_historicos_tarefa_id",
                table: "historicos",
                column: "tarefa_id");

            migrationBuilder.CreateIndex(
                name: "IX_historicos_usuario_id",
                table: "historicos",
                column: "usuario_id");

            migrationBuilder.CreateIndex(
                name: "IX_projetos_criador_do_projeto_id",
                table: "projetos",
                column: "criador_do_projeto_id");

            migrationBuilder.CreateIndex(
                name: "IX_tarefas_projeto_id",
                table: "tarefas",
                column: "projeto_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "comentarios");

            migrationBuilder.DropTable(
                name: "historicos");

            migrationBuilder.DropTable(
                name: "tarefas");

            migrationBuilder.DropTable(
                name: "projetos");

            migrationBuilder.DropTable(
                name: "usuarios");
        }
    }
}
