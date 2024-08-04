using Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Database.Contexts
{
	public class ApplicationContext : DbContext
	{

		public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }	

		public DbSet<Serie> Series { get; set; }
		public DbSet<Genero> Generos { get; set; }
		public DbSet<Productora> Productoras { get; set; }


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			//Fluent api

			#region Tablas
			modelBuilder.Entity<Serie>().ToTable("Series");
			modelBuilder.Entity<Genero>().ToTable("Generos");
			modelBuilder.Entity<Productora>().ToTable("Productoras");
			#endregion

			#region Primary keys
			modelBuilder.Entity<Serie>().HasKey(s => s.Id);
			modelBuilder.Entity<Genero>().HasKey(g => g.Id);
			modelBuilder.Entity<Productora>().HasKey(p => p.Id);
			#endregion

			#region Relationships

			modelBuilder.Entity<Serie>()
				.HasOne(s => s.Genero)
				.WithMany(g => g.Series)
				.HasForeignKey(s => s.GeneroId)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<Serie>()
				.HasOne(s => s.SubGenero)
				.WithMany(g => g.SubGeneroSeries)
				.HasForeignKey(s => s.SubGeneroId)
				.OnDelete(DeleteBehavior.NoAction);

			modelBuilder.Entity<Serie>()
				.HasOne(s => s.Productora)
				.WithMany(p => p.Series)
				.HasForeignKey(s => s.ProductoraId)
				.OnDelete(DeleteBehavior.Cascade);
			#endregion

			#region Property Configurations

			#region Series
			modelBuilder.Entity<Serie>()
				.Property(s => s.Nombre)
				.HasMaxLength(80)
				.IsRequired();
			modelBuilder.Entity<Serie>()
				.Property(s => s.LinkVideo)
				.HasMaxLength(500)
				.IsRequired();
			modelBuilder.Entity<Serie>()
				.Property(s => s.ImagenUrl)
				.HasMaxLength(1000)
				.IsRequired();
			#endregion

			#region Generos
			modelBuilder.Entity<Genero>()
				.Property(s => s.Nombre)
				.HasMaxLength(80)
				.IsRequired();
			#endregion

			#region Productoras
			modelBuilder.Entity<Productora>()
				.Property(s => s.Nombre)
				.HasMaxLength(80)
				.IsRequired();
			#endregion


			#endregion

		}


	}
}
