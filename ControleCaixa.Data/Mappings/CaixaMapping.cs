using ControleCaixa.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControleCaixa.Data.Configurations;

public class CaixaConfiguration : IEntityTypeConfiguration<Caixa>
{
    public void Configure(EntityTypeBuilder<Caixa> builder)
    {
        builder.ToTable("Caixas");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Nome)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.SaldoMinimo)
            .IsRequired();
        
        builder.Property(c => c.DataCriacao)
            .IsRequired();
        
        builder.Property(c => c.DataAlteracao)
            .IsRequired();
        
        builder.Property(c => c.Lixeira)
            .IsRequired()
            .HasDefaultValue(false);

        builder.HasMany(c => c.Movimentacoes)
            .WithOne(m => m.Caixa)
            .HasForeignKey(m => m.CaixaId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}