using System.Collections.Generic;
using System.Threading.Tasks;
using Shopping.ABP.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace Shopping.ABP.Repositories;

public interface ICategoryRepository : IRepository<Category, int>
{
    Task<Category> FindByNameAsync(string name);

    Task<List<Category>> GetListAsync(
        int skipCount,
        int maxResultCount,
        string sorting,
        string filter = null
    );
}
