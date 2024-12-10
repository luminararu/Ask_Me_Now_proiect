using Ask_Me_Now.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Ask_Me_Now.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
            serviceProvider.GetRequiredService
            <DbContextOptions<ApplicationDbContext>>()))
            {
                // Verificam daca in baza de date exista cel putin un rol
                if (context.Roles.Any())
                {
                    return; // baza de date contine deja roluri
                }
                // CREAREA ROLURILOR IN BD
                // daca nu contine roluri, acestea se vor crea
                context.Roles.AddRange(
                new IdentityRole
                {
                    Id = "2c5e174e-3b0e-446f-86af-483d56fd7210",
                    Name = "Admin",
                    NormalizedName = "Admin".ToUpper()
                },
                new IdentityRole
                {
                    Id = "2c5e174e-3b0e-446f-86af-483d56fd7212",
                    Name = "User",
                    NormalizedName = "User".ToUpper()
                });
                context.SaveChanges();

                var hasher = new PasswordHasher<Utilizator>();
                var categorie1 = context.Categorii.FirstOrDefault(c => c.Denumire == "Sanatate");
                var categorie2 = context.Categorii.FirstOrDefault(c => c.Denumire == "Vacante");
                
                context.SaveChanges();
                // CREAREA USERILOR IN BD
                // Se creeaza cate un user pentru fiecare rol
                context.Users.AddRange(
                new Utilizator
                {
                    Id = "8e445865-a24d-4543-a6c6-9443d048cdb0",
                    UserName = "Roxana Popescu",
                    EmailConfirmed = true,
                    NormalizedEmail = "ROXANA_A@GMAIL.COM",
                    Email = "roxana_a@gmail.com",
                    NormalizedUserName = "ROXANA POPESCU",
                    PasswordHash = hasher.HashPassword(null, "Admin1!")
                },
                new Utilizator
                {
                    Id = "8e445865-a24d-4543-a6c6-9443d048cdb2",
                    UserName = "Corina Ionescu",
                    EmailConfirmed = true,
                    NormalizedEmail = "CORINA_U@GMAIL.COM",
                    Email = "corina_u@gmail.com",
                    NormalizedUserName = "CORINA IONESCU",
                    PasswordHash = hasher.HashPassword(null, "User1!")
                });
                context.SaveChanges();

                // ASOCIEREA USER-ROLE
                context.UserRoles.AddRange(
                new IdentityUserRole<string>
                {
                    RoleId = "2c5e174e-3b0e-446f-86af-483d56fd7210",
                    UserId = "8e445865-a24d-4543-a6c6-9443d048cdb0"
                },
                new IdentityUserRole<string>
                {
                    RoleId = "2c5e174e-3b0e-446f-86af-483d56fd7212",
                    UserId = "8e445865-a24d-4543-a6c6-9443d048cdb2"
                });
                context.SaveChanges();
            }
        }
    }
}
