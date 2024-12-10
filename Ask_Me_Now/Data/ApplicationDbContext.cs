using Ask_Me_Now.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Ask_Me_Now.Data
{
    public class ApplicationDbContext : IdentityDbContext<Utilizator>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Categorie> Categorii { get; set; }
        public DbSet<Intrebare> Intrebari { get; set; }
        public DbSet<Raspuns> Raspunsuri { get; set; }
    }
}
