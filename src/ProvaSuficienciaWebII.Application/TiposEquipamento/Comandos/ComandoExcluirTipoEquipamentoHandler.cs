using MediatR;
using Microsoft.EntityFrameworkCore;
using ProvaSuficienciaWebII.Application.Comum.Excecoes;
using ProvaSuficienciaWebII.Infrastructure;

namespace ProvaSuficienciaWebII.Application.TiposEquipamento.Comandos;

/// <summary>
/// Handler da exclusão de tipo de equipamento: impede a remoção quando o tipo
/// ainda está associado a algum equipamento.
/// </summary>
public class ComandoExcluirTipoEquipamentoHandler(ContextoBancoDados contexto) : IRequestHandler<ComandoExcluirTipoEquipamento, bool>
{
    public async Task<bool> Handle(ComandoExcluirTipoEquipamento comando, CancellationToken cancellationToken)
    {
        var tipo = await contexto.TiposEquipamento
            .FirstOrDefaultAsync(t => t.Id == comando.Id, cancellationToken);

        if (tipo is null)
        {
            return false;
        }

        var emUso = await contexto.Equipamentos
            .AnyAsync(e => e.TipoEquipamentoId == tipo.Id, cancellationToken);

        if (emUso)
        {
            throw new ExcecaoTipoEquipamentoEmUso();
        }

        contexto.TiposEquipamento.Remove(tipo);
        await contexto.SaveChangesAsync(cancellationToken);

        return true;
    }
}
