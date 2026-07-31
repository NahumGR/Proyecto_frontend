using Microsoft.AspNetCore.Mvc;
using BibliotecaMVC.Models;
namespace BibliotecaMVC.Controllers;

public class AutoresController : Controller{
	private static List<Autor> _autores = new List<Autor>(){
		new Autor
			{
				ID= 1,
				Nombre ="Miguel",
				Apellido = "de Cervantes Saavedra",
				Nacionalidad = "Española",
				FechaNacimiento = new DateTime(1547, 9, 29),
				Activo = false
			},
			new Autor{
				ID= 2,
				Nombre ="Gabriel José",
				Apellido = "García Marquez",
				Nacionalidad = "Colombia",
				FechaNacimiento = new DateTime(1927, 3, 6),
				Activo = false
			},
			new Autor{
				ID= 3,
				Nombre ="Joanne",
				Apellido = "Rowling",
				Nacionalidad = "Británica",
				FechaNacimiento = new DateTime(1965, 7, 31),
				Activo = true
			},
			new Autor{
				ID= 4,
				Nombre ="Isabel",
				Apellido = "Allende",
				Nacionalidad = "Chilena",
				FechaNacimiento = new DateTime(1942, 8, 2),
				Activo = true
			},
			new Autor{
				ID= 5,
				Nombre ="Salvador Efraín",
				Apellido = "Salazar Arrué",
				Nacionalidad = "Salvadoreña",
				FechaNacimiento = new DateTime(1899, 10, 22),
				Activo = false
			},
			new Autor{
				ID= 6,
				Nombre ="Robert",
				Apellido = "Cecil Martin",
				Nacionalidad = "Estadounidense",
				FechaNacimiento = new DateTime(1952, 12, 5),
				Activo = true
			},
};
		
    public IActionResult Index(){
        return View(_autores);
    }
    
    public IActionResult Details(int id){
		var autor = _autores.FirstOrDefault(x=> x.ID == id);
		
		if (autor == null){
			return NotFound(); 
		}
		
		return View(autor);
	}
	
	public IActionResult Create(){
		return View();
	}
	
	
	[HttpPost]
	[ValidateAntiForgeryToken]
	public IActionResult Create(Autor autor){
		if (!ModelState.IsValid){
			return View(autor);
		}
		
		if (_autores.Any()){
			autor.ID= _autores.Max(x => x.ID) +1;	
		}else{
			autor.ID= 1;	
		}
		
		_autores.Add(autor);
		return RedirectToAction(nameof(Index));
	}
	
	public IActionResult Edit(int id)
{
    var autor = _autores.FirstOrDefault(x => x.ID == id);

    if (autor == null)
    {
        return NotFound();
    }

    return View(autor);
}

[HttpPost]
[ValidateAntiForgeryToken]
public IActionResult Edit(int id, Autor autor)
{
    if (id != autor.ID)
    {
        return NotFound();
    }

    if (!ModelState.IsValid)
    {
        return View(autor);
    }

    var autorExistente = _autores.FirstOrDefault(x => x.ID == id);
    if (autorExistente == null)
    {
        return NotFound();
    }

    autorExistente.Nombre = autor.Nombre;
    autorExistente.Apellido = autor.Apellido;
    autorExistente.Nacionalidad = autor.Nacionalidad;
    autorExistente.FechaNacimiento = autor.FechaNacimiento;
    autorExistente.Activo = autor.Activo;

    return RedirectToAction(nameof(Index));
}


public IActionResult Delete(int id)
{
    var autor = _autores.FirstOrDefault(x => x.ID == id);

    if (autor == null)
    {
        return NotFound();
    }

    return View(autor);
}

[HttpPost, ActionName("Delete")]
[ValidateAntiForgeryToken]
public IActionResult DeleteConfirmed(int id)
{
    var autor = _autores.FirstOrDefault(x => x.ID == id);

    if (autor != null)
    {
        _autores.Remove(autor);
    }

    return RedirectToAction(nameof(Index));
}


}
