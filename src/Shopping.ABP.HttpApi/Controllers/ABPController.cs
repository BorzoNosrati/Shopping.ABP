using Shopping.ABP.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Shopping.ABP.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class ABPController : AbpControllerBase
{
    protected ABPController()
    {
        LocalizationResource = typeof(ABPResource);
    }
}
