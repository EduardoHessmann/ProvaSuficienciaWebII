using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProvaSuficienciaWebII.Application.Comum.Excecoes;
using ProvaSuficienciaWebII.Application.TiposEquipamento.Comandos;
using ProvaSuficienciaWebII.Application.TiposEquipamento.Consultas;
using ProvaSuficienciaWebII.Contracts.Equipamentos.Dto;

namespace ProvaSuficienciaWebII.Api.Controllers;

[ApiController]
[Route("tipos-equipamento")]
public class TiposEquipamentoController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Lista todos os tipos de equipamento.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(List<TipoEquipamentoDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Listar()
    {
        var resposta = await mediator.Send(new ConsultarTiposEquipamento());
        return Ok(resposta);
    }

    /// <summary>
    /// Consulta um tipo de equipamento pelo ID.
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(TipoEquipamentoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ConsultarPorId(int id)
    {
        var resposta = await mediator.Send(new ConsultarTipoEquipamentoPorId(id));

        if (resposta is null)
            return NotFound(new { erro = "Tipo de equipamento não encontrado." });

        return Ok(resposta);
    }

    /// <summary>
    /// Cadastra um novo tipo de equipamento. Exige autenticação via token JWT.
    /// </summary>
    [HttpPost]
    [Authorize]
    [ProducesResponseType(typeof(TipoEquipamentoDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Cadastrar(RequisicaoNovoTipoEquipamento requisicao)
    {
        var resposta = await mediator.Send(new ComandoNovoTipoEquipamento(requisicao));
        return Created($"/tipos-equipamento/{resposta.Id}", resposta);
    }

    /// <summary>
    /// Edita parcialmente um tipo de equipamento. Exige autenticação via token JWT.
    /// </summary>
    [HttpPut("{id:int}")]
    [Authorize]
    [ProducesResponseType(typeof(TipoEquipamentoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Editar(int id, RequisicaoEditarTipoEquipamento requisicao)
    {
        var resposta = await mediator.Send(new ComandoEditarTipoEquipamento(id, requisicao));

        if (resposta is null)
            return NotFound(new { erro = "Tipo de equipamento não encontrado." });

        return Ok(resposta);
    }

    /// <summary>
    /// Exclui um tipo de equipamento, desde que não esteja em uso por nenhum
    /// equipamento. Exige autenticação via token JWT.
    /// </summary>
    [HttpDelete("{id:int}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Excluir(int id)
    {
        try
        {
            var excluido = await mediator.Send(new ComandoExcluirTipoEquipamento(id));

            if (!excluido)
                return NotFound(new { erro = "Tipo de equipamento não encontrado." });

            return Ok(new { success = new { text = "tipo de equipamento removido" } });
        }
        catch (ExcecaoTipoEquipamentoEmUso excecao)
        {
            return Conflict(new { erro = excecao.Message });
        }
    }
}
