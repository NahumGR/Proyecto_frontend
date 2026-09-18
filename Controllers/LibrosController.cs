using BibliotecaMVC.Data;
using Microsoft.AspNetCore.Mvc;
using BibliotecaMVC.Models;
using BibliotecaMVC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaMVC.Controllers;

public class LibrosController : Controller{

    private readonly BibliotecaContext _context;
    private readonly IRepositorioLibro _repositorio;

    public LibrosController(BibliotecaContext context, IRepositorioLibro repositorio){
        _context = context;
        _repositorio = repositorio;
    }

    public async Task<IActionResult> Index(){
        var libros = await _context.Libros.ToListAsync();
        return View(libros);
    }
    
    public async Task<IActionResult> Details(int id){
        var libro = await _context.Libros.FindAsync(id);

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
	public async Task<IActionResult> Create(Libro libro){
		if (!ModelState.IsValid){
			return View(libro);
		}
		
		_context.Libros.Add(libro);
        await _context.SaveChangesAsync();
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
