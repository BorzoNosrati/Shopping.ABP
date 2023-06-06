using Shopping.ABP.Application.Contracts.Dtos.Product;
using Shopping.ABP.Application.Contracts.Services;
using Shopping.ABP.Domain.Entities;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Shopping.ABP.Application.Services;

public class ProductAppService :
    CrudAppService<Product,
        ProductDto,
        int,
        PagedAndSortedResultRequestDto,
        CreateUpdateProductDto>,
    IProductAppService
{
    private readonly IRepository<Product, int> _repository;

    public ProductAppService(IRepository<Product, int> repository) : base(repository)
    {
        _repository = repository;
    }
}
