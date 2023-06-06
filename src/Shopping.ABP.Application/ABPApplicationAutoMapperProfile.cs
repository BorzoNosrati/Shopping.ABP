using AutoMapper;
using Shopping.ABP.Application.Contracts.Dtos.Category;
using Shopping.ABP.Application.Contracts.Dtos.Product;
using Shopping.ABP.Domain.Entities;

namespace Shopping.ABP;


public class ABPApplicationAutoMapperProfile : Profile
{
    public ABPApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */


        // product
        CreateMap<Product, ProductDto>();   
        CreateMap<CreateUpdateProductDto, Product>();      


        //category
        CreateMap<Category, CategoryDto>();      
        CreateMap<Category, CategoryLookupDto>();  
  
        CreateMap<CreateCategoryDto, Category>();
        CreateMap<UpdateCategoryDto, Category>();

        CreateMap<CategoryDto,UpdateCategoryDto >();
    }
}
