using Shopping.ABP.Application.Contracts.Dtos.Category;
using System.Threading.Tasks;
using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Shopping.ABP.Application.Contracts.Services;

public interface ICategoryAppService :IApplicationService
{
    Task<CategoryDto> GetAsync(int id);

    Task<PagedResultDto<CategoryDto>> GetListAsync(GetCategoryListDto input);

    Task<CategoryDto> CreateAsync(CreateCategoryDto input);

    Task UpdateAsync(int id, UpdateCategoryDto input);

    Task DeleteAsync(int id);
    Task<ListResultDto<CategoryLookupDto>> GetCategoryLookupAsync();
}
