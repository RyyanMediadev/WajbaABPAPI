namespace Wajba.Permissions;

public static class WajbaPermissions
{
    //public const string GroupName = "Wajba";

    //Add your own permission names. Example:
    //public const string MyPermission1 = GroupName + ".MyPermission1";






    public const string MainGroupName = "Wajba";

    //Product Group & Permissions
    public const string CategoryGroupName = MainGroupName + ".Categories";
    public const string CreateCategoryPermission = CategoryGroupName + ".CreateAsync";

    public const string UpdateCategoryPermission = CategoryGroupName + ".UpdateAsync";
    public const string GetListCategoryPermission = CategoryGroupName + ".GetListAsyncAsync";
    public const string GetCategoryItemsCategoryPermission = CategoryGroupName + ".GetCategoryItemsdto";

    public const string DeleteCategoryPermission = CategoryGroupName + ".DeleteAsync";

}
