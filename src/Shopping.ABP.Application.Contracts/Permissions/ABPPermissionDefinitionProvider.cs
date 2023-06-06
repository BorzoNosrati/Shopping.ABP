using Shopping.ABP.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Shopping.ABP.Permissions;

public class ABPPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(ABPPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(ABPPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<ABPResource>(name);
    }
}
