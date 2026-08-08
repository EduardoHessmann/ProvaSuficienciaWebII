namespace ProvaSuficienciaWebII.Contracts.Equipamentos.Dto;

/// <summary>
/// Envelope da listagem de equipamentos, no formato definido pelo enunciado
/// da prova: { "equipamentos": [ ... ] }.
/// </summary>
public class RespostaListaEquipamentos
{
    public List<EquipamentoDto> Equipamentos { get; set; } = [];
}
