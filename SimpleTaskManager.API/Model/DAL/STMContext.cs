using Microsoft.EntityFrameworkCore;
using SimpleTaskManager.API.Model.BLL;
using SimpleTaskManager.API.Model.DAL.Configurations;

namespace SimpleTaskManager.API.DAL
{
    public class STMContext : DbContext
    {
        public STMContext(DbContextOptions<STMContext> options) :base(options)
        {

        }
        public DbSet<SimpleTask> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new SimpleTaskConfiguration());
        }
    }
}
