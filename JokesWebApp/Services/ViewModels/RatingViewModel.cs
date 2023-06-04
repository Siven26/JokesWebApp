namespace JokesWebApp.Services.ViewModels
{
    public class RatingViewModel
    {
        public string RatingID { get; set; }
        public string JokeID { get; set; }
        public int RatingValue { get; set; }
        public DateTime RatingDateAdded { get; set; } = DateTime.Now;
        public string CreatorEmail { get; set; }
    }
}
