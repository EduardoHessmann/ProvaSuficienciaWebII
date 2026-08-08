namespace ProvaSuficienciaWebII.Domain.Entidades;

/// <summary>
/// Equipamento cadastrado no sistema, sempre associado a um tipo.
/// </summary>
public class Equipamento
{
    public int Id { get; set; }

    public string Nome { get; set; } = string.Empty;

    public int TipoEquipamentoId { get; set; }

    public TipoEquipamento Tipo { get; set; } = null!;
}
