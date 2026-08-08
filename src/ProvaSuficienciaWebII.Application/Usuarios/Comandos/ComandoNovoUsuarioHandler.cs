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
/// Handler do cadastro de novo usuário: garante e-mail único e armazena somente o hash da senha.
/// </summary>
public class ComandoNovoUsuarioHandler(ContextoBancoDados contexto, IPasswordHasher<Usuario> hasherSenha) : IRequestHandler<ComandoNovoUsuario, UsuarioDto>
{
    public async Task<UsuarioDto> Handle(ComandoNovoUsuario comando, CancellationToken cancellationToken)
    {
        var emailJaCadastrado = await contexto.Usuarios
            .AnyAsync(u => u.Email == comando.Dados.Email, cancellationToken);

        if (emailJaCadastrado)
            throw new ExcecaoEmailJaCadastrado();

        var usuario = new Usuario
        {
            Nome = comando.Dados.Nome,
            Email = comando.Dados.Email
        };

        usuario.SenhaHash = hasherSenha.HashPassword(usuario, comando.Dados.Senha);

        contexto.Usuarios.Add(usuario);
        await contexto.SaveChangesAsync(cancellationToken);

        return MapearUsuario.ParaDto(usuario);
    }
}
