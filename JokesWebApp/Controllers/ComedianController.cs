using JokesWebApp.Services.ViewModels;
using JokesWebApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace JokesWebApp.Controllers
{
    public class ComedianController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ComedianService comedianService { get; set; }

        public ComedianController(ComedianService service, IWebHostEnvironment webHostEnvironment)
        {
            comedianService = service;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult Comedians()
        {
            List<ComedianViewModel> comedians = comedianService.GetAllComedians();

            return this.View(comedians);
        }

        [HttpGet]
        public IActionResult AddComedian()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddComedian(ComedianViewModel comedianVM, IFormFile? file)
        {
            if (file != null)
            {
                await comedianService.SetImage(comedianVM, file);
            }

            await comedianService.CreateComedianAsync(comedianVM);
            return RedirectToAction(nameof(Comedians));
        }

        [HttpGet]
        public IActionResult Update(string id)
        {
            var model = comedianService.UpdateComedianById(id);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateComedian(ComedianViewModel model, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    DeleteOldComedianImage(model.ComedianID);

                    comedianService.SetImage(model, file);
                }

                comedianService.UpdateComedian(model);

                return RedirectToAction("Comedians");
            }

            return View(model);
        }



        private async Task DeleteOldComedianImage(string comedianId)
        {
            ComedianViewModel existingComedian = comedianService.GetComedianDetailsById(comedianId);

            if (existingComedian != null)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string imagePath = Path.Combine(wwwRootPath, "images", "comedian", existingComedian.ComedianImage);

                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);

                    existingComedian.ComedianImage = null;

                    comedianService.UpdateComedian(existingComedian);
                }
            }
        }

        [HttpGet]
        public IActionResult DeleteComedian(string id)
        {
            if (string.IsNullOrEmpty(id) || string.IsNullOrWhiteSpace(id))
            {
                return BadRequest("Invalid comedian id");
            }

            var comedian = comedianService.GetComedianDetailsById(id);
            if (comedian == null)
            {
                return RedirectToAction("Comedians");
            }
            return View(comedian);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteComedianConfirmed(string comedianId)
        {
            if (string.IsNullOrEmpty(comedianId) || string.IsNullOrWhiteSpace(comedianId))
            {
                return BadRequest("Invalid comedian id");
            }

            ComedianViewModel existingComedian = comedianService.GetComedianDetailsById(comedianId);

            if (existingComedian != null)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string imagePath = Path.Combine(wwwRootPath, "images", "comedian", existingComedian.ComedianImage);

                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }

                await comedianService.DeleteComedian(comedianId);
            }
            return RedirectToAction("Comedians");
        }
    }
}
