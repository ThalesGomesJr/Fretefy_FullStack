﻿using Fretefy.Test.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fretefy.Test.Infra.EntityFramework.Mappings
{
    public class RegiaoMap : IEntityTypeConfiguration<Regiao>
    {
        public void Configure(EntityTypeBuilder<Regiao> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Nome).HasMaxLength(1024).IsRequired();
            builder.Property(p => p.Ativo).IsRequired();
            
            builder.HasMany(r => r.RegiaoCidades)
            .WithOne(rc => rc.Regiao)
            .HasForeignKey(rc => rc.RegiaoId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
        }
    }
}
