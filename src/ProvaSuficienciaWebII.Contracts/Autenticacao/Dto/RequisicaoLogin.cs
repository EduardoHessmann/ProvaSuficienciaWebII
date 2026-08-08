using System.ComponentModel.DataAnnotations;

namespace ProvaSuficienciaWebII.Contracts.Autenticacao.Dto;

/// <summary>
/// Credenciais para autenticação na API.
/// </summary>
public class RequisicaoLogin
{
    [Required(ErrorMessage = "O e-mail é obrigatório.")]
    [EmailAddress(ErrorMessage = "O e-mail informado não é válido.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "A senha é obrigatória.")]
    public string Senha { get; set; } = string.Empty;
}
