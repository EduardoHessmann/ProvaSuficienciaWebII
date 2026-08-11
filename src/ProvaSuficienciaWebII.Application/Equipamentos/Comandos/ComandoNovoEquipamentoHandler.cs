using MediatR;
using Microsoft.EntityFrameworkCore;
using ProvaSuficienciaWebII.Application.Equipamentos.Mapeadores;
using ProvaSuficienciaWebII.Contracts.Equipamentos.Dto;
using ProvaSuficienciaWebII.Domain.Entidades;
using ProvaSuficienciaWebII.Infrastructure;

namespace ProvaSuficienciaWebII.Application.Equipamentos.Comandos;

/// <summary>
/// Handler do cadastro de novo equipamento. Se o tipo informado já existir,
/// ele é reaproveitado; caso contrário, um novo tipo é criado junto.
/// </summary>
public class ComandoNovoEquipamentoHandler(ContextoBancoDados contexto) : IRequestHandler<ComandoNovoEquipamento, EquipamentoDto>
{
    public async Task<EquipamentoDto> Handle(ComandoNovoEquipamento comando, CancellationToken cancellationToken)
    {
        var tipo = await ObterOuCriarTipo(comando.Dados.Tipo, cancellationToken);

        var equipamento = new Equipamento
        {
            Nome = comando.Dados.Nome,
            Tipo = tipo
        };

        contexto.Equipamentos.Add(equipamento);
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
