using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TestTaskGFL.Models.TypeConfigurations
{
    //Configuration of folder entity
    public class FolderTypeConfiguration : IEntityTypeConfiguration<Folder>
    {
        public void Configure(EntityTypeBuilder<Folder> builder)
        {
            //One to many self-relation, one folder can have a lot of child folders, but child folder only have one parant folder 
            builder.HasOne(x => x.ParentFolder).WithMany(y => y.ChildFolderes).OnDelete(DeleteBehavior.Restrict);
            //Initial data
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
