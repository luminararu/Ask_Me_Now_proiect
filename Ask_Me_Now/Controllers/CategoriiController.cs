using Ask_Me_Now.Data;
using Ask_Me_Now.Models;    
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ask_Me_Now.Controllers
{
    public class CategoriiController : Controller
    {
            //useri si roluri
            private readonly ApplicationDbContext db;
            private readonly UserManager<Utilizator> _userManager;
            private readonly RoleManager<IdentityRole> _roleManager;
            public CategoriiController(
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

                var categorii = from categorie in db.Categorii
                                 orderby categorie.Denumire
                                 select categorie;
                ViewBag.Categorii = categorii;
                return View();
            }

            
            public ActionResult Show(int id)
            {
                Categorie categorie = db.Categorii.Find(id);
                return View(categorie);
            }

            [Authorize(Roles ="Admin")]
            public ActionResult New()
            {
                return View();
            }

            [HttpPost]
            [Authorize(Roles ="Admin")]
            public ActionResult New(Categorie cat)
            {
                if (string.IsNullOrEmpty(cat.Denumire))
                {
                    ModelState.AddModelError(string.Empty, "Continutul trebuie completat!");
                }
                else
                {   
                db.Categorii.Add(cat);
                    db.SaveChanges();
                    TempData["message"] = "Categoria a fost adaugata cu succes!";
                return RedirectToAction("Index");
                }
                return View(cat);
            }
            
            [Authorize(Roles ="Admin")]
            public ActionResult Edit(int id)
            {
                Categorie categorie = db.Categorii.Find(id);
                return View(categorie);
            }

            [HttpPost]
            [Authorize(Roles ="Admin")]
            public ActionResult Edit(int id, Categorie categorieReq)
            {
                Categorie categorie = db.Categorii.Find(id);
                if (string.IsNullOrEmpty(categorieReq.Denumire))
                {
                    ModelState.AddModelError(string.Empty, "Continutul trebuie completat!");
                }
                else
                {
                    categorie.Denumire = categorieReq.Denumire;
                    db.SaveChanges();
                    TempData["message"] = "Categoria a fost modificata cu succes!";
                    return RedirectToAction("Index");

                }
                return View(categorieReq);
                }

            [HttpPost]
            [Authorize(Roles ="Admin")]
            public ActionResult Delete(int id)
            {

                Categorie categorie= db.Categorii.Include("Intrebari")
                                                    .Include("Intrebari.Raspunsuri")
                                                    .Where(c => c.CategorieId == id)
                                                    .First();

                db.Categorii.Remove(categorie);

                TempData["message"] = "Categoria a fost stearsa cu succes!";
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
}
