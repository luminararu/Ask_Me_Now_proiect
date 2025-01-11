using Ask_Me_Now.Data;
using Ask_Me_Now.Data.Migrations;
using Ask_Me_Now.Models;
using Ganss.Xss;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Ask_Me_Now.Controllers
{
    public class IntrebariController : Controller
    {
        // PASUL 10: useri si roluri 

        private readonly ApplicationDbContext db;
        private readonly UserManager<Utilizator> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public IntrebariController(
        ApplicationDbContext context,
        UserManager<Utilizator> userManager,
        RoleManager<IdentityRole> roleManager
        )
        {
            db = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Index(string sortOrder = "date")
        {
            var intrebari = db.Intrebari.Include("Categorie")
                                        .Include("Utilizator")
                                        .OrderByDescending(a => a.Data);

            if (TempData.ContainsKey("message"))
            {
                ViewBag.Message = TempData["message"];
                ViewBag.Alert = TempData["messageType"];
            }


            var search = "";

            if (Convert.ToString(HttpContext.Request.Query["search"]) != null)
            {
                search = Convert.ToString(HttpContext.Request.Query["search"]).Trim(); // eliminam spatiile libere 

                // Cautare in articol (Title si Content)

                List<int> IntrebariIds = db.Intrebari.Where
                                        (
                                         at => at.Continut.Contains(search)
                                        ).Select(a => a.IntrebareId).ToList();

                // Cautare in comentarii (Content)
                List<int> articleIdsOfCommentsWithSearchString = db.Raspunsuri
                                        .Where
                                        (
                                         c => c.Continut.Contains(search)
                                        ).Select(c => (int)c.IntrebareId).ToList();

                // Se formeaza o singura lista formata din toate id-urile selectate anterior
                List<int> mergedIds = IntrebariIds.Union(articleIdsOfCommentsWithSearchString).ToList();


                // Lista articolelor care contin cuvantul cautat
                // fie in articol -> Title si Content
                // fie in comentarii -> Content

               
                intrebari = db.Intrebari.Where(intrebare => mergedIds.Contains(intrebare.IntrebareId))
                                .Include("Categorie")
                                .Include("Utilizator")
                                .OrderByDescending(a => a.Data);
                    

                }





            ViewBag.SearchString = search;

            // Sortarea răspunsurilor în funcție de criteriul specificat
            switch (sortOrder.ToLower())
            {
                case "popularity":
                    intrebari = intrebari
                        .OrderByDescending(intrebare => intrebare.Raspunsuri.Count());
                    break;
                case "date":
                default:
                    intrebari = intrebari
                        .OrderByDescending(intrebare => intrebare.Data);
                    break;
            }


            // Afisam 3 articole pe pagina
            int _perPage = 3;
            int totalItems = intrebari.Count();

            var currentPageString = HttpContext.Request.Query["page"].FirstOrDefault();
            int currentPage = 1; // default
            if (!string.IsNullOrEmpty(currentPageString) && int.TryParse(currentPageString, out int result))
            {
                currentPage = result;
            }
            var offset = 0;
            // Se calculeaza offsetul in functie de numarul paginii la care suntem
            if (!currentPage.Equals(0))
            {
                offset = (currentPage - 1) * _perPage;
            }
            // Se preiau articolele corespunzatoare pentru fiecare pagina la care ne aflam 
            // in functie de offset
            var intrebariPaginate = intrebari.Skip(offset).Take(_perPage);

            ViewBag.lastPage = Math.Ceiling((float)totalItems / (float)_perPage);
            ViewBag.Intrebari = intrebariPaginate;

            
            var baseUrl = $"/Intrebari/Index?";
            if (!string.IsNullOrEmpty(search))
            {
                baseUrl += $"search={search}&";
            }
            if (!string.IsNullOrEmpty(sortOrder))
            {
                baseUrl += $"sortOrder={sortOrder}&";
            }
            baseUrl += "page=";

            ViewBag.PaginationBaseUrl = baseUrl;
            ViewBag.CurrentPage = currentPage;

            return View();

        }

        public IActionResult Show(int id, string sortOrder = "date")
        {
            /*Intrebare intrebari = db.Intrebari.Include("Categorie")
                                         .Include("Raspunsuri")
                                         .Include("Utilizator")
                              .Where(art => art.IntrebareId == id)
                              .First();
            SetAccessRights();

            if (TempData.ContainsKey("message"))
            {
                ViewBag.Message = TempData["message"];
                ViewBag.Alert = TempData["messageType"];
            }
            return View(intrebari);*/

            // Preluam intrebarea cu raspunsurile asociate
            // Setam drepturile de acces
            Intrebare intrebare = db.Intrebari
                                    .Include("Categorie")
                                    .Include("Utilizator")
                                    .Include(r => r.Raspunsuri)
                                    .Where(art => art.IntrebareId == id)
                                    .FirstOrDefault();

            if (intrebare == null)
            {
                return NotFound();
            }

            // Sortarea rsspunsurilor in functie de criteriul specificat
            switch (sortOrder.ToLower())
            {
                case "popularity":
                    intrebare.Raspunsuri = intrebare.Raspunsuri
                        .OrderByDescending(r => r.Likes - r.Dislikes)
                        .ToList();
                    break;
                case "date":
                default:
                    intrebare.Raspunsuri = intrebare.Raspunsuri
                        .OrderByDescending(r => r.Data)
                        .ToList();
                    break;
            }

            // Transmitem criteriul de sortare către view pentru a fi utilizat
            ViewBag.SortOrder = sortOrder;

            // Setam drepturile de acces
            SetAccessRights();

            if (TempData.ContainsKey("message"))
            {
                ViewBag.Message = TempData["message"];
                ViewBag.Alert = TempData["messageType"];
            }

            return View(intrebare);


        }

        [HttpPost]
        [Authorize(Roles = "User,Admin")]
        public IActionResult Show([FromForm] Raspuns raspuns)
        {
            raspuns.Data = DateTime.Now;

            // preluam Id-ul utilizatorului care posteaza comentariul
            raspuns.UserId = _userManager.GetUserId(User);

            if (ModelState.IsValid)
            {
                db.Raspunsuri.Add(raspuns);
                db.SaveChanges();
                return Redirect("/Intrebari/Show/" + raspuns.IntrebareId);
            }
            else
            {
                Intrebare art = db.Intrebari.Include("Categorie")
                                         .Include("Utilizator")
                                         .Include("Raspunsuri")
                                         .Where(art => art.IntrebareId == raspuns.IntrebareId)
                                         .First();
                SetAccessRights();
                return View(art);
            }
        }

        [Authorize(Roles = "User,Admin")]
        public IActionResult New()
        {
            Intrebare intrebare = new Intrebare();

            intrebare.Categ = GetAllCategories();

            return View(intrebare);
        }

        [HttpPost]
        [Authorize(Roles = "User,Admin")]
        public IActionResult New(Intrebare intrebare)
        {
            var sanitizer = new HtmlSanitizer();

            intrebare.Data = DateTime.Now;

            // preluam Id-ul utilizatorului care posteaza articolul
            intrebare.UserId = _userManager.GetUserId(User);

            if (ModelState.IsValid)
            {
                intrebare.Continut = sanitizer.Sanitize(intrebare.Continut);

                db.Intrebari.Add(intrebare);
                db.SaveChanges();
                TempData["message"] = "Intrebarea a fost adaugata cu succes!";
                TempData["messageType"] = "alert-success";
                return RedirectToAction("Index");
            }
            else
            {
                intrebare.Categ = GetAllCategories();
                return View(intrebare);
            }
        }

        [Authorize(Roles = "User,Admin")]
        public IActionResult Edit(int id)
        {

            Intrebare intrebare = db.Intrebari.Include("Categorie")
                                         .Where(art => art.IntrebareId == id)
                                         .First();

            intrebare.Categ = GetAllCategories();

            if ((intrebare.UserId == _userManager.GetUserId(User)) ||
                User.IsInRole("Admin"))
            {
                return View(intrebare);
            }
            else
            {

                TempData["message"] = "Nu aveti dreptul sa faceti modificari asupra unei intrebari care nu va apartine";
                TempData["messageType"] = "alert-danger";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [Authorize(Roles = "User,Admin")]
        public IActionResult Edit(int id, Intrebare requestArticle)
        {
            var sanitizer = new HtmlSanitizer();

            Intrebare intrebare = db.Intrebari.Find(id);

            if (ModelState.IsValid)
            {
                if ((intrebare.UserId == _userManager.GetUserId(User))
                    || User.IsInRole("Admin"))
                {

                    requestArticle.Continut = sanitizer.Sanitize(requestArticle.Continut);

                    intrebare.Continut = requestArticle.Continut;

                    intrebare.Data = DateTime.Now;
                    intrebare.CategorieId = requestArticle.CategorieId;
                    TempData["message"] = "Intrebarea a fost modificata";
                    TempData["messageType"] = "alert-success";
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["message"] = "Nu aveti dreptul sa faceti modificari asupra unei intrebari care nu va apartine";
                    TempData["messageType"] = "alert-danger";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                requestArticle.Categ = GetAllCategories();
                return View(requestArticle);
            }
        }

        [HttpPost]
        [Authorize(Roles = "User,Admin")]
        public ActionResult Delete(int id)
        {
            Intrebare intrebare = db.Intrebari.Include("Raspunsuri")
                                         .Where(art => art.IntrebareId == id)
                                         .First();

            if ((intrebare.UserId == _userManager.GetUserId(User)) || User.IsInRole("Admin"))
            {
                db.Intrebari.Remove(intrebare);
                db.SaveChanges();
                TempData["message"] = "Intrebarea a fost stearsa cu succes!";
                TempData["messageType"] = "alert-success";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["message"] = "Nu aveti dreptul sa stergeti o intrebare care nu va apartine!";
                TempData["messageType"] = "alert-danger";
                return RedirectToAction("Index");
            }
        }

        // Conditiile de afisare pentru butoanele de editare si stergere
        // butoanele aflate in view-uri
        private void SetAccessRights()
        {
            ViewBag.AfisareButoane = true;

            ViewBag.UserCurent = _userManager.GetUserId(User);

            ViewBag.EsteAdmin = User.IsInRole("Admin");
        }

        [NonAction]
        public IEnumerable<SelectListItem> GetAllCategories()
        {
            // generam o lista de tipul SelectListItem fara elemente
            var selectList = new List<SelectListItem>();

            // extragem toate categoriile din baza de date
            var categories = from cat in db.Categorii
                             select cat;

            // iteram prin categorii
            foreach (var category in categories)
            {
                // adaugam in lista elementele necesare pentru dropdown
                // id-ul categoriei si denumirea acesteia
                selectList.Add(new SelectListItem
                {
                    Value = category.CategorieId.ToString(),
                    Text = category.Denumire
                });
            }
            // returnam lista de categorii
            return selectList;
        }
        public IActionResult IndexNou()
        {
            return View();
        }
    }
}