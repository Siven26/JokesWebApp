using System.ComponentModel.DataAnnotations;

namespace JokesWebApp.Services.ViewModels
{
    public class ComedianViewModel
    {
        [Key]
        public string ComedianID { get; set; }
        public string ComedianName { get; set; }
        public string ComedianImage { get; set; }
        public string ComedianDescription { get; set; }
    }
}
