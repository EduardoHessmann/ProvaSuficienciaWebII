namespace ProvaSuficienciaWebII.Domain.Entidades;

/// <summary>
/// Tipo de um equipamento (ex.: Computador, Audiovisual, Impressora).
/// </summary>
public class TipoEquipamento
{
    public int Id { get; set; }

    public string Nome { get; set; } = string.Empty;
}
