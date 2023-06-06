using Shopping.ABP.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Shopping.ABP;

[DependsOn(
    typeof(ABPEntityFrameworkCoreTestModule)
    )]
public class ABPDomainTestModule : AbpModule
{

}
