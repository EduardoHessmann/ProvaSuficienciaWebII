using System.ComponentModel.DataAnnotations;

namespace ProvaSuficienciaWebII.Contracts.Usuarios.Dto;

/// <summary>
/// Dados necessários para cadastrar um novo usuário.
/// </summary>
public class RequisicaoNovoUsuario
{
    [Required(ErrorMessage = "O nome é obrigatório.")]
    [StringLength(100, ErrorMessage = "O nome deve ter no máximo {1} caracteres.")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "O e-mail é obrigatório.")]
    [EmailAddress(ErrorMessage = "O e-mail informado não é válido.")]
    [StringLength(150, ErrorMessage = "O e-mail deve ter no máximo {1} caracteres.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "A senha é obrigatória.")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "A senha deve ter entre {2} e {1} caracteres.")]
    public string Senha { get; set; } = string.Empty;
}
