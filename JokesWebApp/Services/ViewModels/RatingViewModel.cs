using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using JokesWebApp.Data.DataModels;

namespace JokesWebApp.Services.ViewModels
{
    public class RatingViewModel
    {
        [Key]
        public string RatingID { get; set; }

        [ForeignKey("User")]
        public string UserID { get; set; }

        [ForeignKey("Joke")]
        public string JokeID { get; set; }

        [Range(1, 5)]
        public int RatingValue { get; set; }
    }
}
