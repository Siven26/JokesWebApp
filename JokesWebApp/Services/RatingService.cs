using JokesWebApp.Data.DataModels;
using JokesWebApp.Data;
using JokesWebApp.Services.ViewModels;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using JokesWebApp.Services.Interfaces;

namespace JokesWebApp.Services
{
    public class RatingService : IRatingService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RatingService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public List<RatingViewModel> GetAllRatings(string id)
        {
            return _context.Ratings.Where(c => c.JokeID == id).Include(c => c.User).Include(c => c.Joke)
                .Select(rating => new RatingViewModel
                {
                    RatingID = rating.RatingID,
                    RatingValue = rating.RatingValue,
                    RatingDateAdded = rating.RatingDateAdded,
                    JokeID = rating.JokeID,
                    CreatorEmail = rating.User.Email,
                })
                .ToList();
        }

        public async Task CreateRatingAsync(RatingViewModel model)
        {
            string userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            Rating rating = new Rating
            {
                RatingID = Guid.NewGuid().ToString(),
                RatingValue = model.RatingValue,
                RatingDateAdded = DateTime.Now,
                JokeID = model.JokeID,
                UserID = userId
            };

            await _context.Ratings.AddAsync(rating);
            await _context.SaveChangesAsync();
        }

        public bool HasRatingForJoke(string jokeId, string userId)
        {
            return _context.Ratings.Any(r => r.JokeID == jokeId && r.UserID == userId);
        }
    }
}
