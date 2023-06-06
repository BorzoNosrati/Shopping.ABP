using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Shopping.ABP.Blazor;

[Dependency(ReplaceServices = true)]
public class ABPBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "ABP";
}
