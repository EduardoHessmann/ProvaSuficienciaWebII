using MediatR;
using ProvaSuficienciaWebII.Contracts.Usuarios.Dto;

namespace ProvaSuficienciaWebII.Application.Usuarios.Consultas;

/// <summary>
/// Representa a consulta de um usuário por ID.
/// Retorna nulo quando o usuário não existe.
/// </summary>
public record ConsultarUsuarioPorId(int Id) : IRequest<UsuarioDto?>;
