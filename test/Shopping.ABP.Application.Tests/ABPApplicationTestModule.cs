using Volo.Abp.Modularity;

namespace Shopping.ABP;

[DependsOn(
    typeof(ABPApplicationModule),
    typeof(ABPDomainTestModule)
    )]
public class ABPApplicationTestModule : AbpModule
{

}
