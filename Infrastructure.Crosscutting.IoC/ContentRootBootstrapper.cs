using Application.AppServices;
using Application.AppServices.Implementations;
using AutoMapper;
using Data.Infrastructure.Context;
using Data.Infrastructure.Repositories;
using Domain.Model.Interfaces.Repositories;
using Domain.Model.Interfaces.Services;
using Domain.Model.Interfaces.UoW;
using Domain.Service.Services;
using Infrastructure.Crosscutting.IoC.MappingConfig;
using Infrastructure.Data.UoW;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Crosscutting.IoC
{
    public static class ContentRootBootstrapper
    {
        public static void RegisterDependencies(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<BibliotecaContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("BibliotecaContext")));

            services.AddAutoMapper(typeof(MappingProfiles));

            //Registro das dependências
            services.AddScoped<IAutorAppService, AutorAppService>();
            services.AddScoped<IAutorService, AutorService>();
            services.AddScoped<IAutorRepository, AutorRepository>();
            services.AddScoped<ILivroAppService, LivroAppService>();
            services.AddScoped<ILivroService, LivroService>();
            services.AddScoped<ILivroRepository, LivroRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
