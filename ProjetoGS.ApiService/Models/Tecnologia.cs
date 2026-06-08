using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoGS.ApiService.Models;

[Table("Tecnologias")]
public class Tecnologia
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(200)]
    public string Nome { get; set; } = string.Empty;

    [MaxLength(1000)]
    public string Descricao { get; set; } = string.Empty;

    [MaxLength(200)]
    public string OrigemMissao { get; set; } = string.Empty;


    public DateTime AnoDesenvolvimento { get; set; }

    public DateTime DataCadastro { get; set; } = DateTime.UtcNow;

    // FK para Categoria
    public int CategoriaId { get; set; }
    public Categoria? Categoria { get; set; }
}
