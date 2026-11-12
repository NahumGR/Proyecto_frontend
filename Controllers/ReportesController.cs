using BibliotecaMVC.Data;
using BibliotecaMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClosedXML.Excel;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace BibliotecaMVC.Controllers{
    
    [Authorize(Roles = "Admin,Bibliotecario")]
    public class ReportesController : Controller{

        private readonly BibliotecaContext _context;
        private readonly ILogger<ReportesController> _logger;

        public ReportesController(BibliotecaContext context, ILogger<ReportesController> logger){
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> Index(){
            var totalLibros = await _context.Libros.CountAsync();
            return View(totalLibros);
        }

        public async Task<IActionResult> LibrosPdf(){
            var libros = await ObtenerLibros();
            return File(GenerarReportePdf(libros), "application/pdf");
        }

        public async Task<IActionResult> DescargarLibrosPdf(){
            var libros = await ObtenerLibros();
            return File(GenerarReportePdf(libros), "application/pdf", "ReporteLibros.pdf");
        }

        public async Task<IActionResult> ExportarLibrosExcel(){
            var libros = await ObtenerLibros();
            return File(GenerarReporteExcel(libros),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "ReporteLibros.xlsx");
        }

        private async Task<List<Libro>> ObtenerLibros(){
            return await _context.Libros
                .AsNoTracking()
                .OrderBy(libro => libro.Titulo)
                .ToListAsync();
        }

        private static byte[] GenerarReporteExcel(IReadOnlyCollection<Libro> libros){
            using var workbook = new XLWorkbook();
            var hoja = workbook.Worksheets.Add("Reporte de Libros");

            hoja.Cell(1, 1).Value = "Reporte de libros: Biblioteca Universitaria";
            hoja.Range(1, 1, 1, 4).Merge();
            hoja.Cell(1, 1).Style.Font.Bold = true;
            hoja.Cell(1, 1).Style.Font.FontSize = 16;
            hoja.Cell(1, 1).Style.Font.FontColor = XLColor.FromHtml("#1565C0");
            hoja.Cell(2, 1).Value = $"Generado el {DateTime.Now:dd/MM/yyyy HH:mm}";
            hoja.Range(2, 1, 2, 4).Merge();
            hoja.Cell(2, 1).Style.Font.FontColor = XLColor.Gray;

            var encabezados = new[]{
                "ID", 
                "Título", 
                "Autor", 
            "Categoría" };
            for (int i = 0; i < encabezados.Length; i++){
                hoja.Cell(4, i + 1).Value = encabezados[i];
            }

            var encabezadoTabla = hoja.Range(4, 1, 4, 4);
            encabezadoTabla.Style.Font.Bold = true;
            encabezadoTabla.Style.Font.FontColor = XLColor.White;
            encabezadoTabla.Style.Fill.BackgroundColor = XLColor.FromHtml("#1565C0");
            encabezadoTabla.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            var fila = 5;
            foreach (var libro in libros){
                hoja.Cell(fila, 1).Value = libro.ID;
                hoja.Cell(fila, 2).Value = libro.Titulo;
                hoja.Cell(fila, 3).Value = libro.Autor;
                hoja.Cell(fila, 4).Value = libro.Categoria;
                fila++;
            }
            var bordesTabla = hoja.Range(4, 1, Math.Max(fila - 1, 4), 4);
            bordesTabla.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            bordesTabla.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
            bordesTabla.Style.Border.OutsideBorderColor = XLColor.LightGray;
            bordesTabla.Style.Border.InsideBorderColor = XLColor.LightGray;
            hoja.Range(5, 1, Math.Max(fila - 1, 5), 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            hoja.Cell(fila + 1, 3).Value = "Total de libros:";
            hoja.Cell(fila + 1, 3).Style.Font.Bold = true;
            hoja.Cell(fila + 1, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
            hoja.Cell(fila + 1, 4).Value = libros.Count;
            hoja.Cell(fila + 1, 4).Style.Font.Bold = true;
            hoja.Columns(1, 4).AdjustToContents();
            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            return stream.ToArray();
        }

        private static byte[] GenerarReportePdf(IReadOnlyCollection<Libro> libros){
            return Document.Create(document => document.Page(page =>
            {
                page.Size(PageSizes.Letter);
                page.Margin(42);
                page.PageColor(Colors.Grey.Lighten5);
                page.Header().Column(column =>
                {
                    column.Item().Text("BIBLIOTECA UNIVERSITARIA")
                        .FontSize(11)
                        .Bold()
                        .FontColor(Colors.Blue.Darken2);
                    column.Item().Text("Reporte de libros")
                        .FontSize(26)
                        .Bold()
                        .FontColor(Colors.Grey.Darken3);
                    column.Item().Text($"Generado el {DateTime.Now:dd/MM/yyyy HH:mm}")
                        .FontSize(10)
                        .FontColor(Colors.Grey.Darken1);
                    column.Item().PaddingTop(14).LineHorizontal(2).LineColor(Colors.Blue.Medium);
                });

                page.Content().PaddingVertical(22).Column(column =>{

                    column.Item().Background(Colors.Blue.Darken2).Padding(14).Row(row =>{
                        row.RelativeItem().Text("Libros Registrados")
                            .FontSize(11)
                            .Bold()
                            .FontColor(Colors.White);
                        row.ConstantItem(45).AlignRight().Text(libros.Count.ToString())
                            .FontSize(18)
                            .Bold()
                            .FontColor(Colors.White);
                    });

                    column.Item().PaddingTop(18).Table(table =>{
                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(42);
                            columns.RelativeColumn(2.5f);
                            columns.RelativeColumn(1.8f);
                            columns.RelativeColumn(1.5f);
                        });

                        table.Header(header =>
                        {
                            header.Cell().Element(HeaderCellStyle).Text("ID");
                            header.Cell().Element(HeaderCellStyle).Text("TÍTULO");
                            header.Cell().Element(HeaderCellStyle).Text("AUTOR");
                            header.Cell().Element(HeaderCellStyle).Text("CATEGORÍA");
                        });

                        foreach (var libro in libros)
                        {
                            table.Cell().Element(BodyCellStyle).Text(libro.ID.ToString());
                            table.Cell().Element(BodyCellStyle).Text(libro.Titulo);
                            table.Cell().Element(BodyCellStyle).Text(libro.Autor);
                            table.Cell().Element(BodyCellStyle).Text(libro.Categoria);
                        }
                    });
                });

                page.Footer().AlignCenter().Text(text =>
                {
                    text.Span("Biblioteca Universitaria – Página ");
                    text.CurrentPageNumber();
                    text.Span(" de ");
                    text.TotalPages();
                });

            })).GeneratePdf();

            static IContainer HeaderCellStyle(IContainer container) => container
                .Background(Colors.Blue.Darken2)
                .PaddingVertical(9)
                .PaddingHorizontal(8)
                .DefaultTextStyle(text => text.FontSize(9).Bold().FontColor(Colors.White));

            static IContainer BodyCellStyle(IContainer container) => container
                .Background(Colors.White)
                .BorderBottom(1)
                .BorderColor(Colors.Grey.Lighten2)
                .PaddingVertical(9)
                .PaddingHorizontal(8)
                .DefaultTextStyle(text => text.FontSize(9).FontColor(Colors.Grey.Darken3));
        }
    }
}
