using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace JokesWebApp.Services.ViewModels
{
    public class JokeViewModel
    {
        [Key]
        public string JokeID { get; set; }

        [Required]
        public string JokeName { get; set; }

        [Required]
        public string JokeCategory { get; set; }

        [Required]
        public string JokeText { get; set; }

        [DefaultValue("getdate()")]
        public DateTime JokeDateAdded { get; set; } = DateTime.Now;
        public int CommentsCount { get; set; }
        public List<CommentViewModel> Comments { get; set; }
        public int RatingsCount { get; set; }
        public List<RatingViewModel> Ratings { get; set; }
        public string CreatorEmail { get; set; }
    }
}
