using BibliotecaMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaMVC.Controllers
{
    public class AutoresController : Controller
    {
        private static List<Autor> _autores = new List<Autor>()
            {
                new Autor
                {
                    ID = 1,
                    Nombre = "Gabriel",
                    Apellido = "García Márquez",
                    Nacionalidad = "Colombiana",
                    FechaNacimiento = new DateTime(1927, 3, 6),
                    Activo = true
                },
                new Autor
                {
                    ID = 2,
                    Nombre = "Jane",
                    Apellido = "Austen",
                    Nacionalidad = "Británica",
                    FechaNacimiento = new DateTime(1775, 12, 16),
                    Activo = false
                },
                new Autor
                {
                    ID = 3,
                    Nombre = "Ernest",
                    Apellido = "Hermingway",
                    Nacionalidad = "Estadounidense",
                    FechaNacimiento = new DateTime(1899, 7, 21),
                    Activo = false
                },
                new Autor
                {
                    ID = 4,
                    Nombre = "Isabel",
                    Apellido = "Allende",
                    Nacionalidad = "Chilena",
                    FechaNacimiento = new DateTime(1942, 8, 2),
                    Activo = true
                },
                new Autor
                {
                    ID = 5,
                    Nombre = "Víctor",
                    Apellido = "Hugo",
                    Nacionalidad = "Colombiana",
                    FechaNacimiento = new DateTime(1802, 2, 26),
                    Activo = false
                }
            };

        public IActionResult Index()
        {
            return View(_autores);
        }

        public IActionResult Details(int id)
        {
            var autor = _autores.FirstOrDefault(x => x.ID == id);

            if (autor == null)
            {
                return NotFound();
            }

            return View(autor);
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

            if (_autores.Any())
            {
                autor.ID = _autores.Max(x => x.ID) + 1;
            }
            else
            {
                autor.ID = 1;
            }

            _autores.Add(autor);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var autor = _autores.FirstOrDefault(x => x.ID == id);
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

            var existente = _autores.FirstOrDefault(x => x.ID == id);
            if (existente == null)
            {
                return NotFound();
            }

            existente.Nombre = autor.Nombre;
            existente.Apellido = autor.Apellido;
            existente.Nacionalidad = autor.Nacionalidad;
            existente.FechaNacimiento = autor.FechaNacimiento;
            existente.Activo = autor.Activo;
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var autor = _autores.FirstOrDefault(x => x.ID == id);
            return autor == null ? NotFound() : View(autor);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var autor = _autores.FirstOrDefault(x => x.ID == id);
            if (autor == null)
            {
                return NotFound();
            }

            _autores.Remove(autor);
            return RedirectToAction(nameof(Index));
        }
    }
}
