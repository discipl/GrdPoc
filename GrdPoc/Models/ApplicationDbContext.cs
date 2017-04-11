using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using GrdPoc.Models.Entities;

namespace GrdPoc.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<BlockchainNodeServer> BlockchainNodeServers { get; set; }
        public DbSet<BlockchainAccount> BlockchainAccounts { get; set; }
        public DbSet<SmartContract> SmartContracts { get; set; }

        //public DbSet<BaseIncidentalContract> BaseIncidentalContracts { get; set; }
        public DbSet<Budget> Budgets { get; set; }
        public DbSet<BudgetBalanceTransaction> BudgetBalanceTransactions { get; set; }
        public DbSet<IncidentalContract> IncidentalContracts { get; set; }
        public DbSet<IncidentalContractItem> IncidentalContractItems { get; set; }
        public DbSet<IncidentalContracType> IncidentalContracTypes { get; set; }

        public DbSet<ExecutionProject> ExecutionProjects { get; set; }

        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceItem> InvoiceItems { get; set; }

        public DbSet<MunicipalEntity> MunicipalEntities { get; set; }
        public DbSet<MunicipalEntityType> MunicipalEntityTypes { get; set; }
        public DbSet<PersonaType> PersonaTypes { get; set; }
        public DbSet<ProductCode> ProductCodes { get; set; }
        public DbSet<ProductCodeCategory> ProductCodeCategories { get; set; }
        public DbSet<ProductIncidentalContract> ProductIncidentalContracts { get; set; }
        public DbSet<ServiceIncidentalContract> ServiceIncidentalContracts { get; set; }
        public DbSet<TrainningAttendee> TrainningAttendees { get; set; }
        public DbSet<TrainningIncidentalContract> TrainningIncidentalContracts { get; set; }
        public DbSet<UserAccount> UserAccounts { get; set; }

        //public System.Data.Entity.DbSet<GrdPoc.Models.ViewModels.ServiceContractViewModel> ServiceContractViewModels { get; set; }
    }
}