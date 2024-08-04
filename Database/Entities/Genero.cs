namespace Database.Entities
{
	public class Genero
	{
		public int Id { get; set; }
		public string Nombre { get; set; }
		public ICollection<Serie> Series { get; set; }
		public ICollection<Serie> SubGeneroSeries { get; set; }
	}
}
