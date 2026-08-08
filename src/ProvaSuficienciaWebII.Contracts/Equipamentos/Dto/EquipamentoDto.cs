namespace ProvaSuficienciaWebII.Contracts.Equipamentos.Dto;

/// <summary>
/// Representação de um equipamento retornada pela API, com o tipo aninhado,
/// no formato definido pelo enunciado da prova.
/// </summary>
public class EquipamentoDto
{
    public int Id { get; set; }

    public string Nome { get; set; } = string.Empty;

    public TipoEquipamentoDto Tipo { get; set; } = new();
}
