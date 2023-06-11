using System.ComponentModel.DataAnnotations;

namespace JokesWebApp.Services.ViewModels
{
    public class ShowmanViewModel
    {
        [Key]
        public string ShowmanID { get; set; }
        public string ShowmanName { get; set; }
        public string ShowmanImage { get; set; }
        public string ShowmanDescription { get; set; }
    }
}
