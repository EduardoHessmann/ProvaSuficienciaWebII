namespace ProvaSuficienciaWebII.Application.Comum.Excecoes;

/// <summary>
/// Lançada quando se tenta excluir um tipo de equipamento que ainda está
/// associado a um ou mais equipamentos.
/// </summary>
public class ExcecaoTipoEquipamentoEmUso : Exception
{
    public ExcecaoTipoEquipamentoEmUso()
        : base("O tipo de equipamento está em uso por um ou mais equipamentos e não pode ser removido.")
    {
    }
}
