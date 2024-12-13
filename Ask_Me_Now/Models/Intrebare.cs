using Ask_Me_Now.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime;

namespace Ask_Me_Now.Models
{
    public class Intrebare
    {
        [Key]
        public int IntrebareId { get; set; }

        public int? CategorieId { get; set; }
        public Categorie? Categorie { get; set; }

        public string? UserId { get; set; }
        public Utilizator? Utilizator { get; set; }

        public DateTime Data {  get; set; }

        [Required(ErrorMessage = "O intrebare nu poate avea continutul gol!")]
        [MinLength(5, ErrorMessage = "O intrebare trebuie sa contina minim 5 caractere!")]
        [MaxLength(100, ErrorMessage = "O intrebare nu poaate contine mai mult de 100 de caractere!")]
        public string Continut { get; set; }

        public int Likes { get; set; } = 0;
        public int Dislikes { get; set; } = 0;

        public virtual ICollection<Raspuns> Raspunsuri { get; set; }
        public Intrebare()
        {
            Raspunsuri = new List<Raspuns>();
        }

        [NotMapped]
        public IEnumerable<SelectListItem>? Categ { get; set; }

    }
}