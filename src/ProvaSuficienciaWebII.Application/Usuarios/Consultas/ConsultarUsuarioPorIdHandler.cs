using MediatR;
using Microsoft.EntityFrameworkCore;
using ProvaSuficienciaWebII.Application.Usuarios.Mapeadores;
using ProvaSuficienciaWebII.Contracts.Usuarios.Dto;
using ProvaSuficienciaWebII.Infrastructure;

namespace ProvaSuficienciaWebII.Application.Usuarios.Consultas;

/// <summary>
/// Handler da consulta de usuário por ID.
/// </summary>
public class ConsultarUsuarioPorIdHandler(ContextoBancoDados contexto) : IRequestHandler<ConsultarUsuarioPorId, UsuarioDto?>
{
    public async Task<UsuarioDto?> Handle(ConsultarUsuarioPorId consulta, CancellationToken cancellationToken)
    {
        var usuario = await contexto.Usuarios
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == consulta.Id, cancellationToken);

        return usuario is null ? null : MapearUsuario.ParaDto(usuario);
    }
}
