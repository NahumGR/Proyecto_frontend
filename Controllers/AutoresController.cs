using Microsoft.AspNetCore.Mvc;
using BibliotecaMVC.Models;
namespace BibliotecaMVC.Controllers;

public class AutoresController : Controller{
    public IActionResult Index(){
		List<Autor> autores = new List<Autor>()
		{
			new Autor
			{
				ID= 1,
				Nombre ="Miguel",
				Apellido = "de Cervantes Saavedra",
				Nacionalidad = "Española",
				FechaNacimiento = 1547,
				Activo = false
			},
			new Autor{
				ID= 2,
				Nombre ="Gabriel José",
				Apellido = "García Marquez",
				Nacionalidad = "Colombia",
				FechaNacimiento = 1927,
				Activo = false
			},
			new Autor{
				ID= 3,
				Nombre ="Joanne",
				Apellido = "Rowling",
				Nacionalidad = "Británica",
				FechaNacimiento = 1965,
				Activo = true
			},
			new Autor{
				ID= 4,
				Nombre ="Isabel",
				Apellido = "Allende",
				Nacionalidad = "Chilena",
				FechaNacimiento = 1942,
				Activo = true
			},
			new Autor{
				ID= 5,
				Nombre ="Salvador Efraín",
				Apellido = "Salazar Arrué",
				Nacionalidad = "Salvadoreña",
				FechaNacimiento = 1899,
				Activo = false
			},
			new Autor{
				ID= 6,
				Nombre ="Robert",
				Apellido = "Cecil Martin",
				Nacionalidad = "Estadounidense",
				FechaNacimiento = 1952,
				Activo = true
			},
		};
		ViewBag.Autores= autores;
        return View();
    }
}
