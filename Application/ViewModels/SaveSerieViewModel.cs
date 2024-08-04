using Database.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
	public class SaveSerieViewModel
	{
		public int Id { get; set; }
        [Required(ErrorMessage = "Debe colocar el nombre de la serie")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Debe colocar el URL de la imagen")]
        public string ImagenUrl { get; set; }
        [Required(ErrorMessage = "Debe colocar el URL del video de YouTube")]
        public string LinkVideo { get; set; }
        [Required(ErrorMessage = "Debe seleccionar el género")]
        public int GeneroId { get; set; }
		public int? SubGeneroId { get; set; }
        [Required(ErrorMessage = "Debe seleccionar la productora")]
        public int ProductoraId { get; set; }

		public SaveSerieViewModel()
		{
			GenerosDisponibles = new List<Genero>();
			ProductorasDisponibles = new List<Productora>();
		}

		public List<Genero> GenerosDisponibles { get; set; }
		public List<Productora> ProductorasDisponibles { get; set; }
	}
}
