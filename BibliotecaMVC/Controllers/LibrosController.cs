using BibliotecaMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaMVC.Controllers
{
    public class LibrosController : Controller
    {
        private static readonly List<Libro> _libros = new()
        {
            new Libro
            {
                ID = 1,
                Titulo = "Clean Code",
                Autor = "Robert Martin",
                Categoria = "Programación",
                Precio = 35.5M,
                Disponible = true,
                Imagen = "clean-code.png"
            },
            new Libro
            {
                ID = 2,
                Titulo = "Cien años de soledad",
                Autor = "Gabriel García Márquez",
                Categoria = "Literatura",
                Precio = 18M,
                Disponible = false,
                Imagen = "cien-anos-soledad.png"
            }
        };

        public IActionResult Index() => View(_libros);

        public IActionResult Details(int id)
        {
            var libro = _libros.FirstOrDefault(x => x.ID == id);
            return libro == null ? NotFound() : View(libro);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Libro libro)
        {
            if (!ModelState.IsValid)
            {
                return View(libro);
            }

            libro.ID = _libros.Any() ? _libros.Max(x => x.ID) + 1 : 1;
            _libros.Add(libro);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var libro = _libros.FirstOrDefault(x => x.ID == id);
            return libro == null ? NotFound() : View(libro);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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

            var existente = _libros.FirstOrDefault(x => x.ID == id);
            if (existente == null)
            {
                return NotFound();
            }

            existente.Titulo = libro.Titulo;
            existente.Autor = libro.Autor;
            existente.Categoria = libro.Categoria;
            existente.Precio = libro.Precio;
            existente.Disponible = libro.Disponible;
            existente.Imagen = libro.Imagen;
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var libro = _libros.FirstOrDefault(x => x.ID == id);
            return libro == null ? NotFound() : View(libro);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var libro = _libros.FirstOrDefault(x => x.ID == id);
            if (libro == null)
            {
                return NotFound();
            }

            _libros.Remove(libro);
            return RedirectToAction(nameof(Index));
        }
    }
}
