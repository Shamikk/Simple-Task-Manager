using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleTaskManager.API.Model.BLL;

namespace SimpleTaskManager.API.Model.DAL.Configurations
{
    public class SimpleTaskConfiguration : IEntityTypeConfiguration<SimpleTask>
    {
        public void Configure(EntityTypeBuilder<SimpleTask> builder)
        {
            builder.ToTable("SimpleTask");

            builder.HasKey(k => k.Name);
        }
    }
}
