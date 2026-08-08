namespace ProvaSuficienciaWebII.Application.Comum.Excecoes;

/// <summary>
/// Lançada quando se tenta cadastrar ou alterar um usuário para um e-mail
/// que já pertence a outro usuário.
/// </summary>
public class ExcecaoEmailJaCadastrado : Exception
{
    public ExcecaoEmailJaCadastrado()
        : base("Já existe um usuário cadastrado com este e-mail.")
    {
    }
}
