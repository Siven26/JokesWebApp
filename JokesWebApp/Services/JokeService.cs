using JokesWebApp.Data;
using JokesWebApp.Data.DataModels;
using JokesWebApp.Services.ViewModels;
using JokesWebApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JokesWebApp.Services
{
    public class JokeService : IJokeService
    {
        private readonly ApplicationDbContext context;
        public JokeService(ApplicationDbContext post)
        {
            context = post;
        }

        public List<JokeViewModel> GetAll()
        {
            return context.Jokes.Select(joke => new JokeViewModel()
            {
                JokeID = joke.JokeID,
                JokeName = joke.JokeName,
                JokeCategory = joke.JokeCategory,
                JokeText = joke.JokeText,
                JokeDateAdded = joke.JokeDateAdded,
            }).ToList();
        }

        public async Task CreateAsync(JokeViewModel model)
        {
            Joke joke = new Joke();

            joke.JokeID = Guid.NewGuid().ToString();
            joke.JokeName = model.JokeName;
            joke.JokeCategory = model.JokeCategory;
            joke.JokeText = model.JokeText;
            joke.JokeDateAdded = model.JokeDateAdded;

            await context.Jokes.AddAsync(joke);
            await context.SaveChangesAsync();
        }

        public JokeViewModel GetDetailsById(string id)
        {
            JokeViewModel joke = context.Jokes
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

        public JokeViewModel UpdateById(string id)
        {
            JokeViewModel joke = context.Jokes
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
            Joke joke = context.Jokes.Find(model.JokeID);

            bool isJokeNull = joke == null;
            if (isJokeNull)
            {
                return;
            }

            joke.JokeName = model.JokeName;
            joke.JokeCategory = model.JokeCategory;
            joke.JokeText = model.JokeText;
            joke.JokeDateAdded = model.JokeDateAdded;

            context.Jokes.Update(joke);
            await context.SaveChangesAsync();
        }
    }
}
