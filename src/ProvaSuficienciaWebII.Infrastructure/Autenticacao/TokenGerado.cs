namespace ProvaSuficienciaWebII.Infrastructure.Autenticacao;

/// <summary>
/// Resultado da geração de um token JWT.
/// </summary>
/// <param name="Token">O token JWT assinado.</param>
/// <param name="ExpiraEm">Data e hora (UTC) de expiração do token.</param>
public record TokenGerado(string Token, DateTime ExpiraEm);
