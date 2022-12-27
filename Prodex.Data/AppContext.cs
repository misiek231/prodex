using Microsoft.EntityFrameworkCore;

namespace Prodex.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }


        public DbSet<Process> Processes => Set<Process>();

    }
}
