using JokesWebApp.Data;
using JokesWebApp.Interface;
using System.ComponentModel.DataAnnotations;

namespace JokesWebApp.Models
{
    public class Joke 
    {

        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string JokeQuestion { get; set; }

        [Required]
        public string JokeAnswer { get; set; }

        public string? Description { get; set; }

        public DateTime? CreatedDateTime { get; set; } = DateTime.Now;


    }
}
