using MediatR;
using Microsoft.EntityFrameworkCore;
using ProvaSuficienciaWebII.Application.TiposEquipamento.Mapeadores;
using ProvaSuficienciaWebII.Contracts.Equipamentos.Dto;
using ProvaSuficienciaWebII.Infrastructure;

namespace ProvaSuficienciaWebII.Application.TiposEquipamento.Comandos;

/// <summary>
/// Handler da edição parcial de tipo de equipamento.
/// </summary>
public class ComandoEditarTipoEquipamentoHandler(ContextoBancoDados contexto) : IRequestHandler<ComandoEditarTipoEquipamento, TipoEquipamentoDto?>
{
    public async Task<TipoEquipamentoDto?> Handle(ComandoEditarTipoEquipamento comando, CancellationToken cancellationToken)
    {
        var tipo = await contexto.TiposEquipamento
            .FirstOrDefaultAsync(t => t.Id == comando.Id, cancellationToken);

        if (tipo is null)
            return null;

        if (comando.Dados.Nome is not null)
            tipo.Nome = comando.Dados.Nome;

        await contexto.SaveChangesAsync(cancellationToken);

        return MapearTipoEquipamento.ParaDto(tipo);
    }
}
