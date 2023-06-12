using JokesWebApp.Services.ViewModels;
using JokesWebApp.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace JokesWebApp.Controllers
{
    public class RatingController : Controller
    {
        public RatingService ratingService { get; set; }

        public RatingController(RatingService service)
        {
            ratingService = service;
        }

        [HttpGet]
        public IActionResult Ratings(string id)
        {
            var ratings = ratingService.GetAllRatings(id);
            return View(ratings);
        }

        public IActionResult AlreadyRated()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public IActionResult AddRating(string jokeId)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            bool hasExistingRating = ratingService.HasRatingForJoke(jokeId, userId);
            if (hasExistingRating)
            {
                return RedirectToAction(nameof(AlreadyRated));
            }

            var model = new RatingViewModel { JokeID = jokeId };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddRating(RatingViewModel model)
        {
            await ratingService.CreateRatingAsync(model);

            return RedirectToAction("Ratings", new { id = model.JokeID });
        }
    }
}
