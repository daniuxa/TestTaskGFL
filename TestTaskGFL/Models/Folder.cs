using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestTaskGFL.Models
{
    [Table("Folders")]
    public class Folder
    {
        [Key]
        public int FolderId { get; set; }
        [Required]
        public string FolderName { get; set; } = null!;

        [ForeignKey("ParentFolderId")]
        public Folder? ParentFolder { get; set; }
        public int? ParentFolderId { get; set; }

        public IEnumerable<Folder> ChildFolderes { get; set; } = new List<Folder>();
    }
}
