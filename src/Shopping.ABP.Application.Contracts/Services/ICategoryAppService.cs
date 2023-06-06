using Shopping.ABP.Application.Contracts.Dtos.Category;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Shopping.ABP.Application.Contracts.Services;

public interface ICategoryAppService :
    ICrudAppService< //Defines CRUD methods
        CategoryDto, //Used to show books
        int, //Primary key of the book entity
        PagedAndSortedResultRequestDto, //Used for paging/sorting
        CreateUpdateCategoryDto> //Used to create/update a book
{

}
