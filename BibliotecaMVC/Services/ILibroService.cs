using BibliotecaMVC.Models;

namespace BibliotecaMVC.Services
{
    public interface ILibroService
    {
        List<Libro> ObtenerLibros();
        Libro? ObtenerPorId(int id);
        void Agregar(Libro libro);
        bool Actualizar(Libro libro);
        bool Eliminar(int id);
    }
}
