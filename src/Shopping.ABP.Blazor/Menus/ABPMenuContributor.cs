using System.Threading.Tasks;
using Shopping.ABP.Localization;
using Shopping.ABP.MultiTenancy;
using Shopping.ABP.Permissions;
using Volo.Abp.Identity.Blazor;
using Volo.Abp.SettingManagement.Blazor.Menus;
using Volo.Abp.TenantManagement.Blazor.Navigation;
using Volo.Abp.UI.Navigation;

namespace Shopping.ABP.Blazor.Menus;

public class ABPMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private async Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var administration = context.Menu.GetAdministration();
        var l = context.GetLocalizer<ABPResource>();

        context.Menu.Items.Insert(
            0,
            new ApplicationMenuItem(
                ABPMenus.Home,
                l["Menu:Home"],
                "/",
                icon: "fas fa-home",
                order: 0
            )
        );
        context.Menu.AddItem(
            new ApplicationMenuItem(
                "Shopping",
                l["Menu:Shopping"],
                icon: "fa fa-basket"
            ).AddItem(
                new ApplicationMenuItem(
                    "Shopping.Products",
                    l["Menu:Products"],
                    url: "/products"
                )
    )
            
            );

        if (await context.IsGrantedAsync(ABPPermissions.Categories.Default))
        {
            context.Menu.AddItem(new ApplicationMenuItem(
                "Shopping.Categories",
                l["Menu:Categories"],
                url: "/categories"
            ));
        }


        if (MultiTenancyConsts.IsEnabled)
        {
            administration.SetSubItemOrder(TenantManagementMenuNames.GroupName, 1);
        }
        else
        {
            administration.TryRemoveMenuItem(TenantManagementMenuNames.GroupName);
        }

        administration.SetSubItemOrder(IdentityMenuNames.GroupName, 2);
        administration.SetSubItemOrder(SettingManagementMenus.GroupName, 3);

        //return await Task.CompletedTask;
    }
}
