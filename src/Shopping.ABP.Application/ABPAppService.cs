using System;
using System.Collections.Generic;
using System.Text;
using Shopping.ABP.Localization;
using Volo.Abp.Application.Services;

namespace Shopping.ABP;

/* Inherit your application services from this class.
 */
public abstract class ABPAppService : ApplicationService
{
    protected ABPAppService()
    {
        LocalizationResource = typeof(ABPResource);
    }
}
