using MediatR;
using ProvaSuficienciaWebII.Application.TiposEquipamento.Mapeadores;
using ProvaSuficienciaWebII.Contracts.Equipamentos.Dto;
using ProvaSuficienciaWebII.Domain.Entidades;
using ProvaSuficienciaWebII.Infrastructure;

namespace ProvaSuficienciaWebII.Application.TiposEquipamento.Comandos;

/// <summary>
/// Handler do cadastro de novo tipo de equipamento.
/// </summary>
public class ComandoNovoTipoEquipamentoHandler(ContextoBancoDados contexto) : IRequestHandler<ComandoNovoTipoEquipamento, TipoEquipamentoDto>
{
    public async Task<TipoEquipamentoDto> Handle(ComandoNovoTipoEquipamento comando, CancellationToken cancellationToken)
    {
        var tipo = new TipoEquipamento
        {
            Nome = comando.Dados.Nome
        };

        contexto.TiposEquipamento.Add(tipo);
        await contexto.SaveChangesAsync(cancellationToken);

        return MapearTipoEquipamento.ParaDto(tipo);
    }
}
