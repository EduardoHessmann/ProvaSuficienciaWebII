using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProvaSuficienciaWebII.Domain.Entidades;

namespace ProvaSuficienciaWebII.Infrastructure.Configuracoes;

/// <summary>
/// Configuração de mapeamento da entidade <see cref="TipoEquipamento"/>.
/// Tabela no plural, conforme a nomenclatura padrão de banco de dados.
/// </summary>
public class ConfiguracaoTipoEquipamento : IEntityTypeConfiguration<TipoEquipamento>
{
    public void Configure(EntityTypeBuilder<TipoEquipamento> construtor)
    {
        construtor.ToTable("tipos_equipamento");

        construtor.HasKey(t => t.Id);

        construtor.Property(t => t.Nome)
            .HasMaxLength(100)
            .IsRequired();
    }
}
