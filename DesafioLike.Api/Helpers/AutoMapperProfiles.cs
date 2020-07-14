using AutoMapper;
using DesafioLike.Api.Dtos;
using DesafioLike.Dominio.Entidades;
using DesafioLike.Dominio.Identity;

namespace DesafioLike.Api.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Categoria, CategoriaDto>().ReverseMap();
            CreateMap<Pergunta, PerguntaDto>().ReverseMap();
            CreateMap<Resposta, RespostaDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserLoginDto>().ReverseMap();
            CreateMap<Role, RoleDto>().ReverseMap();
        }
    }
}