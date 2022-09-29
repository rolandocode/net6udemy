using API.DTO;
using AutoMapper;
using Core.Entities;
using System.Reflection.PortableExecutable;

namespace API.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Producto, ProductoDTO>()
                .ReverseMap();

            CreateMap<Categoria, CategoriaDTO>()
                .ReverseMap();

            CreateMap<Marca, MarcaDTO>()
                .ReverseMap();

            CreateMap<Producto, ProductoListDTO>()
                .ForMember(dest => dest.Marca, origen => origen.MapFrom(origen => origen.Marca.Nombre))
                .ForMember(dest => dest.Categoria, origen => origen.MapFrom(origen => origen.Categoria.Nombre))
                .ReverseMap()
                .ForMember(origen => origen.Categoria, dest => dest.Ignore())
                .ForMember(origen => origen.Marca, dest => dest.Ignore());

            CreateMap<Producto, ProductoAddUpdateDTO>()
               .ReverseMap()
               .ForMember(origen => origen.Categoria, dest => dest.Ignore())
               .ForMember(origen => origen.Marca, dest => dest.Ignore());

        }
    }
}
