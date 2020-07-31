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
    public class LivroModelConfiguration : IEntityTypeConfiguration<LivroModel>
    {
        public void Configure(EntityTypeBuilder<LivroModel> builder)
        {
            builder
                .HasOne(livro => livro.Autor)
                .WithMany(autor => autor.Livros)
                .HasPrincipalKey(autor => autor.Id)
                .HasForeignKey(livro => livro.AutorId);

            builder
                .HasKey(x => x.Id);

            builder
                .HasIndex(x => x.Isbn)
                .IsUnique();

            builder
                .Property(x => x.Isbn)
                .HasMaxLength(100);

            builder
                .Property(x => x.Titulo)
                .HasMaxLength(100);
        }
    }
}
