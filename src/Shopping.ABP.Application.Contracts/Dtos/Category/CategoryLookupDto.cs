using Volo.Abp.Application.Dtos;

namespace Shopping.ABP.Application.Contracts.Dtos.Category;

public class CategoryLookupDto : EntityDto<int>
{
    public string Name { get; set; }
}