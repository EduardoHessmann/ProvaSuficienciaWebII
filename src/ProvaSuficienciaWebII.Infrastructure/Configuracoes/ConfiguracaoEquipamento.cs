using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProvaSuficienciaWebII.Domain.Entidades;

namespace ProvaSuficienciaWebII.Infrastructure.Configuracoes;

/// <summary>
/// Configuração de mapeamento da entidade <see cref="Equipamento"/>.
/// Tabela no plural, conforme a nomenclatura padrão de banco de dados.
/// </summary>
public class ConfiguracaoEquipamento : IEntityTypeConfiguration<Equipamento>
{
    public void Configure(EntityTypeBuilder<Equipamento> construtor)
    {
        construtor.ToTable("equipamentos");

        construtor.HasKey(e => e.Id);

        construtor.Property(e => e.Nome)
            .HasMaxLength(100)
            .IsRequired();

        construtor.HasOne(e => e.Tipo)
            .WithMany()
            .HasForeignKey(e => e.TipoEquipamentoId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
