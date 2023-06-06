using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace Shopping.ABP.Application.Contracts.Dtos.Category;

public class CreateUpdateCategoryDto : EntityDto<int>
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; }
    public int? ParentId { get; set; }
}
