using Ask_Me_Now.Models;
using System.ComponentModel.DataAnnotations;

namespace Ask_Me_Now.Models
{
    public class Categorie
    {
        [Key]
        public int CategorieId { get; set; }

        [Required(ErrorMessage = "Numele categoriei este obligatoriu!")]
        [StringLength(50, ErrorMessage = "Numele nu poate contine mai mult de 50 de caractere!")]
        public string Denumire { get; set; }

        [Required(ErrorMessage = "Categoria trebuie sa contina neaparat un numar de intrebari!")]
        public int NumarIntrebari { get; set; }

        public virtual ICollection<Intrebare> Intrebari { get; set; }

    }
}