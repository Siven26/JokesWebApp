using System.ComponentModel.DataAnnotations;

namespace JokesWebApp.Data.DataModels
{
    public class JokeAPI
    {
        [Key]
        public string JokeApiID { get; set; }
        public string Type { get; set; }
        public string Category { get; set; }
        public string Joke { get; set; }
        public string Setup { get; set; }
        public string Delivery { get; set; }
    }
}
