using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProvaSuficienciaWebII.Application.Comum.Excecoes;
using ProvaSuficienciaWebII.Application.Usuarios.Comandos;
using ProvaSuficienciaWebII.Application.Usuarios.Consultas;
using ProvaSuficienciaWebII.Contracts.Usuarios.Dto;

namespace ProvaSuficienciaWebII.Api.Controllers;

[ApiController]
[Route("usuarios")]
public class UsuariosController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Registra um novo usuário. Endpoint público (necessário para criar o primeiro acesso).
    /// </summary>
    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(typeof(UsuarioDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Registrar(RequisicaoNovoUsuario requisicao)
    {
        try
        {
            var resposta = await mediator.Send(new ComandoNovoUsuario(requisicao));
            return Created($"/usuarios/{resposta.Id}", resposta);
        }
        catch (ExcecaoEmailJaCadastrado excecao)
        {
            return Conflict(new { erro = excecao.Message });
        }
    }

    /// <summary>
    /// Lista os usuários cadastrados. Exige autenticação via token JWT.
    /// </summary>
    [HttpGet]
    [Authorize]
    [ProducesResponseType(typeof(List<UsuarioDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Listar()
    {
        var resposta = await mediator.Send(new ConsultarUsuarios());
        return Ok(resposta);
    }

    /// <summary>
    /// Consulta um usuário pelo ID. Exige autenticação via token JWT.
    /// </summary>
    [HttpGet("{id:int}")]
    [Authorize]
    [ProducesResponseType(typeof(UsuarioDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ConsultarPorId(int id)
    {
        var resposta = await mediator.Send(new ConsultarUsuarioPorId(id));

        if (resposta is null)
        {
            return NotFound(new { erro = "Usuário não encontrado." });
        }

        return Ok(resposta);
    }

    /// <summary>
    /// Edita parcialmente um usuário: somente os campos enviados são alterados.
    /// Exige autenticação via token JWT.
    /// </summary>
    [HttpPut("{id:int}")]
    [Authorize]
    [ProducesResponseType(typeof(UsuarioDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Editar(int id, RequisicaoEditarUsuario requisicao)
    {
        try
        {
            var resposta = await mediator.Send(new ComandoEditarUsuario(id, requisicao));

            if (resposta is null)
            {
                return NotFound(new { erro = "Usuário não encontrado." });
            }

            return Ok(resposta);
        }
        catch (ExcecaoEmailJaCadastrado excecao)
        {
            return Conflict(new { erro = excecao.Message });
        }
    }

    /// <summary>
    /// Exclui um usuário. Exige autenticação via token JWT.
    /// </summary>
    [HttpDelete("{id:int}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Excluir(int id)
    {
        var excluido = await mediator.Send(new ComandoExcluirUsuario(id));

        if (!excluido)
        {
            return NotFound(new { erro = "Usuário não encontrado." });
        }

        return Ok(new { success = new { text = "usuário removido" } });
    }
}
