using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reski.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ObjetivoCsharp",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Cargo = table.Column<string>(type: "NVARCHAR2(255)", maxLength: 255, nullable: false),
                    Area = table.Column<string>(type: "NVARCHAR2(255)", maxLength: 255, nullable: false),
                    Descricao = table.Column<string>(type: "NCLOB", maxLength: 4000, nullable: false),
                    Demanda = table.Column<string>(type: "NVARCHAR2(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObjetivoCsharp", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TrilhaCsharp",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Status = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    Conteudo = table.Column<string>(type: "NCLOB", maxLength: 4000, nullable: false),
                    Competencia = table.Column<string>(type: "NVARCHAR2(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrilhaCsharp", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioCsharp",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome = table.Column<string>(type: "NVARCHAR2(120)", maxLength: 120, nullable: false),
                    Email = table.Column<string>(type: "NVARCHAR2(254)", maxLength: 254, nullable: false),
                    Cpf = table.Column<string>(type: "NVARCHAR2(11)", maxLength: 11, nullable: false),
                    SenhaHash = table.Column<string>(type: "NVARCHAR2(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioCsharp", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "UK_UsuarioCsharp_Cpf",
                table: "UsuarioCsharp",
                column: "Cpf",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UK_UsuarioCsharp_Email",
                table: "UsuarioCsharp",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ObjetivoCsharp");

            migrationBuilder.DropTable(
                name: "TrilhaCsharp");

            migrationBuilder.DropTable(
                name: "UsuarioCsharp");
        }
    }
}
