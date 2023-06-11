using JokesWebApp.Services.ViewModels;

namespace JokesWebApp.Services.Interfaces
{
    public interface IShowmanService
    {
        List<ShowmanViewModel> GetAllShowmans();
        Task CreateShowmanAsync(ShowmanViewModel model);
        Task DeleteShowman(string id);
        ShowmanViewModel GetShowmanDetailsById(string id);
        ShowmanViewModel UpdateShowmanById(string id);
        void UpdateShowman(ShowmanViewModel model);
        Task SetImage(ShowmanViewModel showman, IFormFile file);
    }
}
