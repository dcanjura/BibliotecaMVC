using BibliotecaMVC.Models;
using BibliotecaMVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaMVC.Controllers
{
    public class AutoresController : Controller
    {
        private readonly IAutorService _autorService;

        public AutoresController(IAutorService autorService)
        {
            _autorService = autorService;
        }

        public IActionResult Index()
        {
            return View(_autorService.ObtenerAutores());
        }

        public IActionResult Details(int id)
        {
            var autor = _autorService.ObtenerAutorPorId(id);
            return autor == null ? NotFound() : View(autor);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Autor autor)
        {
            if (!ModelState.IsValid)
            {
                return View(autor);
            }

            _autorService.AgregarAutor(autor);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var autor = _autorService.ObtenerAutorPorId(id);
            return autor == null ? NotFound() : View(autor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Autor autor)
        {
            if (id != autor.ID)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(autor);
            }

            return _autorService.ActualizarAutor(autor)
                ? RedirectToAction(nameof(Index))
                : NotFound();
        }

        public IActionResult Delete(int id)
        {
            var autor = _autorService.ObtenerAutorPorId(id);
            return autor == null ? NotFound() : View(autor);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            return _autorService.EliminarAutor(id)
                ? RedirectToAction(nameof(Index))
                : NotFound();
        }
    }
}
