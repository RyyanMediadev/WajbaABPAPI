global using CloudinaryDotNet;
global using Microsoft.AspNetCore.Cors;
global using Microsoft.AspNetCore.Extensions.DependencyInjection;
global using Microsoft.OpenApi.Models;
global using OpenIddict.Validation.AspNetCore;
global using System.Collections.Generic;
global using System.IO;
global using System.Linq;
global using Volo.Abp;
global using Volo.Abp.Data;
global using Volo.Abp.Domain.Entities;
global using Volo.Abp.Domain.Repositories;
global using Volo.Abp.Account;
global using Volo.Abp.Account.Web;
global using Volo.Abp.AspNetCore.MultiTenancy;
global using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
global using Volo.Abp.AspNetCore.Mvc.UI.Theme.LeptonXLite;
global using Volo.Abp.AspNetCore.Mvc.UI.Theme.LeptonXLite.Bundling;
global using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
global using Volo.Abp.AspNetCore.Serilog;
global using Volo.Abp.Autofac;
global using Volo.Abp.Modularity;
global using Volo.Abp.Security.Claims;
global using Volo.Abp.Swashbuckle;
global using Volo.Abp.UI.Navigation.Urls;
global using Volo.Abp.VirtualFileSystem;
global using Wajba.CloudinaryConfigure;
global using Wajba.MultiTenancy;
using Wajba.Hubs;
using Wajba.Middleware;
using Wajba.SwaggerFilters;


namespace Wajba;

