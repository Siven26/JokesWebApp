using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace JokesWebApp.Data.DataModels
{
    public class Comment
    {
        [Key]
        public string CommentID { get; set; }

        [Required]
        public string CommentText { get; set; }

        [DefaultValue("getdate()")]
        public DateTime CommentDateAdded { get; set; } = DateTime.Now;
    }
}
