namespace BibliotecaMVC.Models
{
	public class Libro
	{
		public int  ID {get; set;}
		public required string Titulo {get; set;}
		public required string  Autor {get; set;}
		public required string  Categoria {get; set;}
		public decimal  Precio {get; set;}
		public bool  Disponible {get; set;}
		public string? ImagenUrl { get; set; }
	}	
}
