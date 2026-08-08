using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProvaSuficienciaWebII.Domain.Entidades;

namespace ProvaSuficienciaWebII.Infrastructure.Configuracoes;

/// <summary>
/// Configuração de mapeamento da entidade <see cref="Usuario"/>.
/// Tabela no plural, conforme a nomenclatura padrão de banco de dados.
/// </summary>
public class ConfiguracaoUsuario : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> construtor)
    {
        construtor.ToTable("usuarios");

        construtor.HasKey(u => u.Id);

        construtor.Property(u => u.Nome)
            .HasMaxLength(100)
            .IsRequired();

        construtor.Property(u => u.Email)
            .HasMaxLength(150)
            .IsRequired();

        construtor.Property(u => u.SenhaHash)
            .IsRequired();

        // Não podem existir dois usuários com o mesmo e-mail.
        construtor.HasIndex(u => u.Email).IsUnique();
    }
}
