namespace BibliotecaMVC.Models
{
    public class Categoria
    {
        public int ID { get; set; }

        public required string Nombre { get; set; }

        public string? Descripcion { get; set; }

		public string? ImagenUrl { get; set; }
    }
}