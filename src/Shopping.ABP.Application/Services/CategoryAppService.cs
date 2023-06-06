using Shopping.ABP.Application.Contracts.Dtos.Category;
using Shopping.ABP.Application.Contracts.Services;
using Shopping.ABP.Domain.Entities;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Shopping.ABP.Application.Services;

public class CategoryAppService :
    CrudAppService<Category,
        CategoryDto,
        int,
        PagedAndSortedResultRequestDto,
        CreateUpdateCategoryDto>,
    ICategoryAppService
{
    private readonly IRepository<Category, int> _repository;

    public CategoryAppService(IRepository<Category, int> repository) : base(repository)
    {
        _repository = repository;
    }
}
