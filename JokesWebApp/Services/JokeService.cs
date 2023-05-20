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
            return _context.Jokes.Include(j => j.User)
                .Select(joke => new JokeViewModel()
                {
                    JokeID = joke.JokeID,
                    JokeName = joke.JokeName,
                    JokeCategory = joke.JokeCategory,
                    JokeText = joke.JokeText,
                    JokeDateAdded = joke.JokeDateAdded,
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
            }
            if (id != null)
            {
                var jokeDb = _context.Jokes.FirstOrDefault(x => x.JokeID == id);
                _context.Jokes.Remove(jokeDb);
                await _context.SaveChangesAsync();
            }
        }

        public JokeViewModel GetDetailsById(string id)
        {
            JokeViewModel joke = _context.Jokes
                .Include(j => j.User)
                .Select(joke => new JokeViewModel
                {
                    JokeID = joke.JokeID,
                    JokeName = joke.JokeName,
                    JokeCategory = joke.JokeCategory,
                    JokeText = joke.JokeText,
                    JokeDateAdded = joke.JokeDateAdded,
                    CreatorEmail = joke.User.Email
                }).SingleOrDefault(joke => joke.JokeID == id);

            return joke;
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
