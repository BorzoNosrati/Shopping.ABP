using Shopping.ABP.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Shopping.ABP.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(ABPEntityFrameworkCoreModule),
    typeof(ABPApplicationContractsModule)
    )]
public class ABPDbMigratorModule : AbpModule
{

}
