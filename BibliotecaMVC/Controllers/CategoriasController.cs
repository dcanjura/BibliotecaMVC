using BibliotecaMVC.Data;
using BibliotecaMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaMVC.Controllers
{
    public class CategoriasController : Controller
    {
        private readonly CategoriaRepository _categoriaRepository;
        private readonly ILogger<CategoriasController> _logger;

        public CategoriasController(CategoriaRepository categoriaRepository, ILogger<CategoriasController> logger)
        {
            _categoriaRepository = categoriaRepository;
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.LogInformation("Consultando el listado de categorías.");

            try
            {
                return View(_categoriaRepository.ObtenerCategorias());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocurrió un error al consultar el listado de categorías.");
                throw;
            }
        }

        public IActionResult Create()
        {
            _logger.LogInformation("Mostrando el formulario para crear una categoría.");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Categoria categoria)
        {
            _logger.LogInformation("Iniciando el registro de una nueva categoría.");

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("El formulario para crear una categoría contiene datos no válidos.");
                return View(categoria);
            }

            try
            {
                _categoriaRepository.Agregar(categoria);
                _logger.LogInformation("Categoría creada correctamente.");
                TempData["Mensaje"] = "Categoría agregada correctamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocurrió un error al crear una categoría.");
                throw;
            }
        }

        public IActionResult Edit(int id)
        {
            _logger.LogInformation("Mostrando el formulario de edición de la categoría con ID {CategoriaId}.", id);

            try
            {
                var categoria = _categoriaRepository.ObtenerPorId(id);
                if (categoria == null)
                {
                    _logger.LogWarning("No se encontró la categoría con ID {CategoriaId} para editar.", id);
                    return NotFound();
                }

                return View(categoria);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocurrió un error al cargar la categoría con ID {CategoriaId} para editar.", id);
                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Categoria categoria)
        {
            _logger.LogInformation("Iniciando la actualización de la categoría con ID {CategoriaId}.", id);

            if (id != categoria.Id)
            {
                _logger.LogWarning("El ID de la ruta no coincide con la categoría enviada para edición.");
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("El formulario de edición de la categoría con ID {CategoriaId} contiene datos no válidos.", id);
                return View(categoria);
            }

            try
            {
                if (!_categoriaRepository.Actualizar(categoria))
                {
                    _logger.LogWarning("No se encontró la categoría con ID {CategoriaId} para actualizar.", id);
                    return NotFound();
                }

                _logger.LogInformation("Categoría con ID {CategoriaId} actualizada correctamente.", id);
                TempData["Mensaje"] = "Categoría actualizada correctamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocurrió un error al actualizar la categoría con ID {CategoriaId}.", id);
                throw;
            }
        }

        public IActionResult Delete(int id)
        {
            _logger.LogInformation("Mostrando la confirmación para eliminar la categoría con ID {CategoriaId}.", id);

            try
            {
                var categoria = _categoriaRepository.ObtenerPorId(id);
                if (categoria == null)
                {
                    _logger.LogWarning("No se encontró la categoría con ID {CategoriaId} para eliminar.", id);
                    return NotFound();
                }

                return View(categoria);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocurrió un error al cargar la categoría con ID {CategoriaId} para eliminar.", id);
                throw;
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _logger.LogInformation("Iniciando la eliminación de la categoría con ID {CategoriaId}.", id);

            try
            {
                if (!_categoriaRepository.Eliminar(id))
                {
                    _logger.LogWarning("No se encontró la categoría con ID {CategoriaId} para confirmar la eliminación.", id);
                    return NotFound();
                }

                _logger.LogInformation("Categoría con ID {CategoriaId} eliminada correctamente.", id);
                TempData["Mensaje"] = "Categoría eliminada correctamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocurrió un error al eliminar la categoría con ID {CategoriaId}.", id);
                throw;
            }
        }
    }
}
