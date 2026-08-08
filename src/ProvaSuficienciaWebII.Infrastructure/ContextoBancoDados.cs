using Microsoft.EntityFrameworkCore;
using ProvaSuficienciaWebII.Domain.Entidades;

namespace ProvaSuficienciaWebII.Infrastructure;

/// <summary>
/// Contexto de acesso ao banco de dados PostgreSQL via Entity Framework Core.
/// </summary>
public class ContextoBancoDados : DbContext
{
    public ContextoBancoDados(DbContextOptions<ContextoBancoDados> opcoes) : base(opcoes)
    {
    }

    public DbSet<Usuario> Usuarios => Set<Usuario>();

    public DbSet<Equipamento> Equipamentos => Set<Equipamento>();

    public DbSet<TipoEquipamento> TiposEquipamento => Set<TipoEquipamento>();

    protected override void OnModelCreating(ModelBuilder construtorModelo)
    {
        base.OnModelCreating(construtorModelo);

        // Aplica automaticamente todas as configurações de entidade definidas neste assembly.
        construtorModelo.ApplyConfigurationsFromAssembly(typeof(ContextoBancoDados).Assembly);
    }
}
