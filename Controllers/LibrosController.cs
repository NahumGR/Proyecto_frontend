using Microsoft.AspNetCore.Mvc;
using BibliotecaMVC.Models;
using BibliotecaMVC.Repositories;


namespace BibliotecaMVC.Controllers;

public class LibrosController : Controller{

    private readonly IRepositorioLibro _repositorio;

    public LibrosController(IRepositorioLibro repositorio){
        _repositorio = repositorio;
    }

    public IActionResult Index(){
        var libros = _repositorio.ObtenerTodos();
        return View(libros);
    }
    
    public IActionResult Details(int id){
        var libro = _repositorio.ObtenerPorId(id);

        if (libro == null){
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
		
		_repositorio.Agregar(libro);
		return RedirectToAction(nameof(Index));
	}
    
    public IActionResult Edit(int id){
        var libro = _repositorio.ObtenerPorId(id);

        if (libro == null){
            return NotFound();
        }
            
        return View(libro);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, Libro libro){
        if (id != libro.ID)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return View(libro);
        }

    var libroExistente = _repositorio.ObtenerPorId(id);
    if (libroExistente == null)
    {
        return NotFound();
    }

   _repositorio.Actualizar(libro);

    return RedirectToAction(nameof(Index));
}

public IActionResult Delete(int id)
{
    var libro = _repositorio.ObtenerPorId(id);

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
    _repositorio.Eliminar(id);
    return RedirectToAction(nameof(Index));
}
}
