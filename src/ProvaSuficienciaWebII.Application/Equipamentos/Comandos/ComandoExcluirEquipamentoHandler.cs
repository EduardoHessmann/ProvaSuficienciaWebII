using MediatR;
using Microsoft.EntityFrameworkCore;
using ProvaSuficienciaWebII.Infrastructure;

namespace ProvaSuficienciaWebII.Application.Equipamentos.Comandos;

/// <summary>
/// Handler da exclusão de equipamento.
/// </summary>
public class ComandoExcluirEquipamentoHandler(ContextoBancoDados contexto) : IRequestHandler<ComandoExcluirEquipamento, bool>
{
    public async Task<bool> Handle(ComandoExcluirEquipamento comando, CancellationToken cancellationToken)
    {
        var equipamento = await contexto.Equipamentos
            .FirstOrDefaultAsync(e => e.Id == comando.Id, cancellationToken);

        if (equipamento is null)
            return false;

        contexto.Equipamentos.Remove(equipamento);
        await contexto.SaveChangesAsync(cancellationToken);

        return true;
    }
}
