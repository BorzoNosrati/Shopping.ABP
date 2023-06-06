using Shopping.ABP.Application.Contracts.Dtos.Category;
using Shopping.ABP.Application.Contracts.Services;
using Shopping.ABP.Domain.Entities;
using Shopping.ABP.Repositories;
using Shopping.ABP.Services;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Shopping.ABP.Permissions;

namespace Shopping.ABP.Application.Services;

public class CategoryAppService : ABPAppService, ICategoryAppService
{
    private readonly ICategoryRepository _repository;
    private readonly CategoryManager _manager;

    public CategoryAppService(ICategoryRepository repository, CategoryManager manager)
    {
        _repository = repository;
        _manager = manager;
    }

    

   

    public Task<CategoryDto> GetAsync(int id)
    {
        var category = await _repository.GetAsync(id);
        return ObjectMapper.Map<Category, CategoryDto>(category);
    }

    public Task<PagedResultDto<CategoryDto>> GetListAsync(GetCategoryListDto input)
    {
        if (input.Sorting.IsNullOrWhiteSpace())
        {
            input.Sorting = nameof(Category.Name);
        }

        var categorys = await _repository.GetListAsync(
            input.SkipCount,
            input.MaxResultCount,
            input.Sorting,
            input.Filter
        );

        var totalCount = input.Filter == null
            ? await _repository.CountAsync()
            : await _repository.CountAsync(
                category => category.Name.Contains(input.Filter));

        return new PagedResultDto<CategoryDto>(
            totalCount,
            ObjectMapper.Map<List<Category>, List<CategoryDto>>(categorys)
        );
    }

    public Task UpdateAsync(int id, UpdateCategoryDto input)
    {
        throw new System.NotImplementedException();
    }


   






[Authorize(ABPPermissions.Categorys.Create)]
public async Task<CategoryDto> CreateAsync(CreateCategoryDto input)
{
    var category = await _manager.CreateAsync(
        input.Name,
        input.ParentId
    );

    await _repository.InsertAsync(category);

    return ObjectMapper.Map<Category, CategoryDto>(category);
}



[Authorize(ABPPermissions.Categorys.Edit)]
public async Task UpdateAsync(int id, UpdateCategoryDto input)
{
    var category = await _repository.GetAsync(id);

    if (category.Name != input.Name)
    {
        await _manager.ChangeNameAsync(category, input.Name);
    }
    category.ParentId = input.ParentId;

    await _repository.UpdateAsync(category);
}



[Authorize(ABPPermissions.Categorys.Delete)]
public async Task DeleteAsync(int id)
{
    await _repository.DeleteAsync(id);
}






}
