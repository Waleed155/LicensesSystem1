using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
namespace Licenses.Models
{
    public class LicensesContext:DbContext
    {
        public LicensesContext (DbContextOptions<LicensesContext> options):base(options)
        {

        }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Lot>Lots { get; set; }
        public DbSet<ExcutivePosition> Excutives { get; set; }
        public DbSet<ActivityType> ActivityTypes { get; set; }
        public DbSet<LotOrder> LotOrders { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderSteps> OrderSteps { get; set; }
        public DbSet<Step> Steps { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<LotOrderSteps> LotOrdersSteps { get; set; }
        public DbSet<TransactionLotOrder> TransactionLotOrders {  get; set; }
        public DbSet<Fees> Fees { get; set; }
        public DbSet <TransactionLotOrderStages> TransactionLotOrderStages { get; set; }
        public DbSet<TransactionStages> TransactionStages { get; set; }
        public DbSet<Stage> Stages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.
                Entity<TransactionLotOrder>().
                HasIndex(x => x.TransactionNumber).IsUnique();
            modelBuilder.Entity<TransactionStages>().
                HasIndex(x => x.StageNumber).
                IsUnique(); 
            modelBuilder.Entity<OrderSteps>().
                HasIndex(x=>x.StepNumber).
                IsUnique();
            modelBuilder.
                Entity<Lot>().
                HasIndex(x => new { x.LotNum, x.AreaName, x.NeighborhoodName }).
                IsUnique();
        }

    }
}
