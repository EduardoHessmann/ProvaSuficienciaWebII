using System.ComponentModel.DataAnnotations;

namespace ProvaSuficienciaWebII.Contracts.Usuarios.Dto;

/// <summary>
/// Dados para edição parcial de um usuário: somente as informações alteradas
/// precisam ser enviadas; campos omitidos permanecem idênticos.
/// </summary>
public class RequisicaoEditarUsuario
{
    [StringLength(100, ErrorMessage = "O nome deve ter no máximo {1} caracteres.")]
    public string? Nome { get; set; }

    [EmailAddress(ErrorMessage = "O e-mail informado não é válido.")]
    [StringLength(150, ErrorMessage = "O e-mail deve ter no máximo {1} caracteres.")]
    public string? Email { get; set; }

    [StringLength(100, MinimumLength = 6, ErrorMessage = "A senha deve ter entre {2} e {1} caracteres.")]
    public string? Senha { get; set; }
}
