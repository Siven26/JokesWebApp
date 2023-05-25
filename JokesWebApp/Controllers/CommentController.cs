using JokesWebApp.Services.ViewModels;
using JokesWebApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace JokesWebApp.Controllers
{
    public class CommentController : Controller
    {
        public CommentService commentService { get; set; }

        public CommentController(CommentService service)
        {
            commentService = service;
        }

        [HttpGet]
        public IActionResult Comments(string id)
        {
            var comments = commentService.GetAllComments(id);
            return View(comments);
        }

        public IActionResult AddComment(string jokeId)
        {
            var model = new CommentViewModel { JokeID = jokeId };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(CommentViewModel model)
        {
            await commentService.CreateCommentAsync(model);

            return RedirectToAction("Comments", new { id = model.JokeID });
        }

        [HttpGet]
        public IActionResult Update(string id)
        {
            var model = commentService.UpdateCommentById(id);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateComment(CommentViewModel model)
        {
            if (ModelState.IsValid == true)
            {
                return this.View(model);
            }

            await commentService.UpdateCommentAsync(model);
            return RedirectToAction("Comments", new { id = model.JokeID });
        }

        [HttpGet]
        public IActionResult DeleteComment(string id)
        {
            var model = commentService.UpdateCommentById(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCommentConfirmed(string id)
        {
            var comment = commentService.UpdateCommentById(id);
            if (comment == null)
            {
                return NotFound();
            }
            string jokeId = comment.JokeID;

            await commentService.DeleteComment(id);
            return RedirectToAction("Comments", new { id = jokeId });
        }
    }
}
