using TestTaskGFL.Models;

namespace TestTaskGFL.Services
{
    public interface IFolderService
    {
        Task<Folder?> GetRootFolderAsync();
        Task<IEnumerable<Folder>> GetRootFoldersAsync();
        Task<Folder?> GetFolderByIdAsync(int folderId);
    }
}
