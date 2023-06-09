using JokesWebApp.Services.ViewModels;

namespace JokesWebApp.Services.Interfaces
{
    public interface IComedianService
    {
        List<ComedianViewModel> GetAllComedians();
        Task CreateComedianAsync(ComedianViewModel model);
        Task DeleteComedian(string id);
        ComedianViewModel GetComedianDetailsById(string id);
        ComedianViewModel UpdateComedianById(string id);
        void UpdateComedian(ComedianViewModel model);
        Task SetImage(ComedianViewModel comedian, IFormFile file);
    }
}
