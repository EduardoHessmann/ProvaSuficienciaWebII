using ProvaSuficienciaWebII.Domain.Entidades;

namespace ProvaSuficienciaWebII.Infrastructure.Autenticacao;

/// <summary>
/// Serviço responsável pela geração de tokens JWT para usuários autenticados.
/// </summary>
public interface IGeradorTokenJwt
{
    TokenGerado GerarToken(Usuario usuario);
}
