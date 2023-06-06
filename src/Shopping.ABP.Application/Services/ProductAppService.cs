using Shopping.ABP.Application.Contracts.Dtos.Category;
using Shopping.ABP.Application.Contracts.Dtos.Product;
using Shopping.ABP.Application.Contracts.Services;
using Shopping.ABP.Domain.Entities;
using Shopping.ABP.Permissions;
using Shopping.ABP.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;


namespace Shopping.ABP.Application.Services;

public class ProductAppService :
    CrudAppService<Product,
        ProductDto,
        int,
        PagedAndSortedResultRequestDto,
        CreateUpdateProductDto>,
        IProductAppService
{

    private readonly ICategoryRepository _categoryRepository;

    public ProductAppService(
            IRepository<Product, int> repository,
            ICategoryRepository categoryRepository)
        : base(repository)
    {

        _categoryRepository = categoryRepository;

        GetPolicyName = ABPPermissions.Products.Default;
        GetListPolicyName = ABPPermissions.Products.Default;
        CreatePolicyName = ABPPermissions.Products.Create;
        UpdatePolicyName = ABPPermissions.Products.Edit;
        DeletePolicyName = ABPPermissions.Products.Delete;
    }

    public async override Task<ProductDto> GetAsync(int id)
    {
        var queryable = await Repository.GetQueryableAsync();
        var qc = await _categoryRepository.GetQueryableAsync();

        var query = from product in queryable
                    join category in await _categoryRepository.GetQueryableAsync() on product.CategoryId equals category.Id
                    where product.Id == id
                    select new { product, category };


        var queryResult = await AsyncExecuter.FirstOrDefaultAsync(query);

        if (queryResult is null)
        {
            throw new EntityNotFoundException(typeof(Product), id);
        }

        var productDto = ObjectMapper.Map<Product, ProductDto>(queryResult.product);
        productDto.CategoryName = queryResult.category.Name;
        return productDto;
    }

    public async override Task<PagedResultDto<ProductDto>> GetListAsync(PagedAndSortedResultRequestDto input)
    {
        var q = await Repository.GetQueryableAsync();
        var qc = await _categoryRepository.GetQueryableAsync();
        //Prepare a query to join books and authors
        var query = from product in q
                    join category in qc
                                    on product.CategoryId equals category.Id
                    select new { product, category };

        // paging

        query = query
            .OrderBy(k=>k.product.Name)
            .Skip(input.SkipCount)
            .Take(input.MaxResultCount);

        var queryResult = await AsyncExecuter.ToListAsync(query);

        var productDtos = queryResult.Select(x =>
        {
            var bookDto = ObjectMapper.Map<Product, ProductDto>(x.product);
            bookDto.CategoryName = x.category.Name;
            return bookDto;
        }).ToList();

        var totalCount = await Repository.GetCountAsync();

        return new PagedResultDto<ProductDto>(
            totalCount,
            productDtos
        );

    }
    public async Task<ListResultDto<CategoryLookupDto>> GetCategoryLookupAsync()
    {
        var categories = await _categoryRepository.GetListAsync();

        return new ListResultDto<CategoryLookupDto>(
            ObjectMapper.Map<List<Category>, List<CategoryLookupDto>>(categories)
        );
    }

    private static string NormalizeSorting(string sorting)
    {
        if (sorting.IsNullOrEmpty())
        {
            return $"product.{nameof(Product.Name)}";
        }

        if (sorting.Contains("categoryName", StringComparison.OrdinalIgnoreCase))
        {
            return sorting.Replace(
                "categoryName",
                "category.Name",
                StringComparison.OrdinalIgnoreCase
            );
        }

        return $"product.{sorting}";
    }


}
