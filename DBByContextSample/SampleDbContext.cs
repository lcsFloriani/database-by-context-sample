using Microsoft.EntityFrameworkCore;

namespace DBByContextSample
{
    public class SampleDbContext : DbContext
    {
        public DbSet<Printer> Printers { get; set; }

        public SampleDbContext(DbContextOptions<SampleDbContext> options) : base(options) => Database.EnsureCreated();
    }
}
