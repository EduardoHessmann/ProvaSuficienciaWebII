using Microsoft.EntityFrameworkCore;
using ProvaSuficienciaWebII.Domain.Entidades;

namespace ProvaSuficienciaWebII.Infrastructure;

/// <summary>
/// Contexto de acesso ao banco de dados PostgreSQL via Entity Framework Core.
/// O mapeamento das entidades é feito por anotações nas próprias classes de domínio.
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

        // Impede excluir um tipo que ainda esteja associado a equipamentos.
        // Não existe anotação equivalente: o comportamento de exclusão só pode
        // ser definido aqui. Sem isso, o padrão do EF Core seria excluir em
        // cascata os equipamentos do tipo removido.
        construtorModelo.Entity<Equipamento>()
            .HasOne(equipamento => equipamento.Tipo)
            .WithMany()
            .HasForeignKey(equipamento => equipamento.TipoEquipamentoId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
