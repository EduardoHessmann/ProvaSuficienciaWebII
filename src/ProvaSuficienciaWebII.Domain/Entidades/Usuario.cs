using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProvaSuficienciaWebII.Domain.Entidades;

/// <summary>
/// Usuário do sistema, utilizado para autenticação na API.
/// Tabela no plural, conforme a nomenclatura padrão de banco de dados.
/// </summary>
[Table("usuarios")]
[Index(nameof(Email), IsUnique = true)]
public class Usuario
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Nome { get; set; } = string.Empty;

    [Required]
    [MaxLength(150)]
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Hash da senha do usuário. A senha em texto puro nunca é armazenada.
    /// </summary>
    [Required]
    public string SenhaHash { get; set; } = string.Empty;
}
