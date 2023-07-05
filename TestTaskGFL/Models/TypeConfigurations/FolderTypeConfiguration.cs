using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TestTaskGFL.Models.TypeConfigurations
{
    public class FolderTypeConfiguration : IEntityTypeConfiguration<Folder>
    {
        public void Configure(EntityTypeBuilder<Folder> builder)
        {
            builder.HasOne(x => x.ParentFolder).WithMany(y => y.ChildFolderes).OnDelete(DeleteBehavior.Restrict);
            builder.HasData(
                new Folder { FolderId = 1, FolderName = "Creating Digital Images", ParentFolderId = null},
                new Folder { FolderId = 2, FolderName = "Resources", ParentFolderId = 1 },
                new Folder { FolderId = 3, FolderName = "Evidence", ParentFolderId = 1 },
                new Folder { FolderId = 4, FolderName = "Graphic Products", ParentFolderId = 1 },
                new Folder { FolderId = 5, FolderName = "Primary Sources", ParentFolderId = 2 },
                new Folder { FolderId = 6, FolderName = "Secondary Sources", ParentFolderId = 2 },
                new Folder { FolderId = 7, FolderName = "Process", ParentFolderId = 4 },
                new Folder { FolderId = 8, FolderName = "Final Product", ParentFolderId = 4 }
                );
        }
    }
}
