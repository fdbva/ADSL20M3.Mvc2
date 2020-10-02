using Application.AppServices;
using Application.AppServices.Implementations;
using AutoMapper;
using Data.Infrastructure.Context;
using Domain.Model.Interfaces.Repositories;
using Domain.Model.Interfaces.Services;
using Domain.Model.Interfaces.UoW;
using Domain.Service.Services;
using Infrastructure.Crosscutting.IoC.MappingConfig;
using Infrastructure.Data.Repositories;
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

            services.AddAutoMapper(x => x.AddProfile(typeof(MappingProfiles)));
            
            //Registro das dependências
            services.AddScoped<IAutorCrudAppService, AutorCrudAppService>();
            services.AddScoped<IAutorCrudService, AutorCrudService>();
            services.AddScoped<IAutorRepository, AutorCrudRepository>();
            services.AddScoped<ILivroCrudAppService, LivroCrudAppService>();
            services.AddScoped<ILivroCrudService, LivroCrudService>();
            services.AddScoped<ILivroRepository, LivroCrudRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
