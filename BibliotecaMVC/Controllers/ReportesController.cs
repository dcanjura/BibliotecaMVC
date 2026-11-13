using BibliotecaMVC.Data;
using BibliotecaMVC.Models;
using BibliotecaMVC.Services;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace BibliotecaMVC.Controllers
{
    [Authorize(Roles = RolesBiblioteca.Admin + "," + RolesBiblioteca.Bibliotecario + "," + RolesBiblioteca.Usuario)]
    public class ReportesController : Controller
    {
        private readonly ILibroService _libroService;
        private readonly ILogger<ReportesController> _logger;

        public ReportesController(ILibroService libroService, ILogger<ReportesController> logger)
        {
            _libroService = libroService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View(_libroService.ObtenerLibros());
        }

        public IActionResult LibrosPdf()
        {
            var libros = _libroService.ObtenerLibros();
            var fechaGeneracion = DateTime.Now;

            _logger.LogInformation("Generando reporte PDF de {TotalLibros} libros.", libros.Count);

            var documento = Document.Create(contenedor =>
            {
                contenedor.Page(pagina =>
                {
                    pagina.Size(PageSizes.A4);
                    pagina.Margin(30);
                    pagina.DefaultTextStyle(estilo => estilo.FontSize(10));

                    pagina.Header().Column(columna =>
                    {
                        columna.Item().Text("BibliotecaMVC").FontSize(18).SemiBold().FontColor(Colors.Blue.Darken2);
                        columna.Item().Text("Reporte de libros").FontSize(14).SemiBold();
                        columna.Item().Text($"Generado el {fechaGeneracion:dd/MM/yyyy HH:mm}").FontColor(Colors.Grey.Darken1);
                        columna.Item().Text($"Total de libros: {libros.Count}").SemiBold();
                    });

                    pagina.Content().PaddingVertical(15).Table(tabla =>
                    {
                        tabla.ColumnsDefinition(columnas =>
                        {
                            columnas.ConstantColumn(35);
                            columnas.RelativeColumn(3);
                            columnas.RelativeColumn(2);
                            columnas.RelativeColumn(2);
                        });

                        tabla.Header(encabezado =>
                        {
                            encabezado.Cell().Element(EstiloEncabezado).Text("ID");
                            encabezado.Cell().Element(EstiloEncabezado).Text("Libro");
                            encabezado.Cell().Element(EstiloEncabezado).Text("Autor");
                            encabezado.Cell().Element(EstiloEncabezado).Text("Categoria");
                        });

                        if (libros.Count == 0)
                        {
                            tabla.Cell().ColumnSpan(4).Element(EstiloCelda).AlignCenter()
                                .Text("No hay libros registrados.");
                        }
                        else
                        {
                            foreach (var libro in libros)
                            {
                                tabla.Cell().Element(EstiloCelda).Text(libro.ID.ToString());
                                tabla.Cell().Element(EstiloCelda).Text(libro.Titulo);
                                tabla.Cell().Element(EstiloCelda).Text(libro.Autor);
                                tabla.Cell().Element(EstiloCelda).Text(libro.Categoria);
                            }
                        }
                    });

                    pagina.Footer().AlignCenter().Text(texto =>
                    {
                        texto.Span("Pagina ");
                        texto.CurrentPageNumber();
                        texto.Span(" de ");
                        texto.TotalPages();
                    });
                });
            });

            return File(documento.GeneratePdf(), "application/pdf", "ReporteLibros.pdf");
        }

        public IActionResult LibrosExcel()
        {
            var libros = _libroService.ObtenerLibros();
            var fechaGeneracion = DateTime.Now;

            _logger.LogInformation("Generando reporte Excel de {TotalLibros} libros.", libros.Count);

            using var libroExcel = new XLWorkbook();
            var hoja = libroExcel.Worksheets.Add("Reporte de Libros");

            hoja.Cell(1, 1).Value = "BibliotecaMVC - Reporte de Libros";
            hoja.Range(1, 1, 1, 4).Merge();
            hoja.Cell(1, 1).Style.Font.Bold = true;
            hoja.Cell(1, 1).Style.Font.FontSize = 16;
            hoja.Cell(1, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            hoja.Cell(2, 1).Value = $"Generado el {fechaGeneracion:dd/MM/yyyy HH:mm}";
            hoja.Range(2, 1, 2, 4).Merge();
            hoja.Cell(2, 1).Style.Font.Italic = true;

            hoja.Cell(4, 1).Value = "ID";
            hoja.Cell(4, 2).Value = "Nombre del libro";
            hoja.Cell(4, 3).Value = "Autor";
            hoja.Cell(4, 4).Value = "Categoria";

            var encabezados = hoja.Range(4, 1, 4, 4);
            encabezados.Style.Fill.BackgroundColor = XLColor.FromHtml("#0D6EFD");
            encabezados.Style.Font.FontColor = XLColor.White;
            encabezados.Style.Font.Bold = true;
            encabezados.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            encabezados.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            encabezados.Style.Border.InsideBorder = XLBorderStyleValues.Thin;

            var fila = 5;
            foreach (var libro in libros)
            {
                hoja.Cell(fila, 1).Value = libro.ID;
                hoja.Cell(fila, 2).Value = libro.Titulo;
                hoja.Cell(fila, 3).Value = libro.Autor;
                hoja.Cell(fila, 4).Value = libro.Categoria;
                fila++;
            }

            if (libros.Count == 0)
            {
                hoja.Cell(fila, 1).Value = "No hay libros registrados.";
                hoja.Range(fila, 1, fila, 4).Merge();
                hoja.Cell(fila, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                fila++;
            }

            hoja.Cell(fila, 3).Value = "Total de libros:";
            hoja.Cell(fila, 3).Style.Font.Bold = true;
            hoja.Cell(fila, 4).Value = libros.Count;
            hoja.Cell(fila, 4).Style.Font.Bold = true;
            fila++;

            var datos = hoja.Range(4, 1, fila - 1, 4);
            datos.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            datos.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
            hoja.Columns().AdjustToContents();

            using var memoria = new MemoryStream();
            libroExcel.SaveAs(memoria);

            return File(
                memoria.ToArray(),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "ReporteLibros.xlsx");
        }

        private static IContainer EstiloEncabezado(IContainer contenedor)
        {
            return contenedor
                .Background(Colors.Blue.Darken2)
                .Border(1)
                .BorderColor(Colors.Grey.Lighten2)
                .Padding(5)
                .DefaultTextStyle(estilo => estilo.SemiBold().FontColor(Colors.White));
        }

        private static IContainer EstiloCelda(IContainer contenedor)
        {
            return contenedor
                .Border(1)
                .BorderColor(Colors.Grey.Lighten2)
                .Padding(5);
        }
    }
}
