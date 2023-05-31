
using Ecommerce_API.Data.DTOS;
using Ecommerce_API.Models;
using AutoMapper;

namespace Ecommerce_API.Profiles;

public class CategoriaProfile: Profile
{
    public CategoriaProfile()
    {
        CreateMap<CreateCategoriaDto, Categoria>();
        CreateMap<UpdateCategoriaDto, Categoria>();
        
    }
}
