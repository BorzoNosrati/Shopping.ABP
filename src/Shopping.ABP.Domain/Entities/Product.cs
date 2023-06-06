using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;

namespace Shopping.ABP.Domain.Entities;

public class Product : AuditedAggregateRoot<int>
{
    [Required]
    public string Name { get; set; }

    [Required]
    public int CategoryId { get; set; }

    [Required]
    public float Price { get; set; }

    public Category Category { get; set; }

}
