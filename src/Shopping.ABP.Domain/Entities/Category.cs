using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;

namespace Shopping.ABP.Domain.Entities;

public class Category : AggregateRoot<int>
{
    [Required]
    public string Name { get; set; }
    public int? ParentId { get; set; }

    public Category Parent { get; set;}

    public List<Category> Childs { get; set; }
}
