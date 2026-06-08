using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoGS.ApiService.Models;

[Table("Usuarios")]
public class Usuario
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string Nome { get; set; } = string.Empty;

    [Required, MaxLength(200)]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string SenhaHash { get; set; } = string.Empty;

    [MaxLength(50)]
    public string Perfil { get; set; } = "Pesquisador"; 

    public DateTime DataCadastro { get; set; } = DateTime.UtcNow;
}
