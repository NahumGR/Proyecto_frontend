using Microsoft.AspNetCore.Mvc;
using BibliotecaMVC.Models;
namespace BibliotecaMVC.Controllers;

public class LibrosController : Controller
{
    public IActionResult Index()
    {
		List<Libro> libros = new List<Libro>()
		{
			new Libro{
				ID= 1,
				Titulo ="Clean Code",
				Autor = "Robert Martin",
				Categoria = "Programación",
				Precio = 35.4M,
				Disponible = true
			},
			new Libro{
				ID= 2,
				Titulo ="Cien años de Soledad",
				Autor = "Gabriel García Marquez",
				Categoria = "Literatura",
				Precio = 19,
				Disponible = false
			},
			new Libro{
				ID= 3,
				Titulo ="Harry Potter",
				Autor = "Joanne Rowling",
				Categoria = "Fantasia",
				Precio = 25,
				Disponible = false
			},
			new Libro{
				ID= 4,
				Titulo ="Don Quijote",
				Autor = "Miguel de Cervantes",
				Categoria = "Comedia Aventura",
				Precio = 25,
				Disponible = true
			},
			new Libro{
				ID= 5,
				Titulo ="La casa de los espíritus",
				Autor = "Isabel Allende",
				Categoria = "Comedia Aventura",
				Precio = 27,
				Disponible = false
			},
			new Libro{
				ID= 6,
				Titulo ="Cuentos de barro",
				Autor = "Salvador Arrué",
				Categoria = "narrativo",
				Precio = 27,
				Disponible = true
			},
		};
		ViewBag.Libros = libros;
        return View();
    }
}
