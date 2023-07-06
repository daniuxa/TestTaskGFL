using Microsoft.EntityFrameworkCore;

namespace TestTaskGFL.Models.Contexts
{
    //Db context
    public class FoldersContext : DbContext
    {
        //DbSet of folders
        public DbSet<Folder> Folders { get; set; } = null!;
        public FoldersContext(DbContextOptions<FoldersContext> options) : base(options)
        {   }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(FoldersContext).Assembly);
        }
    }
}
