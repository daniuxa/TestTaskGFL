using TestTaskGFL.Models;

namespace TestTaskGFL.Services
{
    public interface IFolderService
    {
        Task<IEnumerable<Folder>> GetFoldersAsync(int? parentFolderId = null);
        Task<Folder?> GetRootFolderAsync();
        Task<IEnumerable<Folder>> GetRootFoldersAsync();
        Task<Folder?> GetFolderByIdAsync(int folderId);
        Task<Folder> AddFolderAsync(Folder folder);
        Task AddFolderRangeAsync(IEnumerable<Folder> folders, int? parentFolderId);
        Task SaveChangesAsync();
    }
}
