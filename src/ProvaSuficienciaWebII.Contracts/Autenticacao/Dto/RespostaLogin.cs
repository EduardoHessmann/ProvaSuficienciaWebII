namespace ProvaSuficienciaWebII.Contracts.Autenticacao.Dto;

/// <summary>
/// Token JWT retornado após autenticação bem-sucedida.
/// </summary>
public class RespostaLogin
{
    public string Token { get; set; } = string.Empty;

    /// <summary>
    /// Data e hora (UTC) em que o token expira.
    /// </summary>
    public DateTime ExpiraEm { get; set; }
}
