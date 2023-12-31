﻿namespace Infrastructure.Data
{
    using Microsoft.EntityFrameworkCore.Design;
    using Microsoft.EntityFrameworkCore;
    public class ContractContextFactory : IDesignTimeDbContextFactory<ContractContext>
    {
        public ContractContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ContractContext>();
            optionsBuilder.UseSqlServer(
             "Data Source= (localdb)\\MSSQLLocalDB; Initial Catalog=PubContractDataMigrationsTest"); ;
            return new ContractContext(optionsBuilder.Options);
        }
    }
}
