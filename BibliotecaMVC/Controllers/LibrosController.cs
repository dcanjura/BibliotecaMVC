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
        private readonly ILogger<LibrosController> _logger;

        public LibrosController(BibliotecaDbContext context, ILogger<LibrosController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [Authorize(Roles = RolesBiblioteca.Admin + "," + RolesBiblioteca.Bibliotecario + "," + RolesBiblioteca.Usuario)]
        public IActionResult Index()
        {
            _logger.LogInformation("Consultando el listado de libros.");

            try
            {
                var libros = _context.Libros.OrderBy(libro => libro.Titulo).ToList();
                return View(libros);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocurrió un error al consultar el listado de libros.");
                throw;
            }
        }

        [Authorize(Roles = RolesBiblioteca.Admin + "," + RolesBiblioteca.Bibliotecario + "," + RolesBiblioteca.Usuario)]
        public IActionResult Details(int id)
        {
            _logger.LogInformation("Consultando el detalle del libro con ID {LibroId}.", id);

            try
            {
                var libro = _context.Libros.Find(id);
                if (libro == null)
                {
                    _logger.LogWarning("No se encontró el libro con ID {LibroId}.", id);
                    return NotFound();
                }

                return View(libro);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocurrió un error al consultar el libro con ID {LibroId}.", id);
                throw;
            }
        }

        [Authorize(Roles = RolesBiblioteca.Admin + "," + RolesBiblioteca.Bibliotecario)]
        public IActionResult Create()
        {
            _logger.LogInformation("Mostrando el formulario para crear un libro.");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RolesBiblioteca.Admin + "," + RolesBiblioteca.Bibliotecario)]
        public IActionResult Create(Libro libro)
        {
            _logger.LogInformation("Iniciando el registro de un nuevo libro.");

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("El formulario para crear un libro contiene datos no válidos.");
                return View(libro);
            }

            try
            {
                _context.Libros.Add(libro);
                _context.SaveChanges();
                _logger.LogInformation("Libro creado correctamente con ID {LibroId}.", libro.ID);
                TempData["Mensaje"] = "Libro agregado correctamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocurrió un error al crear un libro.");
                throw;
            }
        }

        [Authorize(Roles = RolesBiblioteca.Admin + "," + RolesBiblioteca.Bibliotecario)]
        public IActionResult Edit(int id)
        {
            _logger.LogInformation("Mostrando el formulario de edición del libro con ID {LibroId}.", id);

            try
            {
                var libro = _context.Libros.Find(id);
                if (libro == null)
                {
                    _logger.LogWarning("No se encontró el libro con ID {LibroId} para editar.", id);
                    return NotFound();
                }

                return View(libro);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocurrió un error al cargar el libro con ID {LibroId} para editar.", id);
                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RolesBiblioteca.Admin + "," + RolesBiblioteca.Bibliotecario)]
        public IActionResult Edit(int id, Libro libro)
        {
            _logger.LogInformation("Iniciando la actualización del libro con ID {LibroId}.", id);

            if (id != libro.ID)
            {
                _logger.LogWarning("El ID de la ruta no coincide con el libro enviado para edición.");
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("El formulario de edición del libro con ID {LibroId} contiene datos no válidos.", id);
                return View(libro);
            }

            try
            {
                var libroExistente = _context.Libros.Find(id);
                if (libroExistente == null)
                {
                    _logger.LogWarning("No se encontró el libro con ID {LibroId} para actualizar.", id);
                    return NotFound();
                }

                _context.Libros.Update(libro);
                _context.SaveChanges();
                _logger.LogInformation("Libro con ID {LibroId} actualizado correctamente.", id);
                TempData["Mensaje"] = "Libro actualizado correctamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocurrió un error al actualizar el libro con ID {LibroId}.", id);
                throw;
            }
        }

        [Authorize(Roles = RolesBiblioteca.Admin + "," + RolesBiblioteca.Bibliotecario)]
        public IActionResult Delete(int id)
        {
            _logger.LogInformation("Mostrando la confirmación para eliminar el libro con ID {LibroId}.", id);

            try
            {
                var libro = _context.Libros.Find(id);
                if (libro == null)
                {
                    _logger.LogWarning("No se encontró el libro con ID {LibroId} para eliminar.", id);
                    return NotFound();
                }

                return View(libro);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocurrió un error al cargar el libro con ID {LibroId} para eliminar.", id);
                throw;
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RolesBiblioteca.Admin + "," + RolesBiblioteca.Bibliotecario)]
        public IActionResult DeleteConfirmed(int id)
        {
            _logger.LogInformation("Iniciando la eliminación del libro con ID {LibroId}.", id);

            try
            {
                var libro = _context.Libros.Find(id);
                if (libro == null)
                {
                    _logger.LogWarning("No se encontró el libro con ID {LibroId} para confirmar la eliminación.", id);
                    return NotFound();
                }

                _context.Libros.Remove(libro);
                _context.SaveChanges();
                _logger.LogInformation("Libro con ID {LibroId} eliminado correctamente.", id);
                TempData["Mensaje"] = "Libro eliminado correctamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocurrió un error al eliminar el libro con ID {LibroId}.", id);
                throw;
            }
        }
    }
}
