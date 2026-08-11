using System.ComponentModel.DataAnnotations;

namespace ProvaSuficienciaWebII.Contracts.Equipamentos.Dto;

/// <summary>
/// Dados para edição parcial de um tipo de equipamento: somente os campos
/// enviados são alterados.
/// </summary>
public class RequisicaoEditarTipoEquipamento
{
    [StringLength(100, MinimumLength = 1, ErrorMessage = "O nome deve ter entre {2} e {1} caracteres.")]
    public string? Nome { get; set; }
}
