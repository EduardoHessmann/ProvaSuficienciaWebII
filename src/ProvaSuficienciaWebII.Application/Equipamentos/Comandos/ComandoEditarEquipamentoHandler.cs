using MediatR;
using Microsoft.EntityFrameworkCore;
using ProvaSuficienciaWebII.Application.Equipamentos.Mapeadores;
using ProvaSuficienciaWebII.Contracts.Equipamentos.Dto;
using ProvaSuficienciaWebII.Domain.Entidades;
using ProvaSuficienciaWebII.Infrastructure;

namespace ProvaSuficienciaWebII.Application.Equipamentos.Comandos;

/// <summary>
/// Handler da edição parcial de equipamento, conforme o enunciado da prova:
/// somente as informações enviadas são alteradas; as demais permanecem idênticas.
/// </summary>
public class ComandoEditarEquipamentoHandler(ContextoBancoDados contexto) : IRequestHandler<ComandoEditarEquipamento, EquipamentoDto?>
{
    public async Task<EquipamentoDto?> Handle(ComandoEditarEquipamento comando, CancellationToken cancellationToken)
    {
        var equipamento = await contexto.Equipamentos
            .Include(e => e.Tipo)
            .FirstOrDefaultAsync(e => e.Id == comando.Id, cancellationToken);

        if (equipamento is null)
        {
            return null;
        }

        if (comando.Dados.Nome is not null)
        {
            equipamento.Nome = comando.Dados.Nome;
        }

        if (comando.Dados.Tipo is not null)
        {
            equipamento.Tipo = await ObterOuCriarTipo(comando.Dados.Tipo, cancellationToken);
        }

        await contexto.SaveChangesAsync(cancellationToken);

        return MapearEquipamento.ParaDto(equipamento);
    }

    /// <summary>
    /// Busca o tipo pelo ID informado; quando não existe, cria um novo com o nome recebido.
    /// </summary>
    private async Task<TipoEquipamento> ObterOuCriarTipo(RequisicaoTipoEquipamento dto, CancellationToken cancellationToken)
    {
        var existente = await contexto.TiposEquipamento
            .FirstOrDefaultAsync(t => t.Id == dto.Id, cancellationToken);

        if (existente is not null)
        {
            return existente;
        }

        var novoTipo = new TipoEquipamento { Nome = dto.Nome };
        contexto.TiposEquipamento.Add(novoTipo);

        return novoTipo;
    }
}
