using Shopping.ABP.Application.Contracts.Dtos.Product;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Shopping.ABP.Application.Contracts.Services;

public interface IProductAppService :
    ICrudAppService< //Defines CRUD methods
        ProductDto, //Used to show books
        int, //Primary key of the book entity
        PagedAndSortedResultRequestDto, //Used for paging/sorting
        CreateUpdateProductDto> //Used to create/update a book
{

}
