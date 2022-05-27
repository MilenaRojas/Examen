using Lab3MVC.Models;
using Lab3MVC.Models.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Lab3MVC.Controllers
{
    public class MajorController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        MajorDAO majorDAO;

        public MajorController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        // GET: MajorController
        public ActionResult Index()
        {
            majorDAO = new MajorDAO(_configuration);

            return View(majorDAO.Get());
        }

        // GET: MajorController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MajorController/Create
        public ActionResult Create()
        {
            return View();
        }

        public IActionResult Get()
        {
            try
            {
                majorDAO = new MajorDAO(_configuration);

                return Ok(majorDAO.Get());
            }
            catch (Exception)
            {

                return Error();
            }
          
        }


        // POST: MajorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MajorController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MajorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MajorController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MajorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
