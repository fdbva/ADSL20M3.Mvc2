using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Context.Configurations
{
    public class AutorModelConfiguration : IEntityTypeConfiguration<AutorModel>
    {
        public void Configure(EntityTypeBuilder<AutorModel> builder)
        {
            builder
                .Property(x => x.Nome)
                .HasMaxLength(100)
                .IsRequired();

            builder
                .Property(x => x.UltimoNome)
                .HasMaxLength(100)
                .IsRequired();

            builder
                .Property(x => x.Nascimento)
                .IsRequired();
        }
    }
}
