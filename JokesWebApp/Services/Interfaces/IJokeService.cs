using JokesWebApp.Services.ViewModels;

namespace JokesWebApp.Services.Interfaces
{
    public interface IJokeService
    {
        Task CreateAsync(JokeViewModel model);
        JokeViewModel UpdateById(string id);
        Task UpdateAsync(JokeViewModel model);
    }
}