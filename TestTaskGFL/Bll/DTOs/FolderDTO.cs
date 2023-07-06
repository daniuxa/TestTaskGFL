namespace TestTaskGFL.Bll.DTOs
{
    public class FolderDTO
    {
        public int FolderId { get; set; }
        public string FolderName { get; set; } = null!;
        public int? ParentFolderId { get; set; }
    }
}
