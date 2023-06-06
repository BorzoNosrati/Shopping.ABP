using JetBrains.Annotations;
using Shopping.ABP.Domain.Entities;
using Shopping.ABP.Repositories;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace Shopping.ABP.Services;

public class CategoryManager : DomainService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryManager(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<Category> CreateAsync(
        [NotNull] string name,
        int? parentId)
    {
        Check.NotNullOrWhiteSpace(name, nameof(name));

        var existingCategory = await _categoryRepository.FindByNameAsync(name);
        if (existingCategory != null)
        {
            throw new CategoryAlreadyExistsException(name);
        }

        return new Category(
            name,
            parentId
        );
    }

    public async Task ChangeNameAsync(
        [NotNull] Category category,
        [NotNull] string newName)
    {
        Check.NotNull(category, nameof(category));
        Check.NotNullOrWhiteSpace(newName, nameof(newName));

        var existingCategory = await _categoryRepository.FindByNameAsync(newName);
        if (existingCategory != null && existingCategory.Id != category.Id)
        {
            throw new CategoryAlreadyExistsException(newName);
        }

        category.ChangeName(newName);
    }
}


public class CategoryAlreadyExistsException : BusinessException
{
    public CategoryAlreadyExistsException(string name)
        : base(ABPDomainErrorCodes.CategoryAlreadyExists)
    {
        WithData("name", name);
    }
}