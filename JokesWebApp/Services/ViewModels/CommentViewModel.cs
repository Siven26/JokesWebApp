namespace JokesWebApp.Services.ViewModels
{
    public class CommentViewModel
    {
        public string CommentID { get; set; }
        public string CommentText { get; set; }
        public DateTime CommentDateAdded { get; set; } = DateTime.Now;
        public string JokeID { get; set; }
        public string CreatorEmail { get; set; }
    }
}
