using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProvaSuficienciaWebII.Application.Comum.Excecoes;
using ProvaSuficienciaWebII.Application.Usuarios.Mapeadores;
using ProvaSuficienciaWebII.Contracts.Usuarios.Dto;
using ProvaSuficienciaWebII.Domain.Entidades;
using ProvaSuficienciaWebII.Infrastructure;

namespace ProvaSuficienciaWebII.Application.Usuarios.Comandos;

/// <summary>
/// Handler da edição parcial de usuário: altera somente os campos enviados,
/// garantindo que o e-mail continue único.
/// </summary>
public class ComandoEditarUsuarioHandler(ContextoBancoDados contexto, IPasswordHasher<Usuario> hasherSenha) : IRequestHandler<ComandoEditarUsuario, UsuarioDto?>
{
    public async Task<UsuarioDto?> Handle(ComandoEditarUsuario comando, CancellationToken cancellationToken)
    {
        var usuario = await contexto.Usuarios
            .FirstOrDefaultAsync(u => u.Id == comando.Id, cancellationToken);

        if (usuario is null)
            return null;

        if (comando.Dados.Email is not null && comando.Dados.Email != usuario.Email)
        {
            var emailEmUso = await contexto.Usuarios
                .AnyAsync(u => u.Email == comando.Dados.Email && u.Id != usuario.Id, cancellationToken);

            if (emailEmUso)
                throw new ExcecaoEmailJaCadastrado();

            usuario.Email = comando.Dados.Email;
        }

        if (comando.Dados.Nome is not null)
            usuario.Nome = comando.Dados.Nome;

        if (comando.Dados.Senha is not null)
            usuario.SenhaHash = hasherSenha.HashPassword(usuario, comando.Dados.Senha);

        await contexto.SaveChangesAsync(cancellationToken);

        return MapearUsuario.ParaDto(usuario);
    }
}
