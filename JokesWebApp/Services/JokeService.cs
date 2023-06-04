using JokesWebApp.Data;
using JokesWebApp.Data.DataModels;
using JokesWebApp.Services.ViewModels;
using JokesWebApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace JokesWebApp.Services
{
    public class JokeService : IJokeService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public JokeService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public List<JokeViewModel> GetAll()
        {
            return _context.Jokes.Include(j => j.User).Include(j => j.Comments)
                .Select(joke => new JokeViewModel
                {
                    JokeID = joke.JokeID,
                    JokeName = joke.JokeName,
                    JokeCategory = joke.JokeCategory,
                    JokeText = joke.JokeText,
                    JokeDateAdded = joke.JokeDateAdded,
                    CommentsCount = joke.Comments.Count(),
                    RatingsCount = joke.Ratings.Count(),
                    CreatorEmail = joke.User.Email
                })
                .ToList();
        }

        public async Task CreateAsync(JokeViewModel model)
        {
            string userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            Joke joke = new Joke
            {
                JokeID = Guid.NewGuid().ToString(),
                JokeName = model.JokeName,
                JokeCategory = model.JokeCategory,
                JokeText = model.JokeText,
                JokeDateAdded = model.JokeDateAdded,
                UserID = userId
            };

            await _context.Jokes.AddAsync(joke);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteJoke(string id)
        {
            if (string.IsNullOrEmpty(id) || string.IsNullOrWhiteSpace(id))
            {
                Console.WriteLine("Error!");
                return;
            }

            var jokeDb = _context.Jokes
                .Include(j => j.Comments)
                .Include(j => j.Ratings)
                .FirstOrDefault(x => x.JokeID == id);

            if (jokeDb == null)
            {
                Console.WriteLine("Joke not found!");
                return;
            }

            foreach (var comment in jokeDb.Comments)
            {
                _context.Comments.Remove(comment);
            }

            foreach (var rating in jokeDb.Ratings)
            {
                _context.Ratings.Remove(rating);
            }

            _context.Jokes.Remove(jokeDb);

            await _context.SaveChangesAsync();
        }


        public JokeViewModel GetDetailsById(string id)
        {
            var joke = _context.Jokes
                .Include(j => j.User)
                .Include(j => j.Comments)
                    .ThenInclude(c => c.User)
                .Include(j => j.Ratings)
                .SingleOrDefault(j => j.JokeID == id);

            if (joke == null)
            {
                return null;
            }

            var jokeViewModel = new JokeViewModel
            {
                JokeID = joke.JokeID,
                JokeName = joke.JokeName,
                JokeCategory = joke.JokeCategory,
                JokeText = joke.JokeText,
                JokeDateAdded = joke.JokeDateAdded,
                CreatorEmail = joke.User.Email,
                Comments = joke.Comments.Select(comment => new CommentViewModel
                {
                    CommentID = comment.CommentID,
                    CommentText = comment.CommentText,
                    CommentDateAdded = comment.CommentDateAdded,
                    JokeID = joke.JokeID,
                    CreatorEmail = comment.User.Email
                }).ToList(),
                Ratings = joke.Ratings.Select(rating => new RatingViewModel
                {
                    RatingID = rating.RatingID,
                    RatingValue = rating.RatingValue,
                    RatingDateAdded = rating.RatingDateAdded,
                    JokeID = joke.JokeID,
                    CreatorEmail = rating.User.Email,
                }).ToList()
            };

            return jokeViewModel;
        }

        public JokeViewModel UpdateById(string id)
        {
            JokeViewModel joke = _context.Jokes
                .Select(joke => new JokeViewModel
                {
                    JokeID = joke.JokeID,
                    JokeName = joke.JokeName,
                    JokeCategory = joke.JokeCategory,
                    JokeText = joke.JokeText,
                    JokeDateAdded = joke.JokeDateAdded,
                }).SingleOrDefault(joke => joke.JokeID == id);

            return joke;
        }

        public async Task UpdateAsync(JokeViewModel model)
        {
            Joke joke = _context.Jokes.Find(model.JokeID);

            bool isJokeNull = joke == null;
            if (isJokeNull)
            {
                return;
            }

            joke.JokeName = model.JokeName;
            joke.JokeCategory = model.JokeCategory;
            joke.JokeText = model.JokeText;
            joke.JokeDateAdded = model.JokeDateAdded;

            _context.Jokes.Update(joke);
            await _context.SaveChangesAsync();
        }
    }
}
