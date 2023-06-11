using JokesWebApp.Services.ViewModels;

namespace JokesWebApp.Services.Interfaces
{
    public interface IJokeService
    {
        List<JokeViewModel> GetAll();
        Task CreateAsync(JokeViewModel model);
        Task DeleteJoke(string id);
        JokeViewModel GetDetailsById(string id);
        JokeViewModel UpdateById(string id);
        Task UpdateAsync(JokeViewModel model);
    }
}