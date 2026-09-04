using BibliotecaMVC.Models;

namespace BibliotecaMVC.Services
{
    public class AutorService : IAutorService
    {
        private static readonly List<Autor> _autores = new()
        {
            new Autor { ID = 1, Nombre = "Gabriel", Apellido = "García Márquez", Nacionalidad = "Colombiana", FechaNacimiento = new DateTime(1927, 3, 6), Activo = true },
            new Autor { ID = 2, Nombre = "Jane", Apellido = "Austen", Nacionalidad = "Británica", FechaNacimiento = new DateTime(1775, 12, 16), Activo = false },
            new Autor { ID = 3, Nombre = "Ernest", Apellido = "Hemingway", Nacionalidad = "Estadounidense", FechaNacimiento = new DateTime(1899, 7, 21), Activo = false },
            new Autor { ID = 4, Nombre = "Isabel", Apellido = "Allende", Nacionalidad = "Chilena", FechaNacimiento = new DateTime(1942, 8, 2), Activo = true },
            new Autor { ID = 5, Nombre = "Víctor", Apellido = "Hugo", Nacionalidad = "Francesa", FechaNacimiento = new DateTime(1802, 2, 26), Activo = false }
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
