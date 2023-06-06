using Volo.Abp.Application.Dtos;

namespace Shopping.ABP.Application.Contracts.Dtos.Category;

public class CategoryDto : EntityDto<int>
{
    public string Name { get; set; }
    public int? ParentId { get; set; }
}
