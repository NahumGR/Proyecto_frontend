using Microsoft.AspNetCore.Mvc;
using BibliotecaMVC.Models;
namespace BibliotecaMVC.Controllers;

public class CategoriasController : Controller
{
	private static List<Categoria> _categorias = new List<Categoria>()
		{
			new Categoria{
				ID= 1,
				Nombre ="Programacion",
				Descripcion = "", 
				ImagenUrl = "programacion.jpg"
			},
			new Categoria{
				ID= 2,
				Nombre ="Literatura",
				Descripcion = "",
				ImagenUrl= "literario.jpg"
			},
			new Categoria{
				ID= 3,
				Nombre ="Fantasia",
				Descripcion = "",
				ImagenUrl= "fantasia.jpg"
			},
			new Categoria{
				ID= 4,
				Nombre ="Comedia Aventura",
				Descripcion = "",
				ImagenUrl= "comedia.jpg"
			},
			new Categoria{
				ID= 5,
				Nombre = "Comedia Aventura",
				Descripcion = "",
				ImagenUrl="comedia.jpg"
			},
			new Categoria{
				ID= 6,
				Nombre = "Narrativo",
				Descripcion = "",
				ImagenUrl ="narrativo.jpg"
			},
		};
    
    public IActionResult Index(){
		return View("~/Views/Home/Categorias.cshtml", _categorias);

    }
    
    public IActionResult Details(int id){
    var categoria = _categorias.FirstOrDefault(x => x.ID == id);

    if (categoria == null)
    {
        return NotFound();
    }

    return View(categoria);
	
}
    

}
