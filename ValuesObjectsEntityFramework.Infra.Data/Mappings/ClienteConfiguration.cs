using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using ValuesObjectsEntityFramework.Domain.Entidades;

namespace ValuesObjectsEntityFramework.Infra.Data.Mappings
{
    public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.OwnsOne(c => c.CPF, c =>
            {
                c.Property(e => e.Numero).HasColumnName("CPF");
            });
            builder.OwnsOne(c => c.Endereco, vo =>
            {
                vo.OwnsOne(e => e.CEP, e =>
                {
                    e.Property(c => c.Numero).HasColumnName("CEP");
                });

                vo.Property(e => e.Logradouro).HasColumnName("endereco");
                vo.Property(e => e.Numero).HasColumnName("numeroEndereco");
                vo.Property(e => e.Bairro).HasColumnName("bairro");
                vo.Property(e => e.Cidade).HasColumnName("cidade");
                vo.Property(e => e.UF).HasColumnName("UF");
                vo.Property(e => e.Complemento).HasColumnName("complemento");
            });
        }
    }
}
