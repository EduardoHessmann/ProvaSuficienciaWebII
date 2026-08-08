using System.ComponentModel.DataAnnotations;

namespace ProvaSuficienciaWebII.Contracts.Equipamentos.Dto;

/// <summary>
/// Dados necessários para cadastrar um novo equipamento.
/// </summary>
public class RequisicaoNovoEquipamento
{
    [Required(ErrorMessage = "O nome é obrigatório.")]
    [StringLength(100, ErrorMessage = "O nome deve ter no máximo {1} caracteres.")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "O tipo é obrigatório.")]
    public RequisicaoTipoEquipamento Tipo { get; set; } = null!;
}
