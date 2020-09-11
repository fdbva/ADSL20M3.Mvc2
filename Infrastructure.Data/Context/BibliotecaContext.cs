using Domain.Model.Models;
using Infrastructure.Data.Context.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Data.Infrastructure.Context
{
    //Lembrar de selecionar default project no Dropdown

    //Add-Migration [nome] -context BibliotecaContext

    //Update-database -context BibliotecaContext
    //update-database -context BibliotecaContext -StartupProject Application.WebApi
    public class BibliotecaContext : DbContext
    {
        public BibliotecaContext(DbContextOptions<BibliotecaContext> options)
            : base(options)
        {
        }

        public DbSet<AutorModel> Autores { get; set; }
        public DbSet<LivroModel> Livros { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new LivroModelConfiguration());
            modelBuilder.ApplyConfiguration(new AutorModelConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
