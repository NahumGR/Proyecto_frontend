using BibliotecaMVC.Controllers;
using BibliotecaMVC.Data;
using BibliotecaMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace BibliotecaMVC.Tests;

public class LibrosControllerMockTests
{
    private static Mock<BibliotecaContext> CreateContextMock()
    {
        var options = new DbContextOptionsBuilder<BibliotecaContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var contexto = new Mock<BibliotecaContext>(options) { CallBase = true };
        contexto.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

        return contexto;
    }

    private static Libro NuevoLibro(string titulo = "Clean Code")
    {
        return new Libro
        {
            Titulo = titulo,
            Autor = "Robert Martin",
            Categoria = "Programación",
            Precio = 35.4M,
            Disponible = true
        };
    }

    private static Libro GuardarLibroInicial(Mock<BibliotecaContext> contexto)
    {
        var libro = NuevoLibro("Titulo original");
        contexto.Object.Libros.Add(libro);
        contexto.Object.SaveChanges();
        return libro;
    }

    [Fact]
    public async Task Create_ConLibroValido_GuardaLosCambiosUnaSolaVez()
    {
        var contexto = CreateContextMock();
        var controller = new LibrosController(contexto.Object);

        var result = await controller.Create(NuevoLibro());

        Assert.IsType<RedirectToActionResult>(result);
        contexto.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Create_ConModeloInvalido_NoGuardaLosCambios()
    {
        var contexto = CreateContextMock();
        var controller = new LibrosController(contexto.Object);
        controller.ModelState.AddModelError(nameof(Libro.Titulo), "El título es obligatorio.");

        var result = await controller.Create(NuevoLibro(string.Empty));

        Assert.IsType<ViewResult>(result);
        contexto.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Create_CuandoFallaElGuardado_PropagaLaExcepcion()
    {
        var contexto = CreateContextMock();
        contexto.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(new DbUpdateException("Error simulado de base de datos"));
        var controller = new LibrosController(contexto.Object);

        await Assert.ThrowsAsync<DbUpdateException>(() => controller.Create(NuevoLibro()));
    }

    [Fact]
    public async Task Edit_ConLibroExistente_ActualizaYGuardaLosCambiosUnaSolaVez()
    {
        var contexto = CreateContextMock();
        var libroInicial = GuardarLibroInicial(contexto);
        var controller = new LibrosController(contexto.Object);
        var libroEditado = NuevoLibro("Titulo nuevo");
        libroEditado.ID = libroInicial.ID;

        var result = await controller.Edit(libroInicial.ID, libroEditado);

        Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Titulo nuevo", libroInicial.Titulo);
        contexto.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Edit_ConLibroInexistente_NoGuardaLosCambios()
    {
        var contexto = CreateContextMock();
        var controller = new LibrosController(contexto.Object);
        var libro = NuevoLibro();
        libro.ID = 999;

        var result = await controller.Edit(999, libro);

        Assert.IsType<NotFoundResult>(result);
        contexto.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task DeleteConfirmed_ConLibroExistente_GuardaLosCambiosUnaSolaVez()
    {
        var contexto = CreateContextMock();
        var libro = GuardarLibroInicial(contexto);
        var controller = new LibrosController(contexto.Object);

        var result = await controller.DeleteConfirmed(libro.ID);

        Assert.IsType<RedirectToActionResult>(result);
        contexto.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task DeleteConfirmed_ConLibroInexistente_NoGuardaLosCambios()
    {
        var contexto = CreateContextMock();
        var controller = new LibrosController(contexto.Object);

        var result = await controller.DeleteConfirmed(999);

        Assert.IsType<RedirectToActionResult>(result);
        contexto.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }
}
