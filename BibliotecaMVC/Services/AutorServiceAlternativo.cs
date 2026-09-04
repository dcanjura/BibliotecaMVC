using BibliotecaMVC.Models;

namespace BibliotecaMVC.Services
{
    // Segunda implementación para demostrar que el controlador depende de la interfaz.
    public class AutorServiceAlternativo : IAutorService
    {
        private static readonly List<Autor> _autores = new()
        {
            new Autor { ID = 1, Nombre = "Julio", Apellido = "Verne", Nacionalidad = "Francesa", FechaNacimiento = new DateTime(1828, 2, 8), Activo = true },
            new Autor { ID = 2, Nombre = "Emily", Apellido = "Bronte", Nacionalidad = "Británica", FechaNacimiento = new DateTime(1818, 7, 30), Activo = true },
            new Autor { ID = 3, Nombre = "Mario", Apellido = "Vargas Llosa", Nacionalidad = "Peruana", FechaNacimiento = new DateTime(1936, 3, 28), Activo = true }
        };

        public List<Autor> ObtenerAutores() => _autores;

        public Autor? ObtenerAutorPorId(int id) => _autores.FirstOrDefault(autor => autor.ID == id);

        public void AgregarAutor(Autor autor)
        {
            autor.ID = _autores.Any() ? _autores.Max(x => x.ID) + 1 : 1;
            _autores.Add(autor);
        }

        public bool ActualizarAutor(Autor autor)
        {
            var existente = ObtenerAutorPorId(autor.ID);
            if (existente == null) return false;

            existente.Nombre = autor.Nombre;
            existente.Apellido = autor.Apellido;
            existente.Nacionalidad = autor.Nacionalidad;
            existente.FechaNacimiento = autor.FechaNacimiento;
            existente.Activo = autor.Activo;
            return true;
        }

        public bool EliminarAutor(int id)
        {
            var autor = ObtenerAutorPorId(id);
            if (autor == null) return false;

            _autores.Remove(autor);
            return true;
        }
    }
}
