using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoGS.ApiService.Models;

[Table("Categorias")]
public class Categoria
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string Nome { get; set; } = string.Empty; // Ex: Saúde, Agricultura, Consumo

    [MaxLength(500)]
    public string Descricao { get; set; } = string.Empty;

    public ICollection<Tecnologia> Tecnologias { get; set; } = new List<Tecnologia>();
}
