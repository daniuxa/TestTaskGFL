using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TestTaskGFL.Models;
using TestTaskGFL.Models.Contexts;
using TestTaskGFL.Services;

namespace TestTaskGFL.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IFolderService _folderService;

        public HomeController(ILogger<HomeController> logger, IFolderService folderService)
        {
            _logger = logger;
            _folderService = folderService;
        }

        public async Task<IActionResult> IndexAsync()
        {
            return View(await _folderService.GetRootFoldersAsync());
        }

        public async Task<IActionResult> FolderTreeAsync(int folderId = -1)
        {
            if (folderId == -1)
            {
                return View(await _folderService.GetRootFolderAsync());
            }
            return View(await _folderService.GetFolderByIdAsync(folderId));
        }

    }
}