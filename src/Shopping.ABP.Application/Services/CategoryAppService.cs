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





    public async Task<CategoryDto> GetAsync(int id)
    {
        var category = await _repository.GetAsync(id);
        return ObjectMapper.Map<Category, CategoryDto>(category);
    }

    public async Task<PagedResultDto<CategoryDto>> GetListAsync(GetCategoryListDto input)
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








    [Authorize(ABPPermissions.Categories.Create)]
    public async Task<CategoryDto> CreateAsync(CreateCategoryDto input)
    {
        var category = await _manager.CreateAsync(
            input.Name,
            input.ParentId
        );

        await _repository.InsertAsync(category);

        return ObjectMapper.Map<Category, CategoryDto>(category);
    }



    [Authorize(ABPPermissions.Categories.Edit)]
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



    [Authorize(ABPPermissions.Categories.Delete)]
    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }


    public async Task<ListResultDto<CategoryLookupDto>> GetCategoryLookupAsync()
    {
        var categories = await _repository.GetListAsync();

        return new ListResultDto<CategoryLookupDto>(
            ObjectMapper.Map<List<Category>, List<CategoryLookupDto>>(categories)
        );
    }



}
