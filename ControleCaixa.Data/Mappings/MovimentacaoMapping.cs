using ControleCaixa.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControleCaixa.Data.Configurations;

public class MovimentacaoConfiguration : IEntityTypeConfiguration<Movimentacao>
{
    public void Configure(EntityTypeBuilder<Movimentacao> builder)
    {
        builder.ToTable("Movimentacoes");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Descricao)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.Tipo)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(x => x.Categoria)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(x => x.Valor)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(x => x.DataCriacao)
            .IsRequired();

        builder.Property(x => x.DataAlteracao);

        builder.Property(x => x.Lixeira)
            .IsRequired()
            .HasDefaultValue(false);

        builder.HasOne(x => x.Caixa)
            .WithMany(x => x.Movimentacoes)
            .HasForeignKey(x => x.CaixaId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}