using JetBrains.Annotations;
using Shopping.ABP.Consts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Guids;

namespace Shopping.ABP.Domain.Entities;

public class Category : AggregateRoot<int>
{
    [Required]
    public string Name { get; set; }
    public int? ParentId { get; set; }

    public Category Parent { get; set;}

    public List<Category> Childs { get; set; }


    private Category() { }
    internal Category(
        [NotNull] string name,
        int? parentId)
        : base()
    {
        SetName(name);
        ParentId = parentId;
    }

    internal Category ChangeName([NotNull] string name)
    {
        SetName(name);
        return this;
    }

    private void SetName([NotNull] string name)
    {
        Name = Check.NotNullOrWhiteSpace(
            name,
            nameof(name),
            maxLength: CategoryConsts.MaxNameLength
        );
    }
}
