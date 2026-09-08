using BibliotecaMVC.Models;

namespace BibliotecaMVC.Repositories
{
    public class RepositorioAutorMemoria : IRepositorioAutor
    {
        private static readonly List<Autor> _autores = new()
        {
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
			}
        };

        public IEnumerable<Autor> ObtenerTodos() => _autores;

        public Autor ObtenerPorId(int id) => _autores.FirstOrDefault(x => x.ID == id);

        public void Agregar(Autor autor)
        {
            autor.ID = _autores.Any() ? _autores.Max(x => x.ID) + 1 : 1;
            _autores.Add(autor);
        }

        public void Actualizar(Autor autor)
        {
            var existente = ObtenerPorId(autor.ID);
            if (existente != null)
            {
                existente.Nombre = autor.Nombre;
                existente.Apellido = autor.Apellido;
                existente.Nacionalidad = autor.Nacionalidad;
                existente.FechaNacimiento = autor.FechaNacimiento;
                existente.Activo = autor.Activo;
            }
        }

        public void Eliminar(int id)
{
            var autor = ObtenerPorId(id);
            if (autor != null)
            {
                _autores.Remove(autor);
            }
        }
    }
}