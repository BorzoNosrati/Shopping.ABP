using Shopping.ABP.Localization;
using Volo.Abp.AspNetCore.Components;

namespace Shopping.ABP.Blazor;

public abstract class ABPComponentBase : AbpComponentBase
{
    protected ABPComponentBase()
    {
        LocalizationResource = typeof(ABPResource);
    }
}
