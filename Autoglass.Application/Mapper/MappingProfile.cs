using AutoMapper;
using Autoglass.Domain.Entities;
using Autoglass.Application.DTOs;
using static System.Runtime.InteropServices.JavaScript.JSType;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Produto, ProdutoDto>().ReverseMap();        
    }
}