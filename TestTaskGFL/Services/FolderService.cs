using Microsoft.EntityFrameworkCore;
using TestTaskGFL.Models;
using TestTaskGFL.Models.Contexts;

namespace TestTaskGFL.Services
{
    public class FolderService : IFolderService
    {
        private readonly FoldersContext _foldersContext;

        public FolderService(FoldersContext foldersContext)
        {
            _foldersContext = foldersContext;
        }

        public async Task<Folder?> GetFolderByIdAsync(int folderId)
        {
            Folder? folder = await _foldersContext.Folders.Include(x => x.ChildFolderes).Where(x => x.FolderId == folderId).FirstOrDefaultAsync();
            return folder;
        }

        public async Task<Folder?> GetRootFolderAsync()
        {
            Folder? folder = await _foldersContext.Folders.Include(x => x.ChildFolderes).Where(x => x.ParentFolder == null).FirstOrDefaultAsync();
            return folder;
        }

        public async Task<IEnumerable<Folder>> GetRootFoldersAsync()
        {
            return await _foldersContext.Folders.Where(x => x.ParentFolder == null).ToListAsync();
        }
    }
}
