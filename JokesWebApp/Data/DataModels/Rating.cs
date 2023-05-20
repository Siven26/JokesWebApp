using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace JokesWebApp.Data.DataModels
{
    public class Rating
    {
        [Key]
        public string RatingID { get; set; }

        [ForeignKey("Joke")]
        public string JokeID { get; set; }

        [Range(1, 5)]
        public int RatingValue { get; set; }
    }
}
