# Evidencias de reportes

Al probar la aplicación, guardar en esta carpeta los archivos descargados y las capturas solicitadas.

| Archivo sugerido | Cómo se obtiene |
| --- | --- |
| `ReporteLibros.pdf` | Botón **Generar reporte PDF** en `Reportes`. |
| `ReporteLibros.xlsx` | Botón **Exportar a Excel** en `Reportes`. |
| `01-pagina-reportes.png` | Captura de la pantalla de reportes con los dos botones. |
| `02-reporte-pdf.png` | Captura del PDF abierto con los libros mostrados. |
| `03-reporte-excel.png` | Captura del Excel abierto con los libros mostrados. |

## Librerías utilizadas

- **QuestPDF** genera el documento PDF con encabezado, tabla de libros, fecha de generación, total de libros y numeración de páginas.
- **ClosedXML** crea el archivo Excel con una hoja llamada `Reporte de Libros`, encabezados con formato, bordes, total de libros y columnas ajustadas.

Los dos reportes consultan los libros registrados mediante el servicio del módulo Libros, el cual utiliza Entity Framework Core y SQL Server.
