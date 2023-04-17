using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JokesWebApp.Data.DataModels
{
    public class UserJokeComment
    {
        [Key]
        public string UserJokeCommentID { get; set; }

        [ForeignKey("Comment")]
        public string CommentID { get; set; }

        [ForeignKey("User")]
        public string UserID { get; set; }

        [ForeignKey("Joke")]
        public string JokeID { get; set; }
    }
}
