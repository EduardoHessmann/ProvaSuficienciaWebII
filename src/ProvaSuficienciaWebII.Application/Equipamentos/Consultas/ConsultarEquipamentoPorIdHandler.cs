using MediatR;
using Microsoft.EntityFrameworkCore;
using ProvaSuficienciaWebII.Application.Equipamentos.Mapeadores;
using ProvaSuficienciaWebII.Contracts.Equipamentos.Dto;
using ProvaSuficienciaWebII.Infrastructure;

namespace ProvaSuficienciaWebII.Application.Equipamentos.Consultas;

/// <summary>
/// Handler da consulta de equipamento por ID.
/// </summary>
public class ConsultarEquipamentoPorIdHandler(ContextoBancoDados contexto) : IRequestHandler<ConsultarEquipamentoPorId, EquipamentoDto?>
{
    public async Task<EquipamentoDto?> Handle(ConsultarEquipamentoPorId consulta, CancellationToken cancellationToken)
    {
        var equipamento = await contexto.Equipamentos
            .AsNoTracking()
            .Include(e => e.Tipo)
            .FirstOrDefaultAsync(e => e.Id == consulta.Id, cancellationToken);

        return equipamento is null ? null : MapearEquipamento.ParaDto(equipamento);
    }
}
