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

        public async Task<Folder> AddFolderAsync(Folder folder)
        {
            await _foldersContext.Folders.AddAsync(folder);
            return folder;
        }

        public async Task AddFolderRangeAsync(IEnumerable<Folder> folders, int? parentFolderId = null)
        {
            if (parentFolderId == null)
            {
                await _foldersContext.Folders.AddRangeAsync(folders);
                return;
            }

            foreach (var item in folders)
            {
                if (item.ParentFolderId == null)
                {
                    await _foldersContext.Folders.AddAsync(
                        new Folder { FolderId = item.FolderId, FolderName = item.FolderName, ParentFolderId = parentFolderId});
                }
                else
                {
                    await _foldersContext.Folders.AddAsync(item);
                }
            }
            return;
        }

        public async Task<Folder?> GetFolderByIdAsync(int folderId)
        {
            Folder? folder = await _foldersContext.Folders.Include(x => x.ChildFolderes).Where(x => x.FolderId == folderId).FirstOrDefaultAsync();
            return folder;
        }

        public async Task<IEnumerable<Folder>> GetFoldersAsync(int? parentFolderId = null)
        {
            List<Folder> folders = new List<Folder>();
            if (parentFolderId == null)
            {
                return await _foldersContext.Folders.ToListAsync();
            }
            Folder? parentFolder = await GetFolderByIdAsync((int)parentFolderId);
            if (parentFolder == null)
            {
                return folders;
            }
            AddFoldersRecursive(parentFolder, folders);
            return folders;
        }
        private void AddFoldersRecursive(Folder folder, List<Folder> folders)
        {
            folders.Add(folder);
            _foldersContext.Entry(folder)
                .Collection(f => f.ChildFolderes)
                .Load();
            foreach (var item in folder.ChildFolderes)
            {
                AddFoldersRecursive(item, folders);
            }
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

        public async Task SaveChangesAsync()
        {
            using (var transaction = _foldersContext.Database.BeginTransaction())
            {
                try
                {
                    _foldersContext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Folders ON");
                    await _foldersContext.SaveChangesAsync();
                    _foldersContext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Folders OFF");
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw; // Rethrow the exception for error handling/logging
                }
            }
        }
    }
}
