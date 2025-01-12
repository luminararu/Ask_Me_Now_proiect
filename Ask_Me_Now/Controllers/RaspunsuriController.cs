using Ask_Me_Now.Data;
using Ask_Me_Now.Models;
using Ganss.Xss;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ArticlesApp.Controllers
{
    public class RaspunsuriController : Controller
    {
        // PASUL 10: useri si roluri 

        private readonly ApplicationDbContext db;
        private readonly UserManager<Utilizator> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public RaspunsuriController(
        ApplicationDbContext context,
        UserManager<Utilizator> userManager,
        RoleManager<IdentityRole> roleManager
        )
        {
            db = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        

        [HttpPost]
        [Authorize(Roles = "User,Admin")]
        public IActionResult Delete(int id)
        {
            Raspuns raspuns = db.Raspunsuri.Find(id);

            if (raspuns.UserId == _userManager.GetUserId(User) || User.IsInRole("Admin"))
            {
                db. Raspunsuri.Remove(raspuns);
                db.SaveChanges();
                return Redirect("/Intrebari/Show/" + raspuns.IntrebareId);
            }
            else
            {
                TempData["message"] = "Nu aveti dreptul sa stergeti raspunsul";
                TempData["messageType"] = "alert-danger";
                return RedirectToAction("Index", "Intrebari");
            }
        }


        [Authorize(Roles = "User,Admin")]
        public IActionResult Edit(int id)
        {
            Raspuns raspuns = db.Raspunsuri.Find(id);

            if (raspuns.UserId == _userManager.GetUserId(User) || User.IsInRole("Admin"))
            {
                return View(raspuns);
            }
            else
            {
                TempData["message"] = "Nu aveti dreptul sa editati raspunsul!";
                TempData["messageType"] = "alert-danger";
                return RedirectToAction("Index", "Intrebari");
            }
        }

        [HttpPost]
        [Authorize(Roles = "User,Admin")]
        public IActionResult New(Raspuns raspuns)
        {
            var sanitizer = new HtmlSanitizer();

            raspuns.Data = DateTime.Now;

            // preluam Id-ul utilizatorului care posteaza articolul
            raspuns.UserId = _userManager.GetUserId(User);

            if (ModelState.IsValid)
            {
                raspuns.Continut = sanitizer.Sanitize(raspuns.Continut);

                db.Raspunsuri.Add(raspuns);
                db.SaveChanges();
                TempData["message"] = "Raspunsul a fost adaugata cu succes!";
                TempData["messageType"] = "alert-success";
                return RedirectToAction("Edit");
            }
            else
            {
                return View(raspuns);
            }
         }


        [HttpPost]
        [Authorize(Roles = "User,Admin")]
        public IActionResult Edit(int id, Raspuns requestComment)
        {
            Raspuns raspuns = db.Raspunsuri.Find(id);

            if (raspuns.UserId == _userManager.GetUserId(User) || User.IsInRole("Admin"))
            {
                if (ModelState.IsValid)
                {
                    raspuns.Continut = requestComment.Continut;

                    db.SaveChanges();

                    return Redirect("/Intrebari/Show/" + raspuns.IntrebareId);
                }
                else
                {
                    return View(requestComment);
                }
            }
            else
            {
                TempData["message"] = "Nu aveti dreptul sa editati raspunsul!";
                TempData["messageType"] = "alert-danger";
                return RedirectToAction("Index", "Intrebari");
            }
        }

        [HttpPost]
        [Authorize(Roles = "User,Admin")]
        public IActionResult Like(int id)
        {
            var raspuns = db.Raspunsuri.Find(id);
            if (raspuns == null)
            {
                TempData["message"] = "Raspunsul nu a fost gasit.";
                TempData["alert"] = "alert-danger";
                return RedirectToAction("Index", "Intrebari");
            }

            var userId = _userManager.GetUserId(User);

            var existingInteraction = db.UtilizatorInteractiune
                .FirstOrDefault(ui => ui.UserId == userId && ui.RaspunsId == id);

            if (existingInteraction != null)
            {
                // Actualizare interactiune anterioara (daca exista)
                if (existingInteraction.Liked)
                {
                    TempData["message"] = "Ai dat deja like acestui raspuns.";
                    TempData["alert"] = "alert-warning";
                    return RedirectToAction("Show", "Intrebari", new { id = raspuns.IntrebareId });
                }
                else
                {
                    raspuns.Dislikes--;
                    raspuns.Likes++;
                    existingInteraction.Liked = true;
                }
            }
            else
            {
                //Adaugare interactiune daca nu exista deja
                db.UtilizatorInteractiune.Add(new UtilizatorInteractiune
                {
                    UserId = userId,
                    RaspunsId = id,
                    Liked = true
                });
                raspuns.Likes++;
            }

            db.SaveChanges();

            TempData["message"] = "Ai dat like!";
            TempData["alert"] = "alert-success";
            return RedirectToAction("Show", "Intrebari", new { id = raspuns.IntrebareId });
        }

        [HttpPost]
        [Authorize(Roles = "User,Admin")]
        public IActionResult Dislike(int id)
        {
            var raspuns = db.Raspunsuri.Find(id);
            if (raspuns == null)
            {
                TempData["message"] = "Raspunsul nu a fost gasit.";
                TempData["alert"] = "alert-danger";
                return RedirectToAction("Index", "Intrebari");
            }

            var userId = _userManager.GetUserId(User);

            var existingInteraction = db.UtilizatorInteractiune
                .FirstOrDefault(ui => ui.UserId == userId && ui.RaspunsId == id);

            if (existingInteraction != null)
            {
                if (!existingInteraction.Liked)
                {
                    TempData["message"] = "Ai dat deja dislike acestui raspuns.";
                    TempData["alert"] = "alert-warning";
                    return RedirectToAction("Show", "Intrebari", new { id = raspuns.IntrebareId });
                }
                else
                {
                    raspuns.Likes--;
                    raspuns.Dislikes++;
                    existingInteraction.Liked = false;
                }
            }
            else
            {
                db.UtilizatorInteractiune.Add(new UtilizatorInteractiune
                {
                    UserId = userId,
                    RaspunsId = id,
                    Liked = false
                });
                raspuns.Dislikes++;
            }

            db.SaveChanges();

            TempData["message"] = "Ai dat dislike!";
            TempData["alert"] = "alert-success";
            return RedirectToAction("Show", "Intrebari", new { id = raspuns.IntrebareId });
        }


    }
}