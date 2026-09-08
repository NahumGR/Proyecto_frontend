using BibliotecaMVC.Models;

namespace BibliotecaMVC.Repositories{
    public class RepositorioMemoria:  IRepositorioLibro{

		private static readonly List<Libro> _libros = new(){
            new Libro { 
                ID = 1, Titulo = "Clean Code", 
                Autor = "Robert Martin", 
                Categoria = "Programación" , 
                Precio = 35.4M, Disponible = true  , 
                ImagenUrl = "clean_code.png" 
            },
            new Libro {
                ID = 2, 
                Titulo = "Cien años de Soledad",
                Autor = "Gabriel García Marquez", 
                Categoria = "Literatura", 
                Precio = 19M, 
                Disponible = false, 
                ImagenUrl = "Cien_años_de_soledad.png" 
                
            },
            new Libro 
            { 
                ID = 3, Titulo = "Harry Potter", 
                Autor = "Joanne Rowling", 
                Categoria = "Fantasia", Precio = 25M, 
                Disponible = false, 
                ImagenUrl = "Harry_Potter.jpg" 
            },
            new Libro 
            {
                ID = 4, Titulo = "Don Quijote", 
                Autor = "Miguel de Cervantes", 
                Categoria = "Comedia Aventura", 
                Precio = 25M, Disponible = true, 
                ImagenUrl = "don_quijote.jpg" 
                },
            new Libro { 
                ID = 5, Titulo = "La casa de los espíritus", 
                Autor = "Isabel Allende", 
                Categoria = "Comedia Aventura", 
                Precio = 27M, 
                Disponible = false, 
                ImagenUrl = "casa_espiritus.jpg" 
                },
            new Libro 
            { ID =6, 
            Titulo = 
            "Cuentos de barro", 
            Autor = "Salvador Arrué", 
            Categoria = "narrativo", 
            Precio = 27M, 
            Disponible = true, 
            ImagenUrl = "cuentos_barro.jpg" 
            }
        };

        public IEnumerable<Libro> ObtenerTodos() => _libros;

		public Libro ObtenerPorId(int id) => _libros.FirstOrDefault(x => x.ID == id);

		public void Actualizar(Libro libro)
        {
            var existente = ObtenerPorId(libro.ID);
            if (existente != null)
            {
                existente.Titulo = libro.Titulo;
                existente.Autor = libro.Autor;
                existente.Categoria = libro.Categoria;
                existente.Precio = libro.Precio;
                existente.Disponible = libro.Disponible;
                existente.ImagenUrl = libro.ImagenUrl;
            }
        }

		public void Eliminar(int id)
        {
            var libro = ObtenerPorId(id);
            if (libro != null)
            {
                _libros.Remove(libro);
            }
        }

		public void Agregar(Libro libro)
        {
            libro.ID = _libros.Any() ? _libros.Max(x => x.ID) + 1 : 1;
            _libros.Add(libro);
        }
        
    }
}
