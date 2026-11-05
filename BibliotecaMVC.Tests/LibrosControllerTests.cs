using BibliotecaMVC.Controllers;
using BibliotecaMVC.Models;
using BibliotecaMVC.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace BibliotecaMVC.Tests;

public class LibrosControllerTests
{
    [Fact]
    public void Index_ConLibros_RetornaVistaConListado()
    {
        var libros = new List<Libro>
        {
            CrearLibro(1, "Clean Code"),
            CrearLibro(2, "El Quijote")
        };
        var servicio = new Mock<ILibroService>();
        servicio.Setup(s => s.ObtenerLibros()).Returns(libros);
        var controlador = CrearControlador(servicio);

        var resultado = Assert.IsType<ViewResult>(controlador.Index());
        var modelo = Assert.IsType<List<Libro>>(resultado.Model);

        Assert.Equal(2, modelo.Count);
        Assert.Equal("Clean Code", modelo[0].Titulo);
        servicio.Verify(s => s.ObtenerLibros(), Times.Once);
    }

    [Fact]
    public void Index_SinLibros_RetornaColeccionVacia()
    {
        var servicio = new Mock<ILibroService>();
        servicio.Setup(s => s.ObtenerLibros()).Returns(new List<Libro>());
        var controlador = CrearControlador(servicio);

        var resultado = Assert.IsType<ViewResult>(controlador.Index());
        var modelo = Assert.IsType<List<Libro>>(resultado.Model);

        Assert.Empty(modelo);
        servicio.Verify(s => s.ObtenerLibros(), Times.Once);
    }

    [Fact]
    public void Details_CuandoExisteLibro_RetornaVistaConLibro()
    {
        var libro = CrearLibro(1, "Clean Code");
        var servicio = new Mock<ILibroService>();
        servicio.Setup(s => s.ObtenerPorId(1)).Returns(libro);
        var controlador = CrearControlador(servicio);

        var resultado = Assert.IsType<ViewResult>(controlador.Details(1));
        var modelo = Assert.IsType<Libro>(resultado.Model);

        Assert.NotNull(modelo);
        Assert.Equal(1, modelo.ID);
        Assert.Equal("Clean Code", modelo.Titulo);
        servicio.Verify(s => s.ObtenerPorId(1), Times.Once);
    }

    [Fact]
    public void Details_CuandoNoExisteLibro_RetornaNotFound()
    {
        var servicio = new Mock<ILibroService>();
        servicio.Setup(s => s.ObtenerPorId(99)).Returns((Libro?)null);
        var controlador = CrearControlador(servicio);

        var resultado = controlador.Details(99);

        Assert.IsType<NotFoundResult>(resultado);
        servicio.Verify(s => s.ObtenerPorId(99), Times.Once);
    }

    [Fact]
    public void Create_ConDatosValidos_AgregaLibroYRedirige()
    {
        var libro = CrearLibro(0, "Nuevo libro");
        var servicio = new Mock<ILibroService>();
        var controlador = CrearControlador(servicio);

        var resultado = Assert.IsType<RedirectToActionResult>(controlador.Create(libro));

        Assert.Equal("Index", resultado.ActionName);
        servicio.Verify(s => s.Agregar(libro), Times.Once);
    }

    [Fact]
    public void Create_ConDatosInvalidos_NoAgregaLibro()
    {
        var libro = CrearLibro(0, string.Empty);
        var servicio = new Mock<ILibroService>();
        var controlador = CrearControlador(servicio);
        controlador.ModelState.AddModelError("Titulo", "El título es obligatorio.");

        var resultado = Assert.IsType<ViewResult>(controlador.Create(libro));

        Assert.Equal(libro, resultado.Model);
        servicio.Verify(s => s.Agregar(It.IsAny<Libro>()), Times.Never);
    }

    [Fact]
    public void Edit_ConLibroExistente_ActualizaYRedirige()
    {
        var libro = CrearLibro(1, "Título actualizado");
        var servicio = new Mock<ILibroService>();
        servicio.Setup(s => s.Actualizar(libro)).Returns(true);
        var controlador = CrearControlador(servicio);

        var resultado = Assert.IsType<RedirectToActionResult>(controlador.Edit(1, libro));

        Assert.Equal("Index", resultado.ActionName);
        servicio.Verify(s => s.Actualizar(libro), Times.Once);
    }

    [Fact]
    public void Edit_CuandoNoExisteLibro_RetornaNotFound()
    {
        var libro = CrearLibro(20, "Libro inexistente");
        var servicio = new Mock<ILibroService>();
        servicio.Setup(s => s.Actualizar(libro)).Returns(false);
        var controlador = CrearControlador(servicio);

        var resultado = controlador.Edit(20, libro);

        Assert.IsType<NotFoundResult>(resultado);
        servicio.Verify(s => s.Actualizar(libro), Times.Once);
    }

    [Theory]
    [InlineData(1, 2)]
    [InlineData(3, 4)]
    public void Edit_ConIdsDiferentes_RetornaBadRequest(int idRuta, int idLibro)
    {
        var servicio = new Mock<ILibroService>();
        var controlador = CrearControlador(servicio);
        var libro = CrearLibro(idLibro, "Libro de prueba");

        var resultado = controlador.Edit(idRuta, libro);

        Assert.IsType<BadRequestResult>(resultado);
        servicio.Verify(s => s.Actualizar(It.IsAny<Libro>()), Times.Never);
    }

    [Fact]
    public void DeleteConfirmed_ConLibroExistente_EliminaYRedirige()
    {
        var servicio = new Mock<ILibroService>();
        servicio.Setup(s => s.Eliminar(1)).Returns(true);
        var controlador = CrearControlador(servicio);

        var resultado = Assert.IsType<RedirectToActionResult>(controlador.DeleteConfirmed(1));

        Assert.Equal("Index", resultado.ActionName);
        servicio.Verify(s => s.Eliminar(1), Times.Once);
    }

    [Fact]
    public void DeleteConfirmed_CuandoNoExisteLibro_RetornaNotFound()
    {
        var servicio = new Mock<ILibroService>();
        servicio.Setup(s => s.Eliminar(50)).Returns(false);
        var controlador = CrearControlador(servicio);

        var resultado = controlador.DeleteConfirmed(50);

        Assert.IsType<NotFoundResult>(resultado);
        servicio.Verify(s => s.Eliminar(50), Times.Once);
    }

    private static LibrosController CrearControlador(Mock<ILibroService> servicio)
    {
        var logger = new Mock<ILogger<LibrosController>>();
        var controlador = new LibrosController(servicio.Object, logger.Object)
        {
            TempData = new TempDataDictionary(
                new DefaultHttpContext(),
                Mock.Of<ITempDataProvider>())
        };

        return controlador;
    }

    private static Libro CrearLibro(int id, string titulo)
    {
        return new Libro
        {
            ID = id,
            Titulo = titulo,
            Autor = "Autor de prueba",
            Categoria = "Pruebas",
            Precio = 25M,
            Disponible = true
        };
    }
}
