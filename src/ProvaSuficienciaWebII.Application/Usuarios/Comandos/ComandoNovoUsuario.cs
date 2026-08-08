using MediatR;
using ProvaSuficienciaWebII.Contracts.Usuarios.Dto;

namespace ProvaSuficienciaWebII.Application.Usuarios.Comandos;

/// <summary>
/// Dados para cadastrar um novo usuário.
/// Lança <see cref="Comum.Excecoes.ExcecaoEmailJaCadastrado"/> quando o e-mail já está em uso.
/// </summary>
public record ComandoNovoUsuario(RequisicaoNovoUsuario Dados) : IRequest<UsuarioDto>;
