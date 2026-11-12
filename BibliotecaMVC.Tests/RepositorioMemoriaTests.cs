using BibliotecaMVC.Models;
using BibliotecaMVC.Repositories;

namespace BibliotecaMVC.Tests;

public class RepositorioMemoriaTests
{
    private static IRepositorioLibro CreateRepositorio()
    {
        return new RepositorioMemoria();
    }

    private static Libro NuevoLibro(string titulo = "Libro de prueba")
    {
        return new Libro
        {
            Titulo = titulo,
            Autor = "Autor de prueba",
            Categoria = "Pruebas",
            Precio = 10M,
            Disponible = true
        };
    }

    [Fact]
    public void ObtenerTodos_DevuelveLosLibrosIniciales()
    {
        var repositorio = CreateRepositorio();

        var libros = repositorio.ObtenerTodos();

        Assert.NotEmpty(libros);
    }

    [Fact]
    public void ObtenerPorId_ConIdExistente_DevuelveElLibro()
    {
        var repositorio = CreateRepositorio();

        var libro = repositorio.ObtenerPorId(1);

        Assert.NotNull(libro);
        Assert.Equal(1, libro.ID);
        Assert.Equal("Clean Code", libro.Titulo);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(9999)]
    public void ObtenerPorId_ConIdInexistente_DevuelveNull(int id)
    {
        var repositorio = CreateRepositorio();

        var libro = repositorio.ObtenerPorId(id);

        Assert.Null(libro);
    }

    [Fact]
    public void Agregar_AsignaElSiguienteIdYGuardaElLibro()
    {
        var repositorio = CreateRepositorio();
        var idMaximoAntes = repositorio.ObtenerTodos().Max(l => l.ID);
        var libro = NuevoLibro("Agregar_AsignaId");

        repositorio.Agregar(libro);

        Assert.Equal(idMaximoAntes + 1, libro.ID);
        Assert.Same(libro, repositorio.ObtenerPorId(libro.ID));
    }

    [Fact]
    public void Actualizar_ConLibroExistente_ModificaLosDatos()
    {
        var repositorio = CreateRepositorio();
        var libro = NuevoLibro("Actualizar_Original");
        repositorio.Agregar(libro);
        var cambios = new Libro
        {
            ID = libro.ID,
            Titulo = "Actualizar_Nuevo",
            Autor = "Otro autor",
            Categoria = "Otra categoría",
            Precio = 99M,
            Disponible = false
        };

        repositorio.Actualizar(cambios);

        var actualizado = repositorio.ObtenerPorId(libro.ID);
        Assert.Equal("Actualizar_Nuevo", actualizado.Titulo);
        Assert.Equal("Otro autor", actualizado.Autor);
        Assert.Equal(99M, actualizado.Precio);
        Assert.False(actualizado.Disponible);
    }

    [Fact]
    public void Actualizar_ConLibroInexistente_NoAgregaNiModificaNada()
    {
        var repositorio = CreateRepositorio();
        var cantidadAntes = repositorio.ObtenerTodos().Count();
        var libro = NuevoLibro("Actualizar_Inexistente");
        libro.ID = 9999;

        repositorio.Actualizar(libro);

        Assert.Equal(cantidadAntes, repositorio.ObtenerTodos().Count());
        Assert.Null(repositorio.ObtenerPorId(9999));
    }

    [Fact]
    public void Eliminar_ConLibroExistente_LoQuitaDelListado()
    {
        var repositorio = CreateRepositorio();
        var libro = NuevoLibro("Eliminar_Existente");
        repositorio.Agregar(libro);

        repositorio.Eliminar(libro.ID);

        Assert.Null(repositorio.ObtenerPorId(libro.ID));
    }

    [Fact]
    public void Eliminar_ConIdInexistente_NoAfectaElListado()
    {
        var repositorio = CreateRepositorio();
        var cantidadAntes = repositorio.ObtenerTodos().Count();

        repositorio.Eliminar(9999);

        Assert.Equal(cantidadAntes, repositorio.ObtenerTodos().Count());
    }
}
