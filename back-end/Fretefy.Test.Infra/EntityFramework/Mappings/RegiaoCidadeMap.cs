using Fretefy.Test.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fretefy.Test.Infra.EntityFramework.Mappings
{
    public class RegiaoCidadeMap : IEntityTypeConfiguration<RegiaoCidade>
    {
        public void Configure(EntityTypeBuilder<RegiaoCidade> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.CidadeId).IsRequired();

            builder.HasOne(rc => rc.Regiao)
            .WithMany(r => r.RegiaoCidades)
            .HasForeignKey(rc => rc.RegiaoId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
        }
    }
}
