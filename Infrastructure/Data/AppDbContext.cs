using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {            
        }
        public DbSet<ApplicationBlog> Blogs { get; set; }
        public DbSet<ApplicationCompanyProfile> CompanyProfiles { get; set; }
        public DbSet<ApplicationDownload> Downloads { get; set; }
        public DbSet<ApplicationFinancialKPI> FinancialKPIs { get; set; }
        public DbSet<ApplicationHomeContent> HomeContents { get; set; }
        public DbSet<ApplicationHomeContentMid> HomeContentMids { get; set; }
        public DbSet<ApplicationImageMaster> ImageMasters { get; set; }
        public DbSet<ApplicationMarketKPI> MarketKPIs { get; set; }
        public DbSet<ApplicationNewsAnnounce> NewsAnnounces { get; set; }
        public DbSet<ApplicationOperationalKPI> OperationalKPIs { get; set; }
        public DbSet<ApplicationValuationKPI> ValuationKPIs { get; set; }
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<ApplicationRole> Roles { get; set; }
        public DbSet<ApplicationPermission> Permissions { get; set; }
        public DbSet<ApplicationUserRole> UserRoles { get; set; }
        public DbSet<ApplicationRolePermission> RolePermissions { get; set; }
        public DbSet<ApplicationFaq> Faqs { get; set; }
        public DbSet<ApplicationPromoConfig> PromoConfigs { get; set; }
        public DbSet<ErrorLog> ErrorLogs { get; set; }
    }
}
