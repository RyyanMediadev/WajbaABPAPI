using Wajba.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Wajba.Permissions;

public class WajbaPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        // var myGroup = context.AddGroup(WajbaPermissions.MainGroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(WajbaPermissions.MyPermission1, L("Permission:MyPermission1"));

        var myGroup = context.AddGroup(WajbaPermissions.MainGroupName);
        var CategoryGroup = context.AddGroup(WajbaPermissions.CategoryGroupName, L("Wajba.Categories"));

        CategoryGroup.AddPermission(WajbaPermissions.CreateCategoryPermission, L("Permission:Categories:CreateCategory"));
        CategoryGroup.AddPermission(WajbaPermissions.UpdateCategoryPermission, L("Permission:Categories:UpdateCategory"));

        CategoryGroup.AddPermission(WajbaPermissions.GetCategoryItemsCategoryPermission, L("Permission:Categories:GetCategoryItemsCategory"));
        CategoryGroup.AddPermission(WajbaPermissions.GetListCategoryPermission, L("Permission:Categories:GetListCategory"));
        CategoryGroup.AddPermission(WajbaPermissions.DeleteCategoryPermission, L("Permission:Products:DeleteCategory"));

    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<WajbaResource>(name);
    }
}
