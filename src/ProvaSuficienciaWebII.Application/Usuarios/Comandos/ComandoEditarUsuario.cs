using MediatR;
using ProvaSuficienciaWebII.Contracts.Usuarios.Dto;

namespace ProvaSuficienciaWebII.Application.Usuarios.Comandos;

/// <summary>
/// Dados para edição parcial de um usuário: somente os campos enviados são
/// alterados. Retorna nulo quando o usuário não existe.
/// </summary>
public record ComandoEditarUsuario(int Id, RequisicaoEditarUsuario Dados) : IRequest<UsuarioDto?>;
