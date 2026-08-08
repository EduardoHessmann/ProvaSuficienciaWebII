using MediatR;
using Microsoft.EntityFrameworkCore;
using ProvaSuficienciaWebII.Contracts.Equipamentos.Dto;
using ProvaSuficienciaWebII.Infrastructure;

namespace ProvaSuficienciaWebII.Application.TiposEquipamento.Consultas;

/// <summary>
/// Handler da consulta de tipos de equipamento.
/// </summary>
public class ConsultarTiposEquipamentoHandler(ContextoBancoDados contexto) : IRequestHandler<ConsultarTiposEquipamento, List<TipoEquipamentoDto>>
{
    public async Task<List<TipoEquipamentoDto>> Handle(ConsultarTiposEquipamento consulta, CancellationToken cancellationToken)
    {
        return await contexto.TiposEquipamento
            .AsNoTracking()
            .OrderBy(t => t.Id)
            .Select(t => new TipoEquipamentoDto
            {
                Id = t.Id,
                Nome = t.Nome
            })
            .ToListAsync(cancellationToken);
    }
}
