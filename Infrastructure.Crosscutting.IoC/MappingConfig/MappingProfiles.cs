using Application.ViewModels;
using AutoMapper;
using Domain.Model.Models;

namespace Infrastructure.Crosscutting.IoC.MappingConfig
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<AutorViewModel, AutorModel>().ReverseMap();
            CreateMap<LivroViewModel, LivroModel>().ReverseMap();
            CreateMap<LivroAutorAggregateRequest, LivroAutorAggregateModel>().ReverseMap();
        }
    }
}
