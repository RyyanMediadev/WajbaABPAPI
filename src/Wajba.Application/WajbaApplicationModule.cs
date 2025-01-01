global using Microsoft.Extensions.DependencyInjection;
global using Volo.Abp.Account;
global using Volo.Abp.AutoMapper;
global using Volo.Abp.FeatureManagement;
global using Volo.Abp.Identity;
global using Volo.Abp.Modularity;
global using Volo.Abp.PermissionManagement;
global using Volo.Abp.SettingManagement;
global using Volo.Abp.TenantManagement;

namespace Wajba;

[DependsOn(
    typeof(WajbaDomainModule),
    typeof(AbpAccountApplicationModule),
    typeof(WajbaApplicationContractsModule),
    typeof(AbpIdentityApplicationModule),
    typeof(AbpPermissionManagementApplicationModule),
    typeof(AbpTenantManagementApplicationModule),
    typeof(AbpFeatureManagementApplicationModule),
    typeof(AbpSettingManagementApplicationModule)
    )]
public class WajbaApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<WajbaApplicationModule>();

        });
        context.Services.AddTransient<IImageService, ImageService>();
    }
}
