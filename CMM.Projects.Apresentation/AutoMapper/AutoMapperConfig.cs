using AutoMapper;
using SisGeape2.Apresentation.AutoMapper.Mapping;

namespace SisGeape2.Apresentation.AutoMapper
{
    public class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.Initialize(a =>
            {
                a.AddProfile<DomainToModelView>();
                a.AddProfile<ModelViewToDomain>();

            });
        }
    }
}