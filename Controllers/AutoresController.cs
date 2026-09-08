using Microsoft.AspNetCore.Mvc;
using BibliotecaMVC.Models;
using BibliotecaMVC.Repositories;

namespace BibliotecaMVC.Controllers
{
    public class AutoresController : Controller{
        private readonly IRepositorioAutor _repositorio;

        public AutoresController(IRepositorioAutor repositorio){
            _repositorio = repositorio;
        }

        public IActionResult Index()
        {
            return View(_repositorio.ObtenerTodos());
        }
        
        public IActionResult Details(int id)
        {
            var autor = _repositorio.ObtenerPorId(id);
            if (autor == null)
            {
                return NotFound(); 
            }
            return View(autor);
        }
        
        public IActionResult Create()
{
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Autor autor)
        {
            if (!ModelState.IsValid)
            {
                return View(autor);
            }            
        _repositorio.Agregar(autor);
            return RedirectToAction(nameof(Index));
        }
        
        public IActionResult Edit(int id){
            var autor = _repositorio.ObtenerPorId(id);
            if (autor == null)
            {
                return NotFound();
            }
            return View(autor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Autor autor){
            if (id != autor.ID)
            {
             return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(autor);
            }

            var autorExistente = _repositorio.ObtenerPorId(id);
            if (autorExistente == null)
            {
       return NotFound();
            }

            _repositorio.Actualizar(autor);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var autor = _repositorio.ObtenerPorId(id);
            if (autor == null)
            {
                return NotFound();
            }
            return View(autor);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)    {
            _repositorio.Eliminar(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

