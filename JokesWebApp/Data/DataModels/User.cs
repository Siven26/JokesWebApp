using System.ComponentModel.DataAnnotations;

namespace JokesWebApp.Data.DataModels
{
    public class User
    {
        [Key]
        public string UserID { get; set; }

        [Required]
        [StringLength(50)]
        public string UserName { get; set; }

        [Required]
        public string UserEmail { get; set; }

        [Required]
        public string UserPassword { get; set; }
    }
}
