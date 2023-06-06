using Shopping.ABP.Consts;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace Shopping.ABP.Application.Contracts.Dtos.Category;

public class UpdateCategoryDto : EntityDto<int>
{
    [Required]
    [StringLength(CategoryConsts.MaxNameLength)]
    public string Name { get; set; }
    public int? ParentId { get; set; }
}