[DependsOn(
    typeof(WajbaHttpApiModule),
    typeof(AbpAutofacModule),
    typeof(AbpAspNetCoreMultiTenancyModule),
    typeof(WajbaApplicationModule),
    typeof(WajbaEntityFrameworkCoreModule),
    typeof(AbpAspNetCoreMvcUiLeptonXLiteThemeModule),
    typeof(AbpAccountWebOpenIddictModule),
    typeof(AbpAspNetCoreSerilogModule),
    typeof(AbpSwashbuckleModule)
    

)]
public class WajbaHttpApiHostModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<OpenIddictBuilder>(builder =>
        {
            builder.AddValidation(options =>
            {
                options.AddAudiences("Wajba");
                options.UseLocalServer();
                options.UseAspNetCore();
            });
        });

    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddSwaggerGen(options =>
        {
            
            // Register the custom Document Filter
            options.DocumentFilter<HideBuiltInEndpointsDocumentFilter>();
        });

        //configure signalr service
        // Adding SignalR
        context.Services.AddSignalR();

        // Add ABP Swashbuckle for Swagger UI integration
        //context.Services.AddAbpSwashbuckle();


        //context.Services.AddIdentity<APPUser, IdentityRole>()
        //  .AddEntityFrameworkStores<WajbaDbContext>()
        //  .AddDefaultTokenProviders();
        // Register other necessary services for UserManager<APPUser>
    //context.Services.AddTransient<IUserStore<APPUser>, UserStore<APPUser, IdentityRole, ApplicationDbContext>>();

        //context.Services.AddTransient<icustomuser, CustomUserAppService>();
        //context.Services.AddApplication<WajbaApplicationModule>();
        var configuration = context.Services.GetConfiguration();
        var hostingEnvironment = context.Services.GetHostingEnvironment();

        ConfigureAuthentication(context);
        ConfigureBundles();
        ConfigureUrls(configuration);
        ConfigureConventionalControllers();
        ConfigureVirtualFileSystem(context);
        ConfigureCors(context, configuration);
        ConfigureSwaggerServices(context, configuration);

        var cloudinarySettings = configuration.GetSection("Cloudinary").Get<CloudinarySettings>();

        context.Services.AddSingleton(sp =>
        {
            return new Cloudinary(new Account(
                cloudinarySettings.CloudName,
                cloudinarySettings.ApiKey,
                cloudinarySettings.ApiSecret
            ));
        });
    }

    private void ConfigureAuthentication(ServiceConfigurationContext context)
    {
        context.Services.ForwardIdentityAuthenticationForBearer(OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme);
        context.Services.Configure<AbpClaimsPrincipalFactoryOptions>(options =>
        {
            options.IsDynamicClaimsEnabled = true;
        });

        var configuration = context.Services.GetConfiguration();
        Configure<CloudinarySettings>(configuration.GetSection("CloudinarySettings"));
    }

    private void ConfigureBundles()
    {
        Configure<AbpBundlingOptions>(options =>
        {
            options.StyleBundles.Configure(
                LeptonXLiteThemeBundles.Styles.Global,
                bundle =>
                {
                    bundle.AddFiles("/global-styles.css");
                }
            );
        });
    }

    private void ConfigureUrls(IConfiguration configuration)
    {
        Configure<AppUrlOptions>(options =>
        {
            options.Applications["MVC"].RootUrl = configuration["App:SelfUrl"];
            options.RedirectAllowedUrls.AddRange(configuration["App:RedirectAllowedUrls"]?.Split(',') ?? Array.Empty<string>());
            options.Applications["Angular"].RootUrl = configuration["App:ClientUrl"];
            options.Applications["Angular"].Urls[AccountUrlNames.PasswordReset] = "account/reset-password";
        });
    }

    private void ConfigureVirtualFileSystem(ServiceConfigurationContext context)
    {
        var hostingEnvironment = context.Services.GetHostingEnvironment();

        if (hostingEnvironment.IsDevelopment())
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.ReplaceEmbeddedByPhysical<WajbaDomainSharedModule>(
                    Path.Combine(hostingEnvironment.ContentRootPath,
                        $"..{Path.DirectorySeparatorChar}Wajba.Domain.Shared"));
                options.FileSets.ReplaceEmbeddedByPhysical<WajbaDomainModule>(
                    Path.Combine(hostingEnvironment.ContentRootPath,
                        $"..{Path.DirectorySeparatorChar}Wajba.Domain"));
                options.FileSets.ReplaceEmbeddedByPhysical<WajbaApplicationContractsModule>(
                    Path.Combine(hostingEnvironment.ContentRootPath,
                        $"..{Path.DirectorySeparatorChar}Wajba.Application.Contracts"));
                options.FileSets.ReplaceEmbeddedByPhysical<WajbaApplicationModule>(
                    Path.Combine(hostingEnvironment.ContentRootPath,
                        $"..{Path.DirectorySeparatorChar}Wajba.Application"));
            });
        }
    }
    private void ConfigureConventionalControllers()
    {
        Configure<AbpAspNetCoreMvcOptions>(options =>
        {
            options.ConventionalControllers.Create(typeof(WajbaApplicationModule).Assembly);
        });
    }
    private static void ConfigureSwaggerServices(ServiceConfigurationContext context, IConfiguration configuration)
    {
        context.Services.AddAbpSwaggerGenWithOAuth(
            configuration["AuthServer:Authority"]!,
            new Dictionary<string, string>
            {
                    {"Wajba", "Wajba API"}
            },
            options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Wajba API", Version = "v1" });
                options.DocInclusionPredicate((docName, description) => true);
                options.CustomSchemaIds(type => type.FullName);
            });
    }

    private void ConfigureCors(ServiceConfigurationContext context, IConfiguration configuration)
    {
        context.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder
                    .WithOrigins(configuration["App:CorsOrigins"]?
                        .Split(",", StringSplitOptions.RemoveEmptyEntries)
                        .Select(o => o.RemovePostFix("/"))
                        .ToArray() ?? Array.Empty<string>())
                    .WithAbpExposedHeaders()
                    .SetIsOriginAllowedToAllowWildcardSubdomains()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var app = context.GetApplicationBuilder();
        var env = context.GetEnvironment();
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        app.UseAbpRequestLocalization();
        if (!env.IsDevelopment())
        {
            app.UseErrorPage();
        }
        app.UseCorrelationId();
        app.MapAbpStaticAssets();
        app.UseRouting();
      
        app.UseCors();
        app.UseAuthentication();
        app.UseAbpOpenIddictValidation();
        app.UseConfiguredEndpoints(endpoints =>
        {
            endpoints.MapHub<OfferHub>("/hubs/offer", options =>
            {
                options.LongPolling.PollTimeout = TimeSpan.FromSeconds(60);
            });
            endpoints.MapControllers();
        });

        if (MultiTenancyConsts.IsEnabled)
        {
            app.UseMultiTenancy();
        }
        app.UseUnitOfWork();
        app.UseDynamicClaims();
        app.UseAuthorization();
        app.UseMiddleware<BlockBuiltInEndpointsMiddleware>();
      

        app.UseSwagger();
        app.UseAbpSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Wajba API");
            c.RoutePrefix = string.Empty;
            var configuration = context.ServiceProvider.GetRequiredService<IConfiguration>();
            c.OAuthClientId(configuration["AuthServer:SwaggerClientId"]);
            c.OAuthScopes("Wajba");
        });

        app.UseStaticFiles();
        app.UseAuditing();
        app.UseAbpSerilogEnrichers();
        app.UseConfiguredEndpoints();
       
    }
}