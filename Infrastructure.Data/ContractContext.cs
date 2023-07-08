namespace Infrastructure.Data
{
    using ContractBC.ContractAggregate;
    using Microsoft.EntityFrameworkCore;

    public class ContractContext : DbContext
    {
        public ContractContext(DbContextOptions options) : base(options) { }

        public DbSet<Contract> Contracts => Set<Contract>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ContractVersion>()
                .OwnsMany(ver => ver.Authors)
                .OwnsOne(a => a.Name);
            modelBuilder.Entity<ContractVersion>()
                .OwnsOne(ver => ver.Specs);
        }
    }
}
