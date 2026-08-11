using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProvaSuficienciaWebII.Domain.Entidades;

/// <summary>
/// Equipamento cadastrado no sistema, sempre associado a um tipo.
/// Tabela no plural, conforme a nomenclatura padrão de banco de dados.
/// </summary>
[Table("equipamentos")]
public class Equipamento
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Nome { get; set; } = string.Empty;

    public int TipoEquipamentoId { get; set; }

    [ForeignKey(nameof(TipoEquipamentoId))]
    public TipoEquipamento Tipo { get; set; } = null!;
}
