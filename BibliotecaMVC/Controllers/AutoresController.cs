using BibliotecaMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaMVC.Controllers
{
    public class AutoresController : Controller
    {
        public IActionResult Index()
        {
            List<Autor> autores = new List<Autor>()
            {
                new Autor
                {
                    ID = 1,
                    Nombre = "Gabriel",
                    Apellido = "García Márquez",
                    Nacionalidad = "Colombiana",
                    Fecha_de_Nacimiento = new DateOnly(1927, 3, 6),
                    Activo = true
                },
                new Autor
                {
                    ID = 2,
                    Nombre = "Jane",
                    Apellido = "Austen",
                    Nacionalidad = "Británica",
                    Fecha_de_Nacimiento = new DateOnly(1775, 12, 16),
                    Activo = false
                },
                new Autor
                {
                    ID = 3,
                    Nombre = "Ernest",
                    Apellido = "Hermingway",
                    Nacionalidad = "Estadounidense",
                    Fecha_de_Nacimiento = new DateOnly(1899, 7, 21),
                    Activo = false
                },
                new Autor
                {
                    ID = 4,
                    Nombre = "Isabel",
                    Apellido = "Allende",
                    Nacionalidad = "Chilena",
                    Fecha_de_Nacimiento = new DateOnly(1942, 8, 2),
                    Activo = true
                },
                new Autor
                {
                    ID = 5,
                    Nombre = "Víctor",
                    Apellido = "Hugo",
                    Nacionalidad = "Colombiana",
                    Fecha_de_Nacimiento = new DateOnly(1802, 2, 26),
                    Activo = false
                }
            };

            ViewBag.Autores = autores;

            return View();
        }
    }
}
