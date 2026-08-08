using MediatR;

namespace ProvaSuficienciaWebII.Application.Usuarios.Comandos;

/// <summary>
/// Comando para excluir um usuário.
/// Retorna falso quando o usuário não existe.
/// </summary>
public record ComandoExcluirUsuario(int Id) : IRequest<bool>;
