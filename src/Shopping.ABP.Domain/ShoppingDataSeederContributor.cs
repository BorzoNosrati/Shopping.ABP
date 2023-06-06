using Shopping.ABP.Domain.Entities;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace Shopping.ABP.Domain;

public class ShoppingDataSeederContributor
    : IDataSeedContributor, ITransientDependency
{

    private readonly IRepository<Category, int> _categoryRepository;
    private readonly IRepository<Product, int> _productRepository;

    public ShoppingDataSeederContributor(
        IRepository<Category, int> categoryRepository,
        IRepository<Product, int> productRepository)
    {
        _categoryRepository = categoryRepository;
        _productRepository = productRepository;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        if (await _categoryRepository.GetCountAsync() > 0)
            return;

        var cat1 = await _categoryRepository.InsertAsync(new("Digital Gadget",null ) , autoSave: true);
        var cat2 = await _categoryRepository.InsertAsync(new("Laptops",  cat1.Id) , autoSave: true);

        
        var pro1 = await _productRepository.InsertAsync(new() { CategoryId = cat2.Id, Name = "Macbook Pro 14" }, true);
        var pro2 = await _productRepository.InsertAsync(new() { CategoryId = cat2.Id, Name = "Macbook Pro 16" }, true);



    }
}
