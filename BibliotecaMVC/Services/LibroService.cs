using BibliotecaMVC.Data;
using BibliotecaMVC.Models;

namespace BibliotecaMVC.Services
{
    public class LibroService : ILibroService
    {
        private readonly BibliotecaDbContext _context;

        public LibroService(BibliotecaDbContext context)
        {
            _context = context;
        }

        public List<Libro> ObtenerLibros()
        {
            return _context.Libros.OrderBy(libro => libro.Titulo).ToList();
        }

        public Libro? ObtenerPorId(int id)
        {
            return _context.Libros.Find(id);
        }

        public void Agregar(Libro libro)
        {
            _context.Libros.Add(libro);
            _context.SaveChanges();
        }

        public bool Actualizar(Libro libro)
        {
            if (_context.Libros.Find(libro.ID) == null)
            {
                return false;
            }

            _context.Libros.Update(libro);
            _context.SaveChanges();
            return true;
        }

        public bool Eliminar(int id)
        {
            var libro = _context.Libros.Find(id);
            if (libro == null)
            {
                return false;
            }

            _context.Libros.Remove(libro);
            _context.SaveChanges();
            return true;
        }
    }
}
