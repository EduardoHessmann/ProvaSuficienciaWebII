namespace ProvaSuficienciaWebII.Infrastructure.Autenticacao;

/// <summary>
/// Configurações de geração e validação de tokens JWT, lidas da seção "Jwt" do appsettings.
/// </summary>
public class ConfiguracoesJwt
{
    public const string Secao = "Jwt";

    /// <summary>
    /// Chave secreta utilizada para assinar os tokens (mínimo de 32 caracteres para HMAC-SHA256).
    /// </summary>
    public string Chave { get; set; } = string.Empty;

    public string Emissor { get; set; } = string.Empty;

    public string Publico { get; set; } = string.Empty;

    public int ExpiracaoMinutos { get; set; } = 60;
}
