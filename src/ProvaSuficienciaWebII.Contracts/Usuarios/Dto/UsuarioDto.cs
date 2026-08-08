namespace ProvaSuficienciaWebII.Contracts.Usuarios.Dto;

/// <summary>
/// Representação de um usuário retornada pela API
/// </summary>
public class UsuarioDto
{
    public int Id { get; set; }

    public string Nome { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;
}
