using System.ComponentModel.DataAnnotations;

namespace ProvaSuficienciaWebII.Contracts.Equipamentos.Dto;

/// <summary>
/// Dados necessários para cadastrar um novo tipo de equipamento.
/// </summary>
public class RequisicaoNovoTipoEquipamento
{
    [Required(ErrorMessage = "O nome é obrigatório.")]
    [StringLength(100, ErrorMessage = "O nome deve ter no máximo {1} caracteres.")]
    public string Nome { get; set; } = string.Empty;
}
