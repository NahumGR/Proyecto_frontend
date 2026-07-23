using Microsoft.AspNetCore.Mvc;

namespace BibliotecaMVC.Models
{
	public class Autor
	{
		public int  ID {get; set;}
		public required string Nombre {get; set;}
		public required string  Apellido {get; set;}
		public required string  Nacionalidad {get; set;}
		public required int  FechaNacimiento {get; set;}
		public bool  Activo {get; set;}
	}	
	
}
