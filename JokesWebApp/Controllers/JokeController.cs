using JokesWebApp.Services;
using Microsoft.AspNetCore.Mvc;
using JokesWebApp.Services.ViewModels;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace JokesWebApp.Controllers
{
    public class JokeController : Controller
    {
        public JokeService jokeService { get; set; }

        public JokeController(JokeService service)
        {
            jokeService = service;
        }

        [HttpGet]
        public IActionResult Jokes()
        {
            List<JokeViewModel> jokes = jokeService.GetAll();

            return this.View(jokes);
        }

        [HttpGet]
        public IActionResult Details(string id)
        {
            JokeViewModel joke = jokeService.GetDetailsById(id);

            bool isJokeNull = joke == null;
            if (isJokeNull)
            {
                return this.RedirectToAction("Jokes");
            }

            return this.View(joke);
        }

        [HttpGet]
        [Authorize]
        public IActionResult AddJoke()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddJoke(JokeViewModel jokeVM)
        {
            await jokeService.CreateAsync(jokeVM);
            return RedirectToAction(nameof(Jokes));
        }

        [HttpGet]
        public IActionResult Update(string id)
        {
            var joke = jokeService.GetDetailsById(id);
            if (User.Identity.IsAuthenticated && User.FindFirstValue(ClaimTypes.Email) == joke.CreatorEmail || User.IsInRole("Admin"))
            {
                JokeViewModel jokeToUpdate = this.jokeService.UpdateById(id);
                return this.View(jokeToUpdate);
            }
            return RedirectToAction("WrongUser", "Home");
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> UpdateJoke(JokeViewModel model)
        {
            if (this.ModelState.IsValid == true)
            {
                return this.View(model);
            }

            await this.jokeService.UpdateAsync(model);

            return this.RedirectToAction("Jokes");
        }

        [HttpGet]
        public IActionResult DeleteJoke(string id)
        {
            var joke = jokeService.GetDetailsById(id);

            if (joke == null)
            {
                return BadRequest("Invalid joke id");
            }

            if (User.Identity.IsAuthenticated && User.FindFirstValue(ClaimTypes.Email) == joke.CreatorEmail || User.IsInRole("Admin"))
            {
                var jokeToDelete = jokeService.GetDetailsById(id);
                return View(jokeToDelete);
            }
            return RedirectToAction("WrongUser", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteJokeConfirmed(string id)
        {
            await jokeService.DeleteJoke(id);

            return RedirectToAction("Jokes");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PopulateJokes()
        {
            await jokeService.PopulateDatabaseWithJokes();
            return RedirectToAction(nameof(Jokes));
        }
    }
}
