using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VideoGameLibrary.Data;
using VideoGameLibrary.Interfaces;
using VideoGameLibrary.Models;

namespace VideoGameLibrary.Controllers
{
    public class HomeController : Controller
    {
        IDataAccessLayer dal;
        public HomeController(IDataAccessLayer indal)
        {
            dal = indal;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Collection(string? key)
        {
            if (key == null)
            {
                IEnumerable<Game> allGames = dal.GetCollection();
                return View(allGames);
            }
            else if (key != null)
            {
                IEnumerable<Game> search = dal.SearchForGames(key);
                return View(search);
            }
            return View();
        }
        [HttpGet]
        public IActionResult Filter()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Filter(string genre, string platform, string rating)
        {
            if(ModelState.IsValid)
            {
                IEnumerable<Game> filteredGames = dal.FilterCollection(genre, platform, rating);
                return View("Collection", filteredGames);
            } else
            {
                return View();
            }
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(Game g)
        {
            if (ModelState.IsValid)
            {
                dal.AddGame(g);
                return RedirectToAction("Collection", "Home");
            }
            else
            {
                return View();
            }
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {

            if (id == null)
            {
                ViewData["Error"] = "Game id not provided";
                return View();
            }
            Game? g = dal.GetCollection().Where(g => g.Id == id).FirstOrDefault();
            if (g == null)
            {
                ViewData["Error"] = "Game id does not exist";
            }
            return View(g);            
        }
        [HttpPost]
        public IActionResult Edit(Game g)
        {
            if (ModelState.IsValid)
            {
                dal.EditGame(g);
                TempData["Success"] = "Game Updated";
                return RedirectToAction("Collection", "Home");
            }
            return View();

        }
        public IActionResult Delete(int? id)
        {
            dal.RemoveGame(id);
            TempData["Success"] = "Game Deleted";
            return RedirectToAction("Collection", "Home");
        }
        public IActionResult Loan(int id, string loanName)
        {
            Game? g = dal.GetCollection().Where(g => g.Id == id).FirstOrDefault();
            g.LoanDate = DateTime.Now;
            g.LoanedTo = loanName;
            return RedirectToAction("Collection", "Home");
        }
        public IActionResult Return(int id)
        {
            Game? g = dal.GetCollection().Where(g => g.Id == id).FirstOrDefault();
            g.LoanDate = null;
            g.LoanedTo = null;
            return RedirectToAction("Collection", "Home");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
