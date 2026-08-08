namespace ProvaSuficienciaWebII.Domain.Entidades;

/// <summary>
/// Usuário do sistema, utilizado para autenticação na API.
/// </summary>
public class Usuario
{
    public int Id { get; set; }

    public string Nome { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Hash da senha do usuário. A senha em texto puro nunca é armazenada.
    /// </summary>
    public string SenhaHash { get; set; } = string.Empty;
}
