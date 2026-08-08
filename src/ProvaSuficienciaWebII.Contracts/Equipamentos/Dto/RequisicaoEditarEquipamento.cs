using System.ComponentModel.DataAnnotations;

namespace ProvaSuficienciaWebII.Contracts.Equipamentos.Dto;

/// <summary>
/// Dados para edição parcial de um equipamento, conforme o enunciado da prova:
/// somente as informações alteradas precisam ser enviadas; campos omitidos
/// permanecem idênticos.
/// </summary>
public class RequisicaoEditarEquipamento
{
    [StringLength(100, ErrorMessage = "O nome deve ter no máximo {1} caracteres.")]
    public string? Nome { get; set; }

    public RequisicaoTipoEquipamento? Tipo { get; set; }
}
