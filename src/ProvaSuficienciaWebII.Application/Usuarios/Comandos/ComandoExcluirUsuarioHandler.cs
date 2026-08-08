using MediatR;
using Microsoft.EntityFrameworkCore;
using ProvaSuficienciaWebII.Infrastructure;

namespace ProvaSuficienciaWebII.Application.Usuarios.Comandos;

/// <summary>
/// Handler da exclusão de usuário.
/// </summary>
public class ComandoExcluirUsuarioHandler(ContextoBancoDados contexto) : IRequestHandler<ComandoExcluirUsuario, bool>
{
    public async Task<bool> Handle(ComandoExcluirUsuario comando, CancellationToken cancellationToken)
    {
        var usuario = await contexto.Usuarios
            .FirstOrDefaultAsync(u => u.Id == comando.Id, cancellationToken);

        if (usuario is null)
            return false;

        contexto.Usuarios.Remove(usuario);
        await contexto.SaveChangesAsync(cancellationToken);

        return true;
    }
}
