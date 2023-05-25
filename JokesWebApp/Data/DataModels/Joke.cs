using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace JokesWebApp.Data.DataModels
{
    public class Joke
    {
        [Key]
        public string JokeID { get; set; }

        [Required]
        public string JokeName { get; set;}

        [Required]
        public string JokeCategory { get; set;}

        [Required]
        public string JokeText { get; set;}

        [DefaultValue("getdate()")]
        public DateTime JokeDateAdded { get; set; }

        public string UserID { get; set; }
        public IdentityUser User { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
