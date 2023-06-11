using System.ComponentModel.DataAnnotations;

namespace JokesWebApp.Data.DataModels
{
    public class Showman
    {
        [Key]
        public string ShowmanID { get; set; }
        public string ShowmanName { get; set; }
        public string ShowmanImage { get; set; }
        public string ShowmanDescription { get; set; }
    }
}
