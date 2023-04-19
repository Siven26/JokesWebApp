using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using JokesWebApp.Data.DataModels;

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
    }
}
