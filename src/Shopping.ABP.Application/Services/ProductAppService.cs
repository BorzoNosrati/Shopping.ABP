using Shopping.ABP.Application.Contracts.Dtos.Product;
using Shopping.ABP.Application.Contracts.Services;
using Shopping.ABP.Domain.Entities;
using Shopping.ABP.Permissions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
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
    private readonly IRepository<Product, int> _repository;

    public ProductAppService(IRepository<Product, int> repository) : base(repository)
    {
        _repository = repository;


        GetPolicyName = ABPPermissions.Products.Default;
        GetListPolicyName = ABPPermissions.Products.Default;
        CreatePolicyName = ABPPermissions.Products.Create;
        UpdatePolicyName = ABPPermissions.Products.Edit;
        DeletePolicyName = ABPPermissions.Products.Delete;

        
    }


    

}
