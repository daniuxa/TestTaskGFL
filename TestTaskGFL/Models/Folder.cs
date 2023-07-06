using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestTaskGFL.Models
{
    //Folder entity
    [Table("Folders")]
    public class Folder
    {
        //Folder Id, key value
        [Key]
        public int FolderId { get; set; }

        //Folder name, required field
        [Required]
        public string FolderName { get; set; } = null!;

        //Foreign entity to parent folder, non required
        [ForeignKey("ParentFolderId")]
        public Folder? ParentFolder { get; set; }
        //Foreign key to parent folder, non required
        public int? ParentFolderId { get; set; }

        //Collection of child folders of this folder
        public IEnumerable<Folder> ChildFolderes { get; set; } = new List<Folder>();
    }
}
