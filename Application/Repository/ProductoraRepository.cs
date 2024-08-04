using Database;
using Database.Contexts;
using Database.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repository
{
	public class ProductoraRepository
	{
		private readonly ApplicationContext _dbContext;

		public ProductoraRepository(ApplicationContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task AddAsync(Productora productora)
		{
			await _dbContext.Set<Productora>().AddAsync(productora);
			await _dbContext.SaveChangesAsync();
		}

		public async Task UpdateAsync(Productora productora)
		{
			_dbContext.Entry(productora).State = EntityState.Modified;
			await _dbContext.SaveChangesAsync();
		}

		public async Task DeleteAsync(Productora productora)
		{
			_dbContext.Set<Productora>().Remove(productora);
			await _dbContext.SaveChangesAsync();
		}

		public async Task<List<Productora>> GetAllAsync()
		{
			return await _dbContext.Set<Productora>().ToListAsync();
		}
		public async Task<Productora> GetByIdAsync(int id)
		{
			return await _dbContext.Set<Productora>().FindAsync(id);
		}
	}
}
