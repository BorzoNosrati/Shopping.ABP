using Volo.Abp.Application.Dtos;

namespace Shopping.ABP.Application.Contracts.Dtos.Category;

public class GetCategoryListDto : PagedAndSortedResultRequestDto
{
    public string? Filter { get; set; }
}
