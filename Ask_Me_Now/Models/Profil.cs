using Ask_Me_Now.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ask_Me_Now.Models
{
    public class Profil
    {
        [Key]
        public int ProfilId { get; set; }

        [Required(ErrorMessage = "Numele profilului este obligatoriu!")]
        [MinLength(3, ErrorMessage = "Numele nu poate avea mai putin de 3 caractere!")]
        [MaxLength(25, ErrorMessage = "Numele nu poate avea mai mult de 25 de caractere!")]
        public string Nume { get; set; }

        [Required(ErrorMessage = "Emailul este obligatoriu!")]
        [EmailAddress(ErrorMessage = "Emailul trebuie sa respecte formatul specific emailurilor!")]
        [MinLength(5, ErrorMessage = "Lungimea minima a unui email este de 5 caractere.")]
        [MaxLength(50, ErrorMessage = "Lungimea maxima a unui email este de 50 de caractere.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Profilul trebuie sa contina o descriere!")]
        [MaxLength(200, ErrorMessage = "Profilul nu poate sa contina o descriere mai lunga de 200 de caractere!")]
        public string Descriere { get; set; }

        //aleasa dintr-un dropdown
        [Required(ErrorMessage = "Categoria preferata trebuie aleasa.")]
        public string CategoriePreferata { get; set; }
        public Categorie Categorie { get; set; }

        [NotMapped]
        public IEnumerable<SelectListItem>? Categ { get; set; }

    }
}
