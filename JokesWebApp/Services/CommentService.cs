using JokesWebApp.Data.DataModels;
using JokesWebApp.Data;
using JokesWebApp.Services.ViewModels;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using JokesWebApp.Services.Interfaces;

namespace JokesWebApp.Services
{
    public class CommentService : ICommentService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CommentService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public List<CommentViewModel> GetAllComments(string id)
        {
            return _context.Comments.Where(c => c.JokeID == id).Include(c => c.User).Include(c => c.Joke)
                .Select(comment => new CommentViewModel
                {
                    CommentID = comment.CommentID,
                    CommentText = comment.CommentText,
                    CommentDateAdded = comment.CommentDateAdded,
                    JokeID = comment.JokeID,
                    CreatorEmail = comment.User.Email,
                })
                .ToList();
        }

        public async Task CreateCommentAsync(CommentViewModel model)
        {
            string userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            Comment comment = new Comment
            {
                CommentID = Guid.NewGuid().ToString(),
                CommentText = model.CommentText,
                CommentDateAdded = DateTime.Now,
                JokeID = model.JokeID,
                UserID = userId
            };

            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteComment(string id)
        {
            if (string.IsNullOrEmpty(id) || string.IsNullOrWhiteSpace(id))
            {
                Console.WriteLine("Error!");
            }
            if (id != null)
            {
                var commentDb = _context.Comments.FirstOrDefault(x => x.CommentID == id);

                _context.Comments.Remove(commentDb);
                await _context.SaveChangesAsync();
            }
        }

        public CommentViewModel UpdateCommentById(string id)
        {
            Comment comment = _context.Comments.Include(c => c.User)
                                               .SingleOrDefault(c => c.CommentID == id);

            if (comment == null)
            {
                return null;
            }

            CommentViewModel commentViewModel = new CommentViewModel
            {
                CommentID = comment.CommentID,
                CommentText = comment.CommentText,
                CommentDateAdded = comment.CommentDateAdded,
                JokeID = comment.JokeID,
                CreatorEmail = comment.User.Email
            };

            return commentViewModel;
        }

        public async Task UpdateCommentAsync(CommentViewModel model)
        {
            Comment comment = _context.Comments.Find(model.CommentID);

            bool isCommentNull = comment == null;
            if (isCommentNull)
            {
                return;
            }

            comment.CommentText = model.CommentText;
            comment.CommentDateAdded = model.CommentDateAdded;

            _context.Comments.Update(comment);
            await _context.SaveChangesAsync();
        }
    }
}
