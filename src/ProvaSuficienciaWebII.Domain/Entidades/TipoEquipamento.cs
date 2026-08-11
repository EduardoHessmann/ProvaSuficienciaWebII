using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProvaSuficienciaWebII.Domain.Entidades;

/// <summary>
/// Tipo de um equipamento (ex.: Computador, Audiovisual, Impressora).
/// Tabela no plural, conforme a nomenclatura padrão de banco de dados.
/// </summary>
[Table("tipos_equipamento")]
public class TipoEquipamento
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Nome { get; set; } = string.Empty;
}
