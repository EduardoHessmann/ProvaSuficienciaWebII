using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ProvaSuficienciaWebII.Domain.Entidades;

namespace ProvaSuficienciaWebII.Infrastructure.Autenticacao;

/// <summary>
/// Gera tokens JWT assinados com HMAC-SHA256 a partir das configurações da seção "Jwt".
/// </summary>
public class GeradorTokenJwt(IOptions<ConfiguracoesJwt> opcoes) : IGeradorTokenJwt
{
    private readonly ConfiguracoesJwt _configuracoes = opcoes.Value;

    public TokenGerado GerarToken(Usuario usuario)
    {
        var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuracoes.Chave));
        var credenciais = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);
        var expiraEm = DateTime.UtcNow.AddMinutes(_configuracoes.ExpiracaoMinutos);

        var declaracoes = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, usuario.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Name, usuario.Nome),
            new Claim(JwtRegisteredClaimNames.Email, usuario.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            issuer: _configuracoes.Emissor,
            audience: _configuracoes.Publico,
            claims: declaracoes,
            expires: expiraEm,
            signingCredentials: credenciais);

        return new TokenGerado(new JwtSecurityTokenHandler().WriteToken(token), expiraEm);
    }
}
