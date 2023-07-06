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
        private readonly ILogger<HomeController> _logger;
        private readonly IFolderService _folderService;
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger, IFolderService folderService, IMapper mapper)
        {
            _logger = logger;
            _folderService = folderService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> IndexAsync()
        {
            return View(await _folderService.GetRootFoldersAsync());
        }
        [HttpGet]
        public async Task<IActionResult> FolderTreeAsync(int folderId = -1)
        {
            if (folderId == -1)
            {
                return View(await _folderService.GetRootFolderAsync());
            }
            return View(await _folderService.GetFolderByIdAsync(folderId));
        }
        [HttpPost]
        public async Task<IActionResult> ImportCatalogAsync(IFormFile fileInput, int? parentFolderId = null)
        {
            IEnumerable<Folder> folders;
            if (fileInput == null || fileInput.Length == 0)
            {
                return BadRequest("No file was selected.");
            }
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
            if (folders == null)
            {
                return NoContent();
            }
            await _folderService.AddFolderRangeAsync(folders, parentFolderId);
            await _folderService.SaveChangesAsync();
            return NoContent();
            //return Json(new { folders });
        }
        public async Task<IActionResult> ExportCatalogAsync(int? parentFolderId = null)
        {
            IEnumerable<Folder> folders = await _folderService.GetFoldersAsync(parentFolderId);
            string json = JsonConvert.SerializeObject(_mapper.Map<IEnumerable<FolderDTO>>(folders), Formatting.Indented);
            byte[] byteArray = Encoding.UTF8.GetBytes(json);

            return File(byteArray, "application/json", "folders.json");
        }
    }
}