using BibliotecaMVC.Data;
using BibliotecaMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaMVC.Controllers
{
    public class CategoriasController : Controller
    {
        private readonly CategoriaRepository _categoriaRepository;

        public CategoriasController(CategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public IActionResult Index()
        {
            return View(_categoriaRepository.ObtenerCategorias());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Categoria categoria)
        {
            if (!ModelState.IsValid) return View(categoria);

            _categoriaRepository.Agregar(categoria);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var categoria = _categoriaRepository.ObtenerPorId(id);
            return categoria == null ? NotFound() : View(categoria);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Categoria categoria)
        {
            if (id != categoria.Id) return BadRequest();
            if (!ModelState.IsValid) return View(categoria);

            return _categoriaRepository.Actualizar(categoria)
                ? RedirectToAction(nameof(Index))
                : NotFound();
        }

        public IActionResult Delete(int id)
        {
            var categoria = _categoriaRepository.ObtenerPorId(id);
            return categoria == null ? NotFound() : View(categoria);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            return _categoriaRepository.Eliminar(id)
                ? RedirectToAction(nameof(Index))
                : NotFound();
        }
    }
}
