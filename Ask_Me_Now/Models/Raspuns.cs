using Ask_Me_Now.Models;
using System.ComponentModel.DataAnnotations;

namespace Ask_Me_Now.Models
{
    public class Raspuns
    {

        [Key]
        public int RaspunsId { get; set; }

        [Required(ErrorMessage = "Un raspuns trebuie sa contina un profil asociat!")]
        public int Nume{ get; set; }
        public Utilizator Utilizator { get; set; }

        [Required(ErrorMessage = "Un raspuns trebuie sa contina o intrebare asociata!")]
        public int IntrebareId { get; set; }
        public Intrebare Intrebare { get; set; }

        [Required(ErrorMessage = "Un raspuns trebuie sa contina text!")]
        [StringLength(100, ErrorMessage = "Un raspuns nu poate avea mai mult de 100 de caractere!")]
        public string Continut { get; set; }

        [Required(ErrorMessage = "Un raspuns trebuie sa contina un numar de like-uri!")]
        public int Likes { get; set; }

        [Required(ErrorMessage = "Un raspuns trebuie sa contina un numar de dislike-uri!")]
        public int Dislikes { get; set; }

    }
}