using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace JokesWebApp.Data.DataModels
{
    public class Rating
    {
        [Key]
        public string RatingID { get; set; }
        public string JokeID { get; set; }
        [Range(1, 5)]
        public int RatingValue { get; set; }
        public DateTime RatingDateAdded { get; set; } = DateTime.Now;
        public string UserID { get; set; }

        [ForeignKey("JokeID")]
        public Joke Joke { get; set; }

        [ForeignKey("UserID")]
        public IdentityUser User { get; set; }
    }
}
