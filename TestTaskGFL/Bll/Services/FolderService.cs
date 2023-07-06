using Microsoft.EntityFrameworkCore;
using TestTaskGFL.Models;
using TestTaskGFL.Models.Contexts;

namespace TestTaskGFL.Services
{
    public class FolderService : IFolderService
    {
        //Db context
        private readonly FoldersContext _foldersContext;

        public FolderService(FoldersContext foldersContext)
        {
            _foldersContext = foldersContext;
        }

        //Add one folder to database
        public async Task<Folder> AddFolderAsync(Folder folder)
        {
            await _foldersContext.Folders.AddAsync(folder);
            return folder;
        }

        //Add folders to the data base
        //folders - folders that we want to add
        //parentFolderId - folder id, in which we want to add this collection of folders
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

        //Get folder by id with included collection of childrens
        public async Task<Folder?> GetFolderByIdAsync(int folderId)
        {
            Folder? folder = await _foldersContext.Folders.Include(x => x.ChildFolderes).Where(x => x.FolderId == folderId).FirstOrDefaultAsync();
            return folder;
        }

        //Get folders,
        //if parentFolderId != null, get only folders which are included in this folder,
        //if parentFolderId == null, get all collection of folders
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
            GetFoldersRecursive(parentFolder, folders);
            return folders;
        }

        //Recursive function to get included folders into the folder
        //folder - root folder, included folders in this folder we want to get
        //folders - list of included folders
        private void GetFoldersRecursive(Folder folder, List<Folder> folders)
        {
            //Add currently root folder
            folders.Add(folder);
            //Load child folders
            _foldersContext.Entry(folder)
                .Collection(f => f.ChildFolderes)
                .Load();
            //Add childrens and theirs childrens
            foreach (var item in folder.ChildFolderes)
            {
                GetFoldersRecursive(item, folders);
            }
        }
        //Get root folders, which don't have parent folder
        public async Task<IEnumerable<Folder>> GetRootFoldersAsync()
        {
            return await _foldersContext.Folders.Where(x => x.ParentFolder == null).ToListAsync();
        }

        //Save changes in data base
        public async Task SaveChangesAsync()
        {
            //Start transaction
            using (var transaction = _foldersContext.Database.BeginTransaction())
            {
                try
                {
                    //To insert folders with Id
                    _foldersContext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Folders ON");
                    await _foldersContext.SaveChangesAsync();
                    _foldersContext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Folders OFF");

                    //Commit changes
                    transaction.Commit();
                }
                catch (Exception)
                {
                    //Rollback changes
                    transaction.Rollback();
                    // Rethrow the exception for error handling/logging
                    throw; 
                }
            }
        }
    }
}
