using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Modularity;

namespace Wajba.CustomIdentity
{
    [DependsOn(typeof(AbpIdentityModule), typeof(AbpTenantManagementModule))]
    public class CustomIdentityModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            // Configure custom services here
        }
    }
}
