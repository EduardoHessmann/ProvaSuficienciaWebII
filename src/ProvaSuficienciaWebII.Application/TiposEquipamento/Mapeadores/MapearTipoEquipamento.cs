using ProvaSuficienciaWebII.Contracts.Equipamentos.Dto;
using ProvaSuficienciaWebII.Domain.Entidades;

namespace ProvaSuficienciaWebII.Application.TiposEquipamento.Mapeadores;

/// <summary>
/// Conversões da entidade <see cref="TipoEquipamento"/> para os DTOs da API.
/// </summary>
public static class MapearTipoEquipamento
{
    public static TipoEquipamentoDto ParaDto(TipoEquipamento tipo)
    {
        return new TipoEquipamentoDto
        {
            Id = tipo.Id,
            Nome = tipo.Nome
        };
    }
}
