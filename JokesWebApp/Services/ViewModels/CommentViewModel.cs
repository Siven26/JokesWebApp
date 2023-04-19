using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using JokesWebApp.Data.DataModels;

namespace JokesWebApp.Services.ViewModels
{
    public class CommentViewModel
    {
        [Key]
        public string CommentID { get; set; }

        [Required]
        public string CommentText { get; set; }

        [DefaultValue("getdate()")]
        public DateTime CommentDateAdded { get; set; } = DateTime.Now;
    }
}
