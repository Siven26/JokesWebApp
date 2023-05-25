using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace JokesWebApp.Data.DataModels
{
    public class Comment
    {
        public string CommentID { get; set; }
        public string CommentText { get; set; }
        public string JokeID { get; set; }
        public DateTime CommentDateAdded { get; set; } = DateTime.Now;
        public string UserID { get; set; }

        [ForeignKey("JokeID")]
        public Joke Joke { get; set; }

        [ForeignKey("UserID")]
        public IdentityUser User { get; set; }
    }
}
