namespace ProvaSuficienciaWebII.Contracts.Equipamentos.Dto;

/// <summary>
/// Representação do tipo de um equipamento retornada pela API.
/// </summary>
public class TipoEquipamentoDto
{
    public int Id { get; set; }

    public string Nome { get; set; } = string.Empty;
}
