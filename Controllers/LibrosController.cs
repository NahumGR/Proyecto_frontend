using Microsoft.AspNetCore.Mvc;
using BibliotecaMVC.Models;
namespace BibliotecaMVC.Controllers;

public class LibrosController : Controller
{
	private static List<Libro> _libros = new List<Libro>()
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
    
    public IActionResult Index(){
        return View(_libros);
    }
    
    public IActionResult Details(int id)
{
    var libro = _libros.FirstOrDefault(x => x.ID == id);

    if (libro == null)
    {
        return NotFound();
    }

    return View(libro);
}
    
    
    public IActionResult Create(){
		return View();
	}
	
	
	[HttpPost]
	[ValidateAntiForgeryToken]
	public IActionResult Create(Libro libro){
		if (!ModelState.IsValid){
			return View(libro);
		}
		
		if (_libros.Any()){
			libro.ID= _libros.Max(x => x.ID) +1;	
		}else{
			libro.ID= 1;	
		}
		
		_libros.Add(libro);
		return RedirectToAction(nameof(Index));
	}
    
    public IActionResult Edit(int id)
{
    var libro = _libros.FirstOrDefault(x => x.ID == id);

    if (libro == null)
    {
        return NotFound();
    }

    return View(libro);
}

[HttpPost]
[ValidateAntiForgeryToken]
public IActionResult Edit(int id, Libro libro)
{
    if (id != libro.ID)
    {
        return NotFound();
    }

    if (!ModelState.IsValid)
    {
        return View(libro);
    }

    var libroExistente = _libros.FirstOrDefault(x => x.ID == id);
    if (libroExistente == null)
    {
        return NotFound();
    }

    libroExistente.Titulo = libro.Titulo;
    libroExistente.Autor = libro.Autor;
    libroExistente.Categoria = libro.Categoria;
    libroExistente.Precio = libro.Precio;
    libroExistente.Disponible = libro.Disponible;

    return RedirectToAction(nameof(Index));
}

public IActionResult Delete(int id)
{
    var libro = _libros.FirstOrDefault(x => x.ID == id);

    if (libro == null)
    {
        return NotFound();
    }

    return View(libro);
}

[HttpPost, ActionName("Delete")]
[ValidateAntiForgeryToken]
public IActionResult DeleteConfirmed(int id)
{
    var libro = _libros.FirstOrDefault(x => x.ID == id);

    if (libro != null)
    {
        _libros.Remove(libro);
    }

    return RedirectToAction(nameof(Index));
}
}
