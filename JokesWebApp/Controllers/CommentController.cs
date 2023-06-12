using JokesWebApp.Services.ViewModels;
using JokesWebApp.Services;
using Microsoft.AspNetCore.Mvc;
using JokesWebApp.Data.DataModels;
using System.Security.Claims;
using JokesWebApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

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

        [HttpGet]
        [Authorize]
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
            var comment = commentService.GetCommentDetailsById(id);
            if (User.Identity.IsAuthenticated && User.FindFirstValue(ClaimTypes.Email) == comment.CreatorEmail || User.IsInRole("Admin"))
            {
                CommentViewModel commentToUpdate = commentService.UpdateCommentById(id);
                return View(commentToUpdate);
            }
            return RedirectToAction("WrongUser", "Home");
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
            var comment = commentService.GetCommentDetailsById(id);

            if (comment == null)
            {
                return BadRequest("Invalid comment id");
            }

            if (User.Identity.IsAuthenticated && User.FindFirstValue(ClaimTypes.Email) == comment.CreatorEmail || User.IsInRole("Admin"))
            {
                var commentToDelete = commentService.UpdateCommentById(id);
                return View(commentToDelete);
            }
            return RedirectToAction("WrongUser", "Home");
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
