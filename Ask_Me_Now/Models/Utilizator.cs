using Microsoft.AspNetCore.Identity;

namespace Ask_Me_Now.Models
{
    public class Utilizator : IdentityUser
    {
        public int ProfilId { get; set; }

        public virtual Profil Profil { get; set; }

        // un user poate posta mai multe intrebari
        public virtual ICollection<Intrebare>? Intrebari { get; set; }

        // un user poate posta mai multe raspunsuri
        public virtual ICollection<Raspuns>? Raspunsuri { get; set; }

    }
}
