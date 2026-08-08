using MediatR;
using ProvaSuficienciaWebII.Contracts.Autenticacao.Dto;

namespace ProvaSuficienciaWebII.Application.Autenticacao.Comandos;

/// <summary>
/// Dados para autenticação do usuário.
/// Retorna nulo quando as credenciais são inválidas.
/// </summary>
public record ComandoLoginUsuario(RequisicaoLogin Dados) : IRequest<RespostaLogin?>;
