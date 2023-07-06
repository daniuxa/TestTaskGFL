namespace TestTaskGFL.Bll.DTOs
{
    //Folder DTO for export the folders
    public class FolderDTO
    {
        //Folder id
        public int FolderId { get; set; }

        //Folder name
        public string FolderName { get; set; } = null!;

        //Foreign key to parent folder
        public int? ParentFolderId { get; set; }
    }
}
