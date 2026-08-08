using MediatR;
using Microsoft.EntityFrameworkCore;
using ProvaSuficienciaWebII.Application.TiposEquipamento.Mapeadores;
using ProvaSuficienciaWebII.Contracts.Equipamentos.Dto;
using ProvaSuficienciaWebII.Infrastructure;

namespace ProvaSuficienciaWebII.Application.TiposEquipamento.Consultas;

/// <summary>
/// Handler da consulta de tipo de equipamento por ID.
/// </summary>
public class ConsultarTipoEquipamentoPorIdHandler(ContextoBancoDados contexto) : IRequestHandler<ConsultarTipoEquipamentoPorId, TipoEquipamentoDto?>
{
    public async Task<TipoEquipamentoDto?> Handle(ConsultarTipoEquipamentoPorId consulta, CancellationToken cancellationToken)
    {
        var tipo = await contexto.TiposEquipamento
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.Id == consulta.Id, cancellationToken);

        return tipo is null ? null : MapearTipoEquipamento.ParaDto(tipo);
    }
}
