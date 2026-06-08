namespace ProjetoGS.Web.Models;

public class TecnologiaViewModel
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public string OrigemMissao { get; set; } = string.Empty;
    public DateTime AnoDesenvolvimento { get; set; }
    public DateTime DataCadastro { get; set; }
    public int CategoriaId { get; set; }
    public string? CategoriaNome { get; set; }
}

public class CategoriaViewModel
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
}



public class StatsViewModel
{
    public int TotalTecnologias { get; set; }
    public int TotalMissoes { get; set; }
    public int TotalSetores { get; set; }
    public List<CategoriaStatViewModel> PorCategoria { get; set; } = new();
    public List<TecnologiaViewModel> Recentes { get; set; } = new();
}

public class CategoriaStatViewModel
{
    public string Categoria { get; set; } = string.Empty;
    public int Total { get; set; }
}

public class LoginViewModel
{
    public string Email { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;
}

public class CadastroViewModel
{
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;
}
