using Volo.Abp.Application.Dtos;

namespace Shopping.ABP.Application.Contracts.Dtos.Product;

public class ProductDto : AuditedEntityDto<int>
{
    public string Name { get; set; }
    public int CategoryId { get; set; }
    public float Price { get; set; }
}
