using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BibliotecaMVC.Models
{
	public class Autor
	{
		public int  ID {get; set;}
		
		[StringLength(100)]
		public required string Nombre {get; set;}
		
		[StringLength(100)]
		public required string  Apellido {get; set;}
		
		[StringLength(50)]
		public required string  Nacionalidad {get; set;}
		
		[DataType(DataType.Date)]
		public required DateTime  FechaNacimiento {get; set;}
		
		public bool  Activo {get; set;}
	}	
	
}
