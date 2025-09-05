using AutoMapper;

namespace Infrastructure.AutomapperConfiguration
{
    public static class MappingExtensions
    {
        public static void CreateEntityMappings<TEntity, TDto, TCreateDto, TUpdateDto>(this Profile profile)
        {
            profile.CreateMap<TEntity, TDto>().ReverseMap();
            profile.CreateMap<TCreateDto, TEntity>().ReverseMap();
            profile.CreateMap<TUpdateDto, TEntity>().ReverseMap();
            profile.CreateMap<TCreateDto, TDto>().ReverseMap();
            profile.CreateMap<TUpdateDto, TDto>().ReverseMap();
        }
    }
}
