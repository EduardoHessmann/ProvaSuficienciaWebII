using MediatR;
using Microsoft.EntityFrameworkCore;
using ProvaSuficienciaWebII.Application.Equipamentos.Mapeadores;
using ProvaSuficienciaWebII.Contracts.Equipamentos.Dto;
using ProvaSuficienciaWebII.Infrastructure;

namespace ProvaSuficienciaWebII.Application.Equipamentos.Consultas;

/// <summary>
/// Handler da consulta de equipamentos: lista todos, com o tipo aninhado.
/// </summary>
public class ConsultarEquipamentosHandler(ContextoBancoDados contexto) : IRequestHandler<ConsultarEquipamentos, RespostaListaEquipamentos>
{
    public async Task<RespostaListaEquipamentos> Handle(ConsultarEquipamentos consulta, CancellationToken cancellationToken)
    {
        var equipamentos = await contexto.Equipamentos
            .AsNoTracking()
            .Include(e => e.Tipo)
            .OrderBy(e => e.Id)
            .ToListAsync(cancellationToken);

        return new RespostaListaEquipamentos
        {
            Equipamentos = equipamentos.Select(MapearEquipamento.ParaDto).ToList()
        };
    }
}
