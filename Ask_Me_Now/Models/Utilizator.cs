using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Ask_Me_Now.Models
{
    public class Utilizator : IdentityUser
    {
        //PROFIL

       
        [MinLength(1, ErrorMessage = "Numele nu poate avea mai putin de un caracter!")]
        [MaxLength(25, ErrorMessage = "Numele nu poate avea mai mult de 25 de caractere!")]
        public string? Nume { get; set; }

        [MinLength(1, ErrorMessage = "Prenumele nu poate avea mai putin de un caracter!")]
        [MaxLength(25, ErrorMessage = "Prenumele nu poate avea mai mult de 25 de caractere!")]
        public string? Prenume { get; set; }

        //[Required(ErrorMessage = "Emailul este obligatoriu!")]
        //[EmailAddress(ErrorMessage = "Emailul trebuie sa respecte formatul specific emailurilor!")]
        //[MinLength(5, ErrorMessage = "Lungimea minima a unui email este de 5 caractere.")]
        //[MaxLength(50, ErrorMessage = "Lungimea maxima a unui email este de 50 de caractere.")]
        //public string Email { get; set; }

        [MaxLength(200, ErrorMessage = "Profilul nu poate sa contina o descriere mai lunga de 200 de caractere!")]
        public string? Descriere { get; set; }

        [NotMapped]
        public IEnumerable<SelectListItem>? Categ { get; set; }

        // un user poate posta mai multe intrebari
        public virtual ICollection<Intrebare>? Intrebari { get; set; }

        // un user poate posta mai multe raspunsuri
        public virtual ICollection<Raspuns>? Raspunsuri { get; set; }

        // variabila in care vom retine rolurile existente in baza de date
        // pentru popularea unui dropdown list
        [NotMapped]
        public IEnumerable<SelectListItem>? AllRoles { get; set; }

        public string? PozaProfil { get; set; }

    }
}
