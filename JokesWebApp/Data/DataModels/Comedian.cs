using System.ComponentModel.DataAnnotations;

namespace JokesWebApp.Data.DataModels
{
    public class Comedian
    {
        [Key]
        public string ComedianID { get; set; }
        public string ComedianName { get; set; }
        public string ComedianImage { get; set; }
        public string ComedianDescription { get; set; }
    }
}
