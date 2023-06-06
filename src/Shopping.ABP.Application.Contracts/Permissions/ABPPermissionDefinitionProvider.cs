﻿using Shopping.ABP.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Shopping.ABP.Permissions;

public class ABPPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var bookStoreGroup = context.AddGroup(ABPPermissions.GroupName, L("Permission:BookStore"));

        var booksPermission = bookStoreGroup.AddPermission(ABPPermissions.Products.Default, L("Permission:Products"));
        booksPermission.AddChild(ABPPermissions.Products.Create, L("Permission:Products.Create"));
        booksPermission.AddChild(ABPPermissions.Products.Edit, L("Permission:Products.Edit"));
        booksPermission.AddChild(ABPPermissions.Products.Delete, L("Permission:Products.Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<ABPResource>(name);
    }
}
