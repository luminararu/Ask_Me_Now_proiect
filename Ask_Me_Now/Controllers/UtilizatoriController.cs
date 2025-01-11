using Ask_Me_Now.Data;
using Ask_Me_Now.Data.Migrations;
using Ask_Me_Now.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Ask_Me_Now.Controllers
{
    public class UtilizatoriController : Controller
    {
        private readonly ApplicationDbContext db;

        private readonly UserManager<Utilizator> _userManager;

        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly SignInManager<Utilizator> _signInManager;
        public UtilizatoriController(
            ApplicationDbContext context,
            UserManager<Utilizator> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<Utilizator> signInManager)
        {
            db = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            var users = from user in db.Users
                        orderby user.UserName
                        select user;

            ViewBag.UsersList = users;

            return View();
        }

        public async Task<ActionResult> Show(string id)
        {
            Utilizator user = db.Users.Find(id);
            var roles = await _userManager.GetRolesAsync(user);

            ViewBag.Roles = roles;

            ViewBag.UserCurent = await _userManager.GetUserAsync(User);

            //activitate recenta
            ViewBag.IntrebariRecente = db.Intrebari
                                     .Where(a => a.UserId == user.Id)
                                     .OrderByDescending(a => a.Data)
                                     .Take(3) 
                                     .ToList();

            ViewBag.RaspunsuriRecente = db.Raspunsuri
                                     .Where(a => a.UserId == user.Id)
                                     .OrderByDescending(a => a.Data)
                                     .Take(3)
                                     .ToList();
            return View(user);
        }

        public async Task<ActionResult> Edit(string id)
        {
            Utilizator user = db.Users.Find(id);

            ViewBag.AllRoles = GetAllRoles();

            var roleNames = await _userManager.GetRolesAsync(user); // Lista de nume de roluri

            // Cautam ID-ul rolului in baza de date
            ViewBag.UserRole = _roleManager.Roles
                                              .Where(r => roleNames.Contains(r.Name))
                                              .Select(r => r.Id)
                                              .First(); // Selectam 1 singur rol

            return View(user);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(string id, Utilizator newData, [FromForm] string newRole, [FromForm] IFormFile PozaProfil)
        {
            Utilizator user = db.Users.Find(id);

            user.AllRoles = GetAllRoles();


            if (ModelState.IsValid)
            {
                user.Nume = newData.Nume;
                user.Prenume = newData.Prenume;
                user.Email = newData.Email;
                user.PhoneNumber = newData.PhoneNumber;
                user.Descriere = newData.Descriere;
                if (PozaProfil != null && PozaProfil.Length > 0)
                {
                    // nume fisier
                    var fileName = Path.GetFileName(PozaProfil.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", fileName);

                    // salvare fisier pe server
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await PozaProfil.CopyToAsync(stream);
                    }

                    // Salvează calea fișierului în baza de date
                    user.PozaProfil = "/images/" + fileName;
                }
                // Cautam toate rolurile din baza de date
                var roles = db.Roles.ToList();

                foreach (var role in roles)
                {
                    // Scoatem userul din rolurile anterioare
                    await _userManager.RemoveFromRoleAsync(user, role.Name);
                }
                // Adaugam noul rol selectat
                var roleName = await _roleManager.FindByIdAsync(newRole);
                await _userManager.AddToRoleAsync(user, roleName.ToString());

                db.SaveChanges();

            }
            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            var user = db.Users
                         .Include("Intrebari")
                         .Include("Raspunsuri")
                         .Where(u => u.Id == id)
                         .First();

            //Stergerea intrebarilor asociate userului
            if (user.Intrebari.Count > 0)
            {
                foreach (var comment in user.Intrebari)
                {
                    db.Intrebari.Remove(comment);
                }
            }

            //Stergerea raspunsurilor asociate userului
            if (user.Raspunsuri.Count > 0)
            {
                foreach (var raspuns in user.Raspunsuri)
                {
                    db.Raspunsuri.Remove(raspuns);
                }
            }

            //Stergerea utilizatorului din baza de date
            db.Utilizatori.Remove(user);

            db.SaveChanges();

            if(User.IsInRole("Admin"))
            {
                return RedirectToAction("Index");
            }
            else
            {
                await _signInManager.SignOutAsync();
                return RedirectToPage("/Identity/Account/Register");

            }
            
        }


        [NonAction]
        public IEnumerable<SelectListItem> GetAllRoles()
        {
            var selectList = new List<SelectListItem>();

            var roles = from role in db.Roles
                        select role;

            foreach (var role in roles)
            {
                selectList.Add(new SelectListItem
                {
                    Value = role.Id.ToString(),
                    Text = role.Name.ToString()
                });
            }
            return selectList;
        }
    }
}
