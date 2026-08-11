using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProvaSuficienciaWebII.Application.Equipamentos.Comandos;
using ProvaSuficienciaWebII.Application.Equipamentos.Consultas;
using ProvaSuficienciaWebII.Contracts.Equipamentos.Dto;

namespace ProvaSuficienciaWebII.Api.Controllers;

[ApiController]
[Route("equipamentos")]
public class EquipamentosController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Lista todos os equipamentos, com o tipo aninhado. Exige autenticação via token JWT.
    /// </summary>
    [HttpGet]
    [Authorize]
    [ProducesResponseType(typeof(RespostaListaEquipamentos), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Listar()
    {
        var resposta = await mediator.Send(new ConsultarEquipamentos());
        return Ok(resposta);
    }

    /// <summary>
    /// Consulta um equipamento pelo ID. Exige autenticação via token JWT.
    /// </summary>
    [HttpGet("{id:int}")]
    [Authorize]
    [ProducesResponseType(typeof(EquipamentoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ConsultarPorId(int id)
    {
        var resposta = await mediator.Send(new ConsultarEquipamentoPorId(id));

        if (resposta is null)
        {
            return NotFound(new { erro = "Equipamento não encontrado." });
        }

        return Ok(resposta);
    }

    /// <summary>
    /// Cadastra um novo equipamento. Se o tipo informado não existir, ele é criado junto.
    /// Exige autenticação via token JWT.
    /// </summary>
    [HttpPost]
    [Authorize]
    [ProducesResponseType(typeof(EquipamentoDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Cadastrar(RequisicaoNovoEquipamento requisicao)
    {
        var resposta = await mediator.Send(new ComandoNovoEquipamento(requisicao));
        return Created($"/equipamentos/{resposta.Id}", resposta);
    }

    /// <summary>
    /// Edita parcialmente um equipamento: somente as informações enviadas são
    /// alteradas; as demais permanecem idênticas. Exige autenticação via token JWT.
    /// </summary>
    [HttpPut("{id:int}")]
    [Authorize]
    [ProducesResponseType(typeof(EquipamentoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Editar(int id, RequisicaoEditarEquipamento requisicao)
    {
        var resposta = await mediator.Send(new ComandoEditarEquipamento(id, requisicao));

        if (resposta is null)
        {
            return NotFound(new { erro = "Equipamento não encontrado." });
        }

        return Ok(resposta);
    }

    /// <summary>
    /// Exclui um equipamento. Exige autenticação via token JWT.
    /// </summary>
    [HttpDelete("{id:int}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Excluir(int id)
    {
        var excluido = await mediator.Send(new ComandoExcluirEquipamento(id));

        if (!excluido)
        {
            return NotFound(new { erro = "Equipamento não encontrado." });
        }

        // Formato de resposta definido pelo enunciado da prova.
        return Ok(new { success = new { text = "equipamento removido" } });
    }
}
