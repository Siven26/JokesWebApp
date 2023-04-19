using System.ComponentModel.DataAnnotations;
using JokesWebApp.Data.DataModels;

namespace JokesWebApp.Services.ViewModels
{
    public class UserViewModel
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
