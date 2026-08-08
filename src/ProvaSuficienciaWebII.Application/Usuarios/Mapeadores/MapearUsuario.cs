using ProvaSuficienciaWebII.Contracts.Usuarios.Dto;
using ProvaSuficienciaWebII.Domain.Entidades;

namespace ProvaSuficienciaWebII.Application.Usuarios.Mapeadores;

/// <summary>
/// Conversões da entidade <see cref="Usuario"/> para os DTOs da API.
/// </summary>
public static class MapearUsuario
{
    public static UsuarioDto ParaDto(Usuario usuario)
    {
        return new UsuarioDto
        {
            Id = usuario.Id,
            Nome = usuario.Nome,
            Email = usuario.Email
        };
    }
}
