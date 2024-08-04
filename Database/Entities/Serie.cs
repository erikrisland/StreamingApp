namespace Database.Entities
{
	public class Serie
	{
		public int Id { get; set; }
		public string Nombre { get; set; }
		public string ImagenUrl { get; set; }
		public string LinkVideo { get; set; }

		//FK
		public int GeneroId { get; set; } 
		public int? SubGeneroId { get; set; }
		public int ProductoraId { get; set; }

		//Navigation Property
		public Genero Genero { get; set; } 
		public Genero? SubGenero { get; set; }
		public Productora Productora { get; set; } 


	}
}
