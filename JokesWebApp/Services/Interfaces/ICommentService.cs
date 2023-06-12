using JokesWebApp.Services.ViewModels;

namespace JokesWebApp.Services.Interfaces
{
    public interface ICommentService
    {
        List<CommentViewModel> GetAllComments(string id);
        Task CreateCommentAsync(CommentViewModel model);
        Task DeleteComment(string id);
        CommentViewModel GetCommentDetailsById(string id);
        CommentViewModel UpdateCommentById(string id);
        Task UpdateCommentAsync(CommentViewModel model);
    }
}
