using Swashbuckle.AspNetCore.SwaggerGen;

namespace Wajba.SwaggerFilters
{
    public class HideBuiltInEndpointsDocumentFilter : IDocumentFilter
    {
        private readonly string[] _blockedPaths = new[]
        {
            "/api/users",
            "/api/users/", 
            "/api/roles",
            "/api/roles/",
            "/api/permissions",
            "/api/permissions/",
            "/api/identity/users",
            "/api/identity/users/",
            "/api/Account",
             "/api/account/verify-password-reset-token",
             "/api/account/register",
              "/api/account/login",
               "/api/account/logout",
                "/api/account/check-password",
             "/api/account/reset-password",
             "/api/account/send-password-reset-code",
            "/api/identity/roles",
            "/api/identity/roles/",
            "/api/identity/permissions",
            "/api/identity/permissions/",
            "/api/abp/api-definition",
            "/api/abp/application-configuration",
            "/api/abp/application-localization",
             "/api/multi-tenancy/tenants",
            "/api/multi-tenancy/tenants/",
            "/api/abp/multi-tenancy/tenants/by-name/{name}",
            "/api/abp/multi-tenancy/tenants/by-id/{id}",
             "/api/User",
             "/api/Home",
              "/api/permission-management/permissions",
                "/api/setting-management/emailing",
                  "/api/setting-management/emailing/send-test-email",
                   "/api/setting-management/timezones",
                    "/api/setting-management/timezone",
                    "/api/setting-management/timezone/timezones",
                     "/api/feature-management/features",

        };

        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            foreach (var path in _blockedPaths)
            {
                var keysToRemove = swaggerDoc.Paths.Keys
                    .Where(p => p.StartsWith(path, System.StringComparison.OrdinalIgnoreCase))
                    .ToList();

                foreach (var key in keysToRemove)
                {
                    swaggerDoc.Paths.Remove(key);
                }
            }
        }
    }
}

