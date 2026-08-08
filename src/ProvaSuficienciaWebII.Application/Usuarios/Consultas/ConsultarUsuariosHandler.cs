using MediatR;
using Microsoft.EntityFrameworkCore;
using ProvaSuficienciaWebII.Contracts.Usuarios.Dto;
using ProvaSuficienciaWebII.Infrastructure;

namespace ProvaSuficienciaWebII.Application.Usuarios.Consultas;

/// <summary>
/// Handler da consulta de usuários: lista todos os cadastrados, sem expor as senhas.
/// </summary>
public class ConsultarUsuariosHandler(ContextoBancoDados contexto) : IRequestHandler<ConsultarUsuarios, List<UsuarioDto>>
{
    public async Task<List<UsuarioDto>> Handle(ConsultarUsuarios consulta, CancellationToken cancellationToken)
    {
        return await contexto.Usuarios
            .OrderBy(u => u.Id)
            .Select(u => new UsuarioDto
            {
                Id = u.Id,
                Nome = u.Nome,
                Email = u.Email
            })
            .ToListAsync(cancellationToken);
    }
}
