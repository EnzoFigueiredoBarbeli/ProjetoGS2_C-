using Microsoft.EntityFrameworkCore;
using ProjetoGS.ApiService.Models;

namespace ProjetoGS.ApiService.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Tecnologia> Tecnologias => Set<Tecnologia>();
    public DbSet<Categoria>  Categorias  => Set<Categoria>();
    public DbSet<Usuario>    Usuarios    => Set<Usuario>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Seed: Categorias
        modelBuilder.Entity<Categoria>().HasData(
            new Categoria { Id = 1, Nome = "Saúde",       Descricao = "Tecnologias aplicadas à medicina e bem-estar" },
            new Categoria { Id = 2, Nome = "Agricultura",  Descricao = "Inovações para o agronegócio e produção de alimentos" },
            new Categoria { Id = 3, Nome = "Consumo",      Descricao = "Produtos para o mercado consumidor" },
            new Categoria { Id = 4, Nome = "Meio Ambiente",Descricao = "Soluções para monitoramento e sustentabilidade" },
            new Categoria { Id = 5, Nome = "Comunicação",  Descricao = "Conectividade e telecomunicações" }
        );

        // Seed: Tecnologias
        modelBuilder.Entity<Tecnologia>().HasData(
            new Tecnologia { Id = 1, Nome = "Espuma Viscoelástica", Descricao = "Desenvolvida pela NASA para absorção de impactos em assentos de naves.", OrigemMissao = "NASA - Anos 70", AnoDesenvolvimento = new DateTime(1970, 1, 1), CategoriaId = 3 },
            new Tecnologia { Id = 2, Nome = "Purificador de Água",  Descricao = "Sistema de filtragem iônica criado para missões tripuladas.", OrigemMissao = "Apollo", AnoDesenvolvimento = new DateTime(1968, 1, 1), CategoriaId = 2 },
            new Tecnologia { Id = 3, Nome = "Sensor de Imagem CMOS",Descricao = "Miniaturização de câmeras para uso em smartphones.", OrigemMissao = "Programa Espacial", AnoDesenvolvimento = new DateTime(1990, 1, 1), CategoriaId = 3 },
            new Tecnologia { Id = 4, Nome = "Monitoramento Climático por Satélite", Descricao = "Redes de satélites para previsão do tempo e monitoramento ambiental.", OrigemMissao = "ISS", AnoDesenvolvimento = new DateTime(2000, 1, 1), CategoriaId = 4 },
            new Tecnologia { Id = 5, Nome = "Agronegócio de Precisão", Descricao = "GPS e dados satelitais aplicados à agricultura.", OrigemMissao = "GPS / NAVSTAR", AnoDesenvolvimento = new DateTime(1995, 1, 1), CategoriaId = 2 }
        );

        // Seed: Admin (BCrypt hash de "admin123")
        modelBuilder.Entity<Usuario>().HasData(
            new Usuario
            {
                Id = 1,
                Nome = "Administrador",
                Email = "admin@projetogs.com",
                SenhaHash = BCrypt.Net.BCrypt.HashPassword("admin123"),
                Perfil = "Administrador",
                DataCadastro = new DateTime(2026, 1, 1)
            },
            new Usuario
            {
                Id = 2,
                Nome = "Pesquisador Demo",
                Email = "pesquisador@projetogs.com",
                SenhaHash = BCrypt.Net.BCrypt.HashPassword("pesq123"),
                Perfil = "Pesquisador",
                DataCadastro = new DateTime(2026, 1, 1)
            }
        );
    }
}
