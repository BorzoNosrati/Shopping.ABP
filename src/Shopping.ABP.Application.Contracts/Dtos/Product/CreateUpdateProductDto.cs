using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace Shopping.ABP.Application.Contracts.Dtos.Product;

public class CreateUpdateProductDto : AuditedEntityDto<int>
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    [Required]
    public int CategoryId { get; set; }
    [Required]
    public float Price { get; set; }
}
