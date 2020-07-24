using Domain.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Infrastructure.Context
{
    //Lembrar de selecionar default project no Dropdown

    //Add-Migration

    //Update-database
    public class BibliotecaContext : DbContext
    {
        public BibliotecaContext(DbContextOptions<BibliotecaContext> options)
            : base(options)
        {
        }

        public DbSet<AutorModel> Autores { get; set; }
    }
}
