using BibliotecaMVC.Data;
using Microsoft.AspNetCore.Mvc;
using BibliotecaMVC.Models;
using BibliotecaMVC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaMVC.Controllers;

public class LibrosController : Controller{

    private readonly BibliotecaContext _context;

    public LibrosController(BibliotecaContext context){
        _context = context;
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
    
    public async Task<IActionResult> Edit(int id){
        var libro = await _context.Libros.FindAsync(id);

        if (libro == null){
            return NotFound();
        }
            
        return View(libro);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Libro libro){
        if (id != libro.ID){ return NotFound();}

        if (!ModelState.IsValid)
        {
            return View(libro);
        }

    var libroExistente = await _context.Libros.FindAsync(id);
    if (libroExistente == null)
    {
        return NotFound();
    }
    
    libroExistente.Titulo = libro.Titulo;
    libroExistente.Autor = libro.Autor;
    libroExistente.Categoria = libro.Categoria;
    libroExistente.Precio = libro.Precio;
    libroExistente.Disponible = libro.Disponible;
    libroExistente.ImagenUrl = libro.ImagenUrl;

   _context.Libros.Update(libroExistente);
   await _context.SaveChangesAsync();

    return RedirectToAction(nameof(Index));
}

public async Task<IActionResult> Delete(int id)
{
    var libro = await _context.Libros.FindAsync(id);
    if (libro == null)
    {
        return NotFound();
    }

    return View(libro);
}

[HttpPost, ActionName("Delete")]
[ValidateAntiForgeryToken]
public async Task<IActionResult> DeleteConfirmed(int id)
{
     var libro = await _context.Libros.FindAsync(id);
    if (libro != null)
    {
        _context.Libros.Remove(libro);
        await _context.SaveChangesAsync();
    }
    return RedirectToAction(nameof(Index));
}
}
