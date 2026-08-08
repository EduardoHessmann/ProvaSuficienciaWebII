using MediatR;
using ProvaSuficienciaWebII.Contracts.Usuarios.Dto;

namespace ProvaSuficienciaWebII.Application.Usuarios.Consultas;

/// <summary>
/// Representa a consulta de todos os usuários cadastrados.
/// </summary>
public record ConsultarUsuarios : IRequest<List<UsuarioDto>>;
