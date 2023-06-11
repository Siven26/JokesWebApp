using JokesWebApp.Services.ViewModels;
using JokesWebApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace JokesWebApp.Controllers
{
    public class ShowmanController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ShowmanService showmanService { get; set; }

        public ShowmanController(ShowmanService service, IWebHostEnvironment webHostEnvironment)
        {
            showmanService = service;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult Showmans()
        {
            List<ShowmanViewModel> showmans = showmanService.GetAllShowmans();

            return this.View(showmans);
        }

        [HttpGet]
        public IActionResult AddShowman()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddShowman(ShowmanViewModel showmanVM, IFormFile? file)
        {
            if (file != null)
            {
                await showmanService.SetImage(showmanVM, file);
            }

            await showmanService.CreateShowmanAsync(showmanVM);
            return RedirectToAction(nameof(Showmans));
        }

        [HttpGet]
        public IActionResult Update(string id)
        {
            var model = showmanService.UpdateShowmanById(id);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateShowman(ShowmanViewModel model, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    DeleteOldShowmanImage(model.ShowmanID);

                    showmanService.SetImage(model, file);
                }

                showmanService.UpdateShowman(model);

                return RedirectToAction("Showmans");
            }

            return View(model);
        }



        private async Task DeleteOldShowmanImage(string showmanId)
        {
            ShowmanViewModel existingShowman = showmanService.GetShowmanDetailsById(showmanId);

            if (existingShowman != null)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string imagePath = Path.Combine(wwwRootPath, "images", "showman", existingShowman.ShowmanImage);

                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);

                    existingShowman.ShowmanImage = null;

                    showmanService.UpdateShowman(existingShowman);
                }
            }
        }

        [HttpGet]
        public IActionResult DeleteShowman(string id)
        {
            if (string.IsNullOrEmpty(id) || string.IsNullOrWhiteSpace(id))
            {
                return BadRequest("Invalid showman id");
            }

            var showman = showmanService.GetShowmanDetailsById(id);
            if (showman == null)
            {
                return RedirectToAction("Showmans");
            }
            return View(showman);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteShowmanConfirmed(string showmanId)
        {
            if (string.IsNullOrEmpty(showmanId) || string.IsNullOrWhiteSpace(showmanId))
            {
                return BadRequest("Invalid showman id");
            }

            ShowmanViewModel existingShowman = showmanService.GetShowmanDetailsById(showmanId);

            if (existingShowman != null)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string imagePath = Path.Combine(wwwRootPath, "images", "showman", existingShowman.ShowmanImage);

                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }

                await showmanService.DeleteShowman(showmanId);
            }
            return RedirectToAction("Showmans");
        }
    }
}
