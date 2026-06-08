using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoGS.ApiService.Migrations;

public partial class InitialCreate : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Categorias",
            columns: table => new
            {
                Id        = table.Column<int>(nullable: false).Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                Nome      = table.Column<string>(maxLength: 100, nullable: false),
                Descricao = table.Column<string>(maxLength: 500, nullable: false, defaultValue: "")
            },
            constraints: table => table.PrimaryKey("PK_Categorias", x => x.Id));

        migrationBuilder.CreateTable(
            name: "Usuarios",
            columns: table => new
            {
                Id           = table.Column<int>(nullable: false).Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                Nome         = table.Column<string>(maxLength: 100, nullable: false),
                Email        = table.Column<string>(maxLength: 200, nullable: false),
                SenhaHash    = table.Column<string>(nullable: false),
                Perfil       = table.Column<string>(maxLength: 50, nullable: false, defaultValue: "Pesquisador"),
                DataCadastro = table.Column<DateTime>(nullable: false)
            },
            constraints: table => table.PrimaryKey("PK_Usuarios", x => x.Id));

        migrationBuilder.CreateTable(
            name: "Tecnologias",
            columns: table => new
            {
                Id                 = table.Column<int>(nullable: false).Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                Nome               = table.Column<string>(maxLength: 200, nullable: false),
                Descricao          = table.Column<string>(maxLength: 1000, nullable: false, defaultValue: ""),
                OrigemMissao       = table.Column<string>(maxLength: 200, nullable: false, defaultValue: ""),
                AnoDesenvolvimento = table.Column<DateTime>(nullable: false),
                DataCadastro       = table.Column<DateTime>(nullable: false),
                CategoriaId        = table.Column<int>(nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Tecnologias", x => x.Id);
                table.ForeignKey("FK_Tecnologias_Categorias_CategoriaId", x => x.CategoriaId, "Categorias", "Id", onDelete: ReferentialAction.Restrict);
            });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable("Tecnologias");
        migrationBuilder.DropTable("Usuarios");
        migrationBuilder.DropTable("Categorias");
    }
}
