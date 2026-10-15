using BibliotecaMVC.Data;
using BibliotecaMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaMVC.Controllers
{
    [Authorize]
    public class LibrosController : Controller
    {
        private readonly BibliotecaDbContext _context;

        public LibrosController(BibliotecaDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = RolesBiblioteca.Administrador + "," + RolesBiblioteca.Lector)]
        public IActionResult Index()
        {
            var libros = _context.Libros.OrderBy(libro => libro.Titulo).ToList();
            return View(libros);
        }

        [Authorize(Roles = RolesBiblioteca.Administrador)]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RolesBiblioteca.Administrador)]
        public IActionResult Create(Libro libro)
        {
            if (!ModelState.IsValid)
            {
                return View(libro);
            }

            _context.Libros.Add(libro);
            _context.SaveChanges();

            TempData["Mensaje"] = "Libro agregado correctamente.";

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = RolesBiblioteca.Administrador)]
        public IActionResult Edit(int id)
        {
            var libro = _context.Libros.Find(id);
            return libro == null ? NotFound() : View(libro);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RolesBiblioteca.Administrador)]
        public IActionResult Edit(int id, Libro libro)
        {
            if (id != libro.ID)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(libro);
            }

            var libroExistente = _context.Libros.Find(id);
            if (libroExistente == null)
            {
                return NotFound();
            }

            _context.Libros.Update(libro);
            _context.SaveChanges();
            TempData["Mensaje"] = "Libro actualizado correctamente.";

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = RolesBiblioteca.Administrador)]
        public IActionResult Delete(int id)
        {
            var libro = _context.Libros.Find(id);
            return libro == null ? NotFound() : View(libro);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RolesBiblioteca.Administrador)]
        public IActionResult DeleteConfirmed(int id)
        {
            var libro = _context.Libros.Find(id);
            if (libro == null)
            {
                return NotFound();
            }

            _context.Libros.Remove(libro);
            _context.SaveChanges();
            TempData["Mensaje"] = "Libro eliminado correctamente.";

            return RedirectToAction(nameof(Index));
        }
    }
}
