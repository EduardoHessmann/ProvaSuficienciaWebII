using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProvaSuficienciaWebII.Application.Autenticacao.Comandos;
using ProvaSuficienciaWebII.Contracts.Autenticacao.Dto;

namespace ProvaSuficienciaWebII.Api.Controllers;

[ApiController]
[Route("login")]
public class AutenticacaoController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Autentica o usuário e retorna o token JWT para acesso aos endpoints protegidos.
    /// </summary>
    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(typeof(RespostaLogin), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login(RequisicaoLogin requisicao)
    {
        var resposta = await mediator.Send(new ComandoLoginUsuario(requisicao));

        if (resposta is null)
            return Unauthorized(new { erro = "E-mail ou senha inválidos." });

        return Ok(resposta);
    }
}
