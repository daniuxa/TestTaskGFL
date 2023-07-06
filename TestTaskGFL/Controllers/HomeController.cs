using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;
using TestTaskGFL.Bll.DTOs;
using TestTaskGFL.Models;
using TestTaskGFL.Models.Contexts;
using TestTaskGFL.Services;

namespace TestTaskGFL.Controllers
{
    public class HomeController : Controller
    {
        //Folder service
        private readonly IFolderService _folderService;
        //Auto mapper
        private readonly IMapper _mapper;

        public HomeController(IFolderService folderService, IMapper mapper)
        {
            _folderService = folderService;
            _mapper = mapper;
        }

        //Start up
        [HttpGet]
        public async Task<IActionResult> IndexAsync()
        {
            return View(await _folderService.GetRootFoldersAsync());
        }

        //Get a tree of structure of folders
        [HttpGet]
        public async Task<IActionResult> FolderTreeAsync(int folderId)
        {
            return View(await _folderService.GetFolderByIdAsync(folderId));
        }

        //Import folders from the jsom file,
        //fileInput - json file, where we get structure of folders
        //parentFolderId - can be imported in specific folder
        //if parentFolderId == null, import into the root
        [HttpPost]
        public async Task<IActionResult> ImportFoldersAsync(IFormFile fileInput, int? parentFolderId = null)
        {
            IEnumerable<Folder> folders;

            if (fileInput == null || fileInput.Length == 0)
            {
                return BadRequest("No file was selected.");
            }

            //Reading the file stream
            using (var reader = new StreamReader(fileInput.OpenReadStream()))
            {
                try
                {
                    var jsonString = reader.ReadToEnd();
                    folders = JsonConvert.DeserializeObject<IEnumerable<Folder>>(jsonString);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            //if folder is empty
            if (folders == null)
            {
                return NoContent();
            }

            //Add folders and save changes
            await _folderService.AddFolderRangeAsync(folders, parentFolderId);
            await _folderService.SaveChangesAsync();
            return NoContent();
            //return Json(new { folders });
        }

        //Export folders from the database to the json file
        //parentFolderId - folder from which we export structure
        //if parentFolderId == null, export from the root
        public async Task<IActionResult> ExportFoldersAsync(int? parentFolderId = null)
        {
            IEnumerable<Folder> folders = await _folderService.GetFoldersAsync(parentFolderId);
            string json = JsonConvert.SerializeObject(_mapper.Map<IEnumerable<FolderDTO>>(folders), Formatting.Indented);
            byte[] byteArray = Encoding.UTF8.GetBytes(json);

            return File(byteArray, "application/json", "folders.json");
        }
    }
}