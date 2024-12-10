using Ask_Me_Now.Data;
using Ask_Me_Now.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ask_Me_Now.Controllers
{
    public class ProfiluriController : Controller
    {
        //useri si roluri
        private readonly ApplicationDbContext db;
        private readonly UserManager<Utilizator> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public ProfiluriController(
        ApplicationDbContext context,
        UserManager<Utilizator> userManager,
        RoleManager<IdentityRole> roleManager
        )
        {
            db = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public ActionResult Index()
        {
            if (TempData.ContainsKey("message"))
            {
                ViewBag.message = TempData["message"].ToString();
            }

            var profiluri = from profil in db.Profiluri
                            orderby profil.Nume
                            select profil;
            ViewBag.Categorii = profiluri;
            return View();
        }
        public ActionResult Show(int id)
        {
            Profil profil = db.Profiluri.Find(id);
            return View(profil);
        }

        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        public ActionResult New(Profil prof)
        {
            if (ModelState.IsValid)
            {
                db.Profiluri.Add(prof);
                db.SaveChanges();
                TempData["message"] = "Profilul a fost inregistrat cu succes!";
                return RedirectToAction("Index");
            }
            else
            {
                return View(prof);
            }
        }

        public ActionResult Edit(int id)
        {
            Profil profil = db.Profiluri.Find(id);
            return View(profil);
        }

        [HttpPost]
        public ActionResult Edit(int id, Profil profilReq)
        {
            Profil profil = db.Profiluri.Find(id);

            if (ModelState.IsValid)
            {
                profil.Nume = profilReq.Nume;
                profil.Email = profilReq.Email;
                profil.Descriere = profilReq.Descriere;
                profil.CategoriePreferata = profilReq.CategoriePreferata;
                db.SaveChanges();
                TempData["message"] = "Profilul a fost modificat cu succes!";
                return RedirectToAction("Index");
            }
            else
            {
                return View(profilReq);
            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {

            Profil profil = db.Profiluri.Include("Intrebari")
                                        .Include("Intrebari.Raspunsuri")
                                        .Where(c => c.ProfilId == id)
                                        .First();

            db.Profiluri.Remove(profil);

            TempData["message"] = "Profilul a fost eliminat cu succes!";
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
