using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProvaSuficienciaWebII.Contracts.Autenticacao.Dto;
using ProvaSuficienciaWebII.Domain.Entidades;
using ProvaSuficienciaWebII.Infrastructure;
using ProvaSuficienciaWebII.Infrastructure.Autenticacao;

namespace ProvaSuficienciaWebII.Application.Autenticacao.Comandos;

/// <summary>
/// Handler do login: valida e-mail e senha e gera o token JWT em caso de sucesso.
/// </summary>
public class ComandoLoginUsuarioHandler(ContextoBancoDados contexto, IPasswordHasher<Usuario> hasherSenha, IGeradorTokenJwt geradorToken) : IRequestHandler<ComandoLoginUsuario, RespostaLogin?>
{
    public async Task<RespostaLogin?> Handle(ComandoLoginUsuario comando, CancellationToken cancellationToken)
    {
        var usuario = await contexto.Usuarios
            .FirstOrDefaultAsync(u => u.Email == comando.Dados.Email, cancellationToken);

        if (usuario is null)
        {
            return null;
        }

        var resultado = hasherSenha.VerifyHashedPassword(usuario, usuario.SenhaHash, comando.Dados.Senha);

        if (resultado == PasswordVerificationResult.Failed)
        {
            return null;
        }

        var tokenGerado = geradorToken.GerarToken(usuario);

        return new RespostaLogin
        {
            Token = tokenGerado.Token,
            ExpiraEm = tokenGerado.ExpiraEm
        };
    }
}
