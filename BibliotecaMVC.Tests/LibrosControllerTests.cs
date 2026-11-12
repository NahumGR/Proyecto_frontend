using BibliotecaMVC.Controllers;
using BibliotecaMVC.Data;
using BibliotecaMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaMVC.Tests;

public class LibrosControllerTests
{
    private static BibliotecaContext CreateContext(string? databaseName = null)
    {
        var options = new DbContextOptionsBuilder<BibliotecaContext>()
            .UseInMemoryDatabase(databaseName ?? Guid.NewGuid().ToString())
            .Options;

        return new BibliotecaContext(options);
    }

    private static LibrosController CreateController(BibliotecaContext context)
    {
        return new LibrosController(context);
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

    [Fact]
    public async Task Index_ConLibros_DevuelveLaListaDeLibros()
    {
        await using var context = CreateContext();
        context.Libros.AddRange(NuevoLibro("Clean Code"), NuevoLibro("Don Quijote"));
        await context.SaveChangesAsync();
        var controller = CreateController(context);

        var result = await controller.Index();

        var view = Assert.IsType<ViewResult>(result);
        var libros = Assert.IsAssignableFrom<IEnumerable<Libro>>(view.Model);
        Assert.Equal(2, libros.Count());
    }

    [Fact]
    public async Task Index_SinLibros_DevuelveUnaListaVacia()
    {
        await using var context = CreateContext();
        var controller = CreateController(context);

        var result = await controller.Index();

        var view = Assert.IsType<ViewResult>(result);
        var libros = Assert.IsAssignableFrom<IEnumerable<Libro>>(view.Model);
        Assert.Empty(libros);
    }

    [Fact]
    public async Task Details_ConLibroExistente_DevuelveElLibro()
    {
        await using var context = CreateContext();
        var libro = NuevoLibro("Harry Potter");
        context.Libros.Add(libro);
        await context.SaveChangesAsync();
        var controller = CreateController(context);

        var result = await controller.Details(libro.ID);

        var view = Assert.IsType<ViewResult>(result);
        Assert.NotNull(view.Model);
        Assert.Same(libro, view.Model);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(999)]
    public async Task Details_ConLibroInexistente_DevuelveNotFound(int id)
    {
        await using var context = CreateContext();
        var controller = CreateController(context);

        var result = await controller.Details(id);

        Assert.IsType<NotFoundResult>(result);
    }


    [Fact]
    public void Create_Get_DevuelveLaVista()
    {
        using var context = CreateContext();
        var controller = CreateController(context);

        var result = controller.Create();

        Assert.IsType<ViewResult>(result);
    }

    [Fact]
    public async Task Create_ConLibroValido_GuardaElLibroYRedirigeAlIndice()
    {
        await using var context = CreateContext();
        var controller = CreateController(context);
        var libro = NuevoLibro("Cuentos de barro");

        var result = await controller.Create(libro);

        var redirect = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal(nameof(LibrosController.Index), redirect.ActionName);
        Assert.Equal(1, await context.Libros.CountAsync());
        Assert.Equal("Cuentos de barro", (await context.Libros.SingleAsync()).Titulo);
    }

    [Fact]
    public async Task Create_ConModeloInvalido_NoGuardaYDevuelveLaVista()
    {
        await using var context = CreateContext();
        var controller = CreateController(context);
        controller.ModelState.AddModelError(nameof(Libro.Titulo), "El título es obligatorio.");
        var libro = NuevoLibro(string.Empty);

        var result = await controller.Create(libro);

        var view = Assert.IsType<ViewResult>(result);
        Assert.Same(libro, view.Model);
        Assert.False(controller.ModelState.IsValid);
        Assert.Equal(0, await context.Libros.CountAsync());
    }


    [Fact]
    public async Task Edit_ConLibroInexistente_DevuelveNotFound()
    {
        await using var context = CreateContext();
        var controller = CreateController(context);
        var libro = NuevoLibro();
        libro.ID = 999;

        var result = await controller.Edit(999, libro);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task Edit_ConIdentificadorDiferente_DevuelveNotFound()
    {
        await using var context = CreateContext();
        var controller = CreateController(context);
        var libro = NuevoLibro();
        libro.ID = 1;

        var result = await controller.Edit(2, libro);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task Edit_ConModeloInvalido_DevuelveLaVistaSinGuardar()
    {
        var databaseName = Guid.NewGuid().ToString();
        int libroId;
        await using (var seedContext = CreateContext(databaseName))
        {
            var libro = NuevoLibro("Titulo original");
            seedContext.Libros.Add(libro);
            await seedContext.SaveChangesAsync();
            libroId = libro.ID;
        }

        await using var context = CreateContext(databaseName);
        var controller = CreateController(context);
        controller.ModelState.AddModelError(nameof(Libro.Titulo), "El título es obligatorio.");
        var libroEditado = NuevoLibro("Titulo cambiado");
        libroEditado.ID = libroId;

        var result = await controller.Edit(libroId, libroEditado);

        var view = Assert.IsType<ViewResult>(result);
        Assert.Same(libroEditado, view.Model);
        var guardado = await context.Libros.AsNoTracking().SingleAsync();
        Assert.Equal("Titulo original", guardado.Titulo);
    }

    [Fact]
    public async Task Edit_ConLibroValido_ActualizaElLibroYRedirigeAlIndice()
    {
        var databaseName = Guid.NewGuid().ToString();
        int libroId;
        await using (var seedContext = CreateContext(databaseName))
        {
            var libro = NuevoLibro("Titulo original");
            seedContext.Libros.Add(libro);
            await seedContext.SaveChangesAsync();
            libroId = libro.ID;
        }

        await using var context = CreateContext(databaseName);
        var controller = CreateController(context);
        var libroEditado = new Libro
        {
            ID = libroId,
            Titulo = "Titulo actualizado",
            Autor = "Autor actualizado",
            Categoria = "Novela",
            Precio = 10M,
            Disponible = false
        };

        var result = await controller.Edit(libroId, libroEditado);

        var redirect = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal(nameof(LibrosController.Index), redirect.ActionName);
        var guardado = await context.Libros.AsNoTracking().SingleAsync();
        Assert.Equal("Titulo actualizado", guardado.Titulo);
        Assert.Equal("Autor actualizado", guardado.Autor);
        Assert.Equal(10M, guardado.Precio);
        Assert.False(guardado.Disponible);
    }


    [Fact]
    public async Task Delete_Get_ConLibroExistente_DevuelveLaVistaConElLibro()
    {
        await using var context = CreateContext();
        var libro = NuevoLibro();
        context.Libros.Add(libro);
        await context.SaveChangesAsync();
        var controller = CreateController(context);

        var result = await controller.Delete(libro.ID);

        var view = Assert.IsType<ViewResult>(result);
        Assert.Same(libro, view.Model);
    }

    [Fact]
    public async Task Delete_Get_ConLibroInexistente_DevuelveNotFound()
    {
        await using var context = CreateContext();
        var controller = CreateController(context);

        var result = await controller.Delete(999);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task DeleteConfirmed_ConLibroExistente_EliminaElLibroYRedirigeAlIndice()
    {
        await using var context = CreateContext();
        var libro = NuevoLibro();
        context.Libros.Add(libro);
        await context.SaveChangesAsync();
        var controller = CreateController(context);

        var result = await controller.DeleteConfirmed(libro.ID);

        var redirect = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal(nameof(LibrosController.Index), redirect.ActionName);
        Assert.Empty(context.Libros);
    }

    [Fact]
    public async Task DeleteConfirmed_ConLibroInexistente_RedirigeSinEliminarNada()
    {
        await using var context = CreateContext();
        context.Libros.Add(NuevoLibro());
        await context.SaveChangesAsync();
        var controller = CreateController(context);

        var result = await controller.DeleteConfirmed(999);

        var redirect = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal(nameof(LibrosController.Index), redirect.ActionName);
        Assert.Equal(1, await context.Libros.CountAsync());
    }

    [Fact]
    public void LibrosController_RequiereUsuarioAutenticado()
    {
        var atributos = typeof(LibrosController).GetCustomAttributes(typeof(AuthorizeAttribute), true);

        Assert.NotEmpty(atributos);
    }

    [Theory]
    [InlineData("Create")]
    [InlineData("Edit")]
    [InlineData("Delete")]
    [InlineData("DeleteConfirmed")]
    public void AccionesDeEscritura_RequierenRolAdminOBibliotecario(string accion)
    {
        var metodos = typeof(LibrosController).GetMethods()
            .Where(m => m.Name == accion && m.DeclaringType == typeof(LibrosController))
            .ToList();

        Assert.NotEmpty(metodos);
        foreach (var metodo in metodos)
        {
            var atributo = metodo.GetCustomAttributes(typeof(AuthorizeAttribute), true)
                .Cast<AuthorizeAttribute>()
                .SingleOrDefault();

            Assert.NotNull(atributo);
            Assert.Equal("Admin,Bibliotecario", atributo.Roles);
        }
    }
}
