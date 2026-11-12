using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BibliotecaMVC.Models;
using Microsoft.AspNetCore.Authorization;

namespace BibliotecaMVC.Controllers;
public class HomeController : Controller
{
    [AllowAnonymous]
    public IActionResult Index()
    {
        return View();
    }
	
	public IActionResult Categorias()
    {

		return RedirectToAction("Index", "Categorias");
	}
    
    [Authorize(Roles = "Admin,Bibliotecario")]
	public IActionResult Usuarios()
	{
		return View();
	}
    
	[Authorize(Roles = "Admin,Bibliotecario")]
	public IActionResult Prestamos()
	{
		return View();
	}
	
    [AllowAnonymous]
	public IActionResult AcercaDe()
	{
		return View();
	}

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    [AllowAnonymous]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    
    [AllowAnonymous]
    public IActionResult CodigoEstado(int codigo)
    {
        Response.StatusCode = codigo;
        return View("Error", new ErrorViewModel { StatusCode = codigo });
    }
}
