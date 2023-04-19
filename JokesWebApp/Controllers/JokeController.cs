using JokesWebApp.Services;
using Microsoft.AspNetCore.Mvc;
using JokesWebApp.Services.ViewModels;
using Microsoft.AspNetCore.Cors.Infrastructure;
using JokesWebApp.Data.DataModels;
using System.Net;

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

            bool isCourseNull = joke == null;
            if (isCourseNull)
            {
                return this.RedirectToAction("Jokes");
            }

            return this.View(joke);
        }

        [HttpGet]
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
            JokeViewModel joke = this.jokeService.UpdateById(id);

            return this.View(joke);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Update(JokeViewModel model)
        {
            if (this.ModelState.IsValid == false)
            {
                return this.View(model);
            }

            await this.jokeService.UpdateAsync(model);

            return this.RedirectToAction("Jokes");
        }
    }
}
