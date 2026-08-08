using System.ComponentModel.DataAnnotations;

namespace ProvaSuficienciaWebII.Contracts.Equipamentos.Dto;

/// <summary>
/// Tipo informado ao cadastrar ou editar um equipamento.
/// Quando o ID corresponde a um tipo existente, ele é reaproveitado;
/// caso contrário, um novo tipo é criado com o nome informado.
/// </summary>
public class RequisicaoTipoEquipamento
{
    [Range(0, int.MaxValue, ErrorMessage = "O id do tipo não pode ser negativo.")]
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome do tipo é obrigatório.")]
    [StringLength(100, ErrorMessage = "O nome do tipo deve ter no máximo {1} caracteres.")]
    public string Nome { get; set; } = string.Empty;
}
