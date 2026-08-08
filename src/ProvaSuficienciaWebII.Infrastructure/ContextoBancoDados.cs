using Microsoft.EntityFrameworkCore;

namespace ProvaSuficienciaWebII.Infrastructure;

/// <summary>
/// Contexto de acesso ao banco de dados PostgreSQL via Entity Framework Core.
/// </summary>
public class ContextoBancoDados : DbContext
{
    public ContextoBancoDados(DbContextOptions<ContextoBancoDados> opcoes) : base(opcoes)
    {
    }

    // Os DbSets das entidades serão adicionados aqui conforme o domínio evoluir.

    protected override void OnModelCreating(ModelBuilder construtorModelo)
    {
        base.OnModelCreating(construtorModelo);

        // Aplica automaticamente todas as configurações de entidade definidas neste assembly.
        construtorModelo.ApplyConfigurationsFromAssembly(typeof(ContextoBancoDados).Assembly);
    }
}
