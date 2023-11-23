using Microsoft.EntityFrameworkCore;

namespace AdventureWorks.Data
{
    public class AdventureWorksDbContext : DbContext
    {
        public DbSet<SalesOrderDetail> SalesOrderDetail { get; set; }

        public AdventureWorksDbContext(DbContextOptions<AdventureWorksDbContext> options)
        : base(options)
        {

        }

        protected void BuildModel(ModelBuilder modelBuilder)
        {
        }
    }
}