using Microsoft.EntityFrameworkCore;

namespace TestTaskGFL.Models.Contexts
{
    public class FoldersContext : DbContext
    {
        public DbSet<Folder> Folders { get; set; } = null!;
        public FoldersContext(DbContextOptions<FoldersContext> options) : base(options)
        {   }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(FoldersContext).Assembly);
        }
    }
}
