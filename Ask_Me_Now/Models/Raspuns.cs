using Ask_Me_Now.Models;
using System.ComponentModel.DataAnnotations;

namespace Ask_Me_Now.Models
{
    public class Raspuns
    {

        [Key]
        public int RaspunsId { get; set; }

        public string? UserId{ get; set; }
        public Utilizator? Utilizator { get; set; }

        public int? IntrebareId { get; set; }
        public Intrebare? Intrebare { get; set; }

        [Required(ErrorMessage = "Un raspuns trebuie sa contina text!")]
        [StringLength(100, ErrorMessage = "Un raspuns nu poate avea mai mult de 100 de caractere!")]
        public string Continut { get; set; }

        public int Likes { get; set; } = 0;
        public int Dislikes { get; set; } = 0;

        public DateTime Data { get; set; }
    }
}