﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace JokesWebApp.Data.DataModels
{
    public class Joke
    {
        [Key]
        public string JokeID { get; set; }

        [Required]
        public string JokeName { get; set;}

        [Required]
        public string JokeCategory { get; set;}

        [Required]
        public string JokeText { get; set;}

        [DefaultValue("getdate()")]
        public DateTime JokeDateAdded { get; set; }
    }
}