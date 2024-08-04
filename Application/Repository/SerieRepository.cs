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
    public class SerieRepository
    {
        private readonly ApplicationContext _dbContext;

        public SerieRepository(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Serie serie)
        {
            await _dbContext.Series.AddAsync(serie);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Serie serie)
        {
            _dbContext.Series.Update(serie);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Serie serie)
        {
            _dbContext.Series.Remove(serie);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Serie> GetByIdAsync(int id)
        {
            return await _dbContext.Series.FindAsync(id);
        }

        public async Task<List<Serie>> GetAllAsync()
        {
            return await _dbContext.Series.ToListAsync();
        }

    }
}
