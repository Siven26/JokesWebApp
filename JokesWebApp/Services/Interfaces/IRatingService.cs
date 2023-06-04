using JokesWebApp.Services.ViewModels;

namespace JokesWebApp.Services.Interfaces
{
    public interface IRatingService
    {
        List<RatingViewModel> GetAllRatings(string id);
        Task CreateRatingAsync(RatingViewModel model);
        bool HasRatingForJoke(string jokeId, string userId);
    }
}
