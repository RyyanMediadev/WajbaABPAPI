global using Wajba.Models.CompanyDomain;
global using Wajba.Models.CurrenciesDomain;
global using Wajba.Models.FaqsDomain;
global using Wajba.Models.LanguageDomain;
global using Wajba.Models.SiteDomain;
global using Wajba.Models.ThemesDomain;
global using Wajba.Models.TimeSlotsDomain;
global using Wajba.Models.OrderSetup;
global using Wajba.Models.PopularItemsDomain;
global using Wajba.Models.NotificationDomain;
global using Wajba.Models.UsersDomain;
global using Wajba.Models.WajbaUserRoleDomain;
global using Wajba.Models.WajbaUserDomain;
global using Wajba.Models.PushNotificationDomains;
using Wajba.Models.OrdersDomain;
using Wajba.Models.Orders;

namespace Wajba.EntityFrameworkCore;


[ConnectionStringName("Default")]


public class WajbaDbContext : 
AbpDbContext<WajbaDbContext>

{
   
    #region Entities from the modules

    //Identity
    public DbSet<IdentityUser> Users { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<IdentityLinkUser> LinkUsers { get; set; }
    public DbSet<IdentityUserDelegation> UserDelegations { get; set; }
    public DbSet<IdentitySession> Sessions { get; set; }
    // Tenant Management
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

    #endregion
    #region entities

    public DbSet<APPUser> CustomUsers { get; set; }
    public DbSet<Item> Items { get; set; }
    //public DbSet<address> addresses { get; set; }
    public DbSet<ItemVariation> ItemVariations { get; set; }
  public DbSet<PopularItem> PopularItems { get; set; }
    public DbSet<PopulartItemBranches> PopulartItemBranches { get; set; }
    public DbSet<ItemAddon> ItemAddons { get; set; }
    public DbSet<ItemExtra> ItemExtras { get; set; }
    public DbSet<ItemAttribute> ItemAttributes { get; set; }
    public DbSet<Offer> Offers { get; set; }
    public DbSet<OfferItem> OfferItems { get; set; }
    public DbSet<OfferCategory> OfferCategories { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Language> Languages { get; set; }
    public DbSet<Currencies> Currencies { get; set; }
    public DbSet<Coupon> Coupons { get; set; }
    public DbSet<ItemTax> ItemTaxes { get; set; }
    public DbSet<ItemBranch> itemBranches { get; set; }
    public DbSet<Branch> Branches { get; set; }
    public DbSet<Theme> Themes { get; set; }
    public DbSet<Site> Sites { get; set; }
    public DbSet<TimeSlot> TimeSlots { get; set; }
    public DbSet<DineInTable> DineInTables { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<FAQs> FAQs { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<PushNotification> PushNotifications { get; set; }
    public DbSet<OTP> OTPs { get; set; }
    public DbSet<OrderSetup> OrderSetups { get; set; }
    public DbSet<WajbaUser> WajbaUsers { get; set; }
	public DbSet<WajbaUserRole> WajbaUserRoles { get; set; }

	public DbSet<WajbaUserBranch> WajbaUserBranches { get; set; }

    public DbSet<WajbaUserAddress> WajbaUserAddresses { get; set; }



    public DbSet<PosOrder> PosOrders { get; set; }
    public DbSet<PosDeliveryOrder> PosDeliveryOrders { get; set; }
    public DbSet<Order> Order { get; set; }
    public DbSet<DineInOrder> DineInOrders { get; set; }
    public DbSet<DeliveryOrder> Deliveries { get; set; }
    public DbSet<DriveThruOrder> DriveThruOrders { get; set; }
    public DbSet<PickUpOrder> PickUpOrders { get; set; }


    #endregion
    public WajbaDbContext(DbContextOptions<WajbaDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* Include modules to your migration db context */

        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureIdentity();
        builder.ConfigureOpenIddict();
        builder.ConfigureFeatureManagement();
        builder.ConfigureTenantManagement();
        builder.Entity<ItemBranch>()
.HasKey(p => new { p.BranchId, p.ItemId });
        builder.Entity<PopulartItemBranches>()
            .HasKey(p => new { p.BranchId, p.PopularItemId });
        /* Configure your own tables/entities inside here */

        builder.ApplyConfigurationsFromAssembly(typeof(WajbaDbContext).Assembly);
    }
}